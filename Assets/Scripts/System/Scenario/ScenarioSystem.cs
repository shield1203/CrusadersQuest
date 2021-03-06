using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenarioSystem : MonoBehaviour
{
    [SerializeField]
    GameObject m_blockBox;

    [SerializeField]
    Transform[] m_blockPosition = new Transform[maxBlockCount];

    List<GameObject> m_skillBlocks = new List<GameObject>();

    IEnumerator OnCreateBlock;

    IEnumerator[] OnMoveBlock = new IEnumerator[maxBlockCount];

    const int maxBlockCount = 9;
    const int maxBlockChain = 3;
    const float blockSpeed = 50.0f;

    const float soldierUnitInitXPos = -20f;
    const float soldierUnitInitYPos = -2.35f;
    const float soldierUnitDistance = 2f;
    const float maxCameraXPos = 48.21f;

    Random m_random = new Random();

    GameObject m_map;

    List<GameObject> m_soldierUnits = new List<GameObject>();
    List<GameObject> m_monsterUnits = new List<GameObject>();

    [SerializeField]
    StageSoldierInfo m_stageSoldierInfo;

    bool m_stageClear = false;
    bool m_gameOver = false;

    private void Awake()
    {
        UIManager.Instance.ActiveUI(false);
    }

    void Start()
    {
        SoundSystem.Instance.StartBGM(BGM.stage_thema);

        OnCreateBlock = CreateBlock();
        StartCoroutine(OnCreateBlock);

        List<SoldierData> soldierTeam = SoldierManager.Instance.GetSoldierTeam();
        for (int index = 0; index < soldierTeam.Count; index++)
        {
            GameObject soldierUnit = Instantiate(Resources.Load(soldierTeam[index].prefabPath) as GameObject);
            soldierUnit.transform.position = new Vector2(soldierUnitInitXPos + (index * soldierUnitDistance), soldierUnitInitYPos);
            m_soldierUnits.Add(soldierUnit);
        }

        Stage curStage = StageManager.Instance.GetStage();
        
        m_map = Instantiate(Resources.Load(curStage.mapPath) as GameObject);

        foreach(MonsterPlacement monster in curStage.monsters)
        {
            MonsterData monsterData = MonsterManager.Instance.GetMonsterData(monster.code);

            Vector2 monsterLocation = new Vector2();
            monsterLocation.x = monster.x;
            monsterLocation.y = monster.y;

            GameObject monsterUnit = Instantiate(Resources.Load(monsterData.prefabPath) as GameObject,
                monsterLocation, transform.rotation);
            monsterUnit.GetComponent<MonsterUnit>().InitializeMonsterUnit(monsterData, m_soldierUnits);

            m_monsterUnits.Add(monsterUnit);
        }

        for (int index = 0; index < m_soldierUnits.Count; index++)
        {
            m_soldierUnits[index].GetComponent<SoldierUnit>().InitializeSoldierUnit(soldierTeam[index], m_monsterUnits);
        }

        m_stageSoldierInfo.InitializeStageSoldierInfo(m_soldierUnits);

        StageManager.Instance.SetClear(false);
    }

    void Update()
    {
        for(int index = 0; index < m_skillBlocks.Count; index++)
        {
            if(m_skillBlocks[index].GetComponent<SkillBlock>().GetBlockIndex() != index)
            {
                OnMoveBlock[index] = MoveBlock(index);
                StartCoroutine(OnMoveBlock[index]);
            }
        }

        if (m_stageClear || m_gameOver) return;

        for (int index = 0; index < m_soldierUnits.Count; index++)
        {
            if(m_soldierUnits[index].transform.position.x > Camera.main.transform.position.x)
            {
                Vector3 position = Camera.main.transform.position;
                position.x = Mathf.Clamp(m_soldierUnits[index].transform.position.x, 0, maxCameraXPos);
                Camera.main.transform.position = position;
            }
        }

        if (CheckStageClear())
        {
            SoundSystem.Instance.StopBGM();
            SoundSystem.Instance.PlaySound(Sound.victory);

            m_stageClear = true;
            StageManager.Instance.SetClear(true);

            StopCoroutine(OnCreateBlock);
            StartCoroutine(StageClear());
        }

        if(CheckGameOver())
        {
            SoundSystem.Instance.StopBGM();

            m_gameOver = true;

            StopCoroutine(OnCreateBlock);
            StartCoroutine(GameOver());
        }
    }

    IEnumerator CreateBlock()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            if(m_skillBlocks.Count < maxBlockCount)
            {
                int random = Random.Range(0, 3);
                GameObject block = Instantiate(Resources.Load("Block/BlockUI") as GameObject, m_blockBox.transform);
                block.transform.position = m_blockPosition[maxBlockCount - 1].position;
                block.GetComponent<SkillBlock>().m_touchDelegate = UseBlock;
                m_skillBlocks.Add(block);

                string thumbnailPath = m_soldierUnits[random].GetComponent<SoldierUnit>().GetSkillThumbnail();
                block.GetComponent<SkillBlock>().InitializeSkillBlock(maxBlockCount, (BlockColor)random, true, thumbnailPath);
            }
        }
    }

    void UseBlock(int index)
    {
        if (m_skillBlocks[index].transform.position.x != m_blockPosition[index].position.x) return;

        List<int> linkedBlockIndex = GetLinkedBlocksIndex(index);
        if(!m_gameOver && !m_stageClear)
        {
            m_soldierUnits[(int)m_skillBlocks[index].GetComponent<SkillBlock>().GetBlockColor()].GetComponent<SoldierUnit>().AddSkillLink(linkedBlockIndex.Count);
        }

        int minIndex = maxBlockCount;
        foreach(int blockIndex in linkedBlockIndex)
        {
            minIndex = minIndex > blockIndex ? blockIndex : minIndex;
            m_skillBlocks[blockIndex].GetComponent<SkillBlock>().CreateParticle();
            Destroy(m_skillBlocks[blockIndex]);
        }

        for(int i = 0; i < maxBlockCount; i++)
        {
            if(OnMoveBlock[i] != null) StopCoroutine(OnMoveBlock[i]);
        }
        
        m_skillBlocks.RemoveRange(minIndex, linkedBlockIndex.Count);
    }

    IEnumerator MoveBlock(int blockIndex)
    {
        m_skillBlocks[blockIndex].GetComponent<SkillBlock>().SetBlockIndex(blockIndex);

        while (m_skillBlocks[blockIndex].transform.position.x != m_blockPosition[blockIndex].position.x)
        {
            yield return null;

            m_skillBlocks[blockIndex].transform.position = Vector3.MoveTowards(m_skillBlocks[blockIndex].transform.position,
                m_blockPosition[blockIndex].position, blockSpeed);
        }

        if (!m_skillBlocks[blockIndex].GetComponent<SkillBlock>().IsPrevBlockLinked() && CanBlockLink(blockIndex))
        {
            m_skillBlocks[blockIndex].GetComponent<SkillBlock>().PrevBlockLink();
            m_skillBlocks[blockIndex - 1].GetComponent<SkillBlock>().NextBlockLink();

            if(GetLinkedBlocksIndex(blockIndex).Count == maxBlockChain)
            {
                foreach(int index in GetLinkedBlocksIndex(blockIndex))
                {
                    m_skillBlocks[index].GetComponent<SkillBlock>().AnimationPlay();
                }
            }
        }
    }

    bool CanBlockLink(int blockIndex)
    {
        if (blockIndex == 0) return false;

        int linkCount = 0;        
        for(int index = 0; index < blockIndex; index++)
        {
            if (m_skillBlocks[index].GetComponent<SkillBlock>().GetBlockColor()
                == m_skillBlocks[blockIndex].GetComponent<SkillBlock>().GetBlockColor())
            {
                linkCount++;
                linkCount %= maxBlockChain;
            }
            else
            {
                linkCount = 0;
            }
        }

        return linkCount > 0;
    }

    List<int> GetLinkedBlocksIndex(int index)
    {
        List<int> linkedBlockIndex = new List<int>();

        int prevIndex = index;
        while (m_skillBlocks[prevIndex].GetComponent<SkillBlock>().IsPrevBlockLinked())
        {
            prevIndex--;
            linkedBlockIndex.Add(prevIndex);
        }

        linkedBlockIndex.Add(index);

        int nextIndex = index;
        while(m_skillBlocks[nextIndex].GetComponent<SkillBlock>().IsNextBlockLinked())
        {
            nextIndex++;
            linkedBlockIndex.Add(nextIndex);
        }

        return linkedBlockIndex;
    }

    bool CheckGameOver()
    {
        bool isGameOver = true;
        for (int index = 0; index < m_soldierUnits.Count; index++)
        {
            if (!m_soldierUnits[index].GetComponent<UnitBase>().IsDie())
            {
                isGameOver = false;
                break;
            }
        }

        return isGameOver;
    }

    IEnumerator GameOver()
    {
        GameObject stageClearUI = Instantiate(Resources.Load("UI/GameOver") as GameObject);

        yield return new WaitForSeconds(4.25f);

        SceneManager.LoadScene("Lobby");
        UIManager.Instance.RemoveOneUI();
    }

    bool CheckStageClear()
    {
        bool isStageClear = true;
        for (int index = 0; index < m_monsterUnits.Count; index++)
        {
            if (!m_monsterUnits[index].GetComponent<UnitBase>().IsDie())
            {
                isStageClear = false;
                break;
            }
        }

        return isStageClear;
    }

    IEnumerator StageClear()
    {
        float goalPosition = Camera.main.transform.position.x - 3.23f;

        for (int index = 0; index < m_soldierUnits.Count; index++)
        {
            m_soldierUnits[index].GetComponent<SoldierUnit>().SetGoalPoint(goalPosition + (index * 3.23f));
        }

        bool isGoal = false;
        while (!isGoal)
        {
            yield return null;

            isGoal = true;
            for (int index = 0; index < m_soldierUnits.Count; index++)
            {
                if (!m_soldierUnits[index].GetComponent<SoldierUnit>().IsGoal())
                {
                    isGoal = false;
                    break;
                }
            }
        }

        GameObject stageClearUI = Instantiate(Resources.Load("UI/StageClear") as GameObject);

        yield return new WaitForSeconds(4.25f);

        SceneManager.LoadScene("Result");
        UIManager.Instance.RemoveOneUI();
    }

    void OnFinishedStage()
    {
        StopCoroutine(OnCreateBlock);
    }
}
