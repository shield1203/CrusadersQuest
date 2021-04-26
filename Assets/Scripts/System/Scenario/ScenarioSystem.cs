﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioSystem : MonoBehaviour
{
    [SerializeField]
    GameObject m_blockBox;

    [SerializeField]
    Transform[] m_blockPosition = new Transform[maxBlockCount];

    List<GameObject> m_skillBlocks = new List<GameObject>();

    IEnumerator OnCreateBlock;

    IEnumerator[] OnMoveBlock = new IEnumerator[9];

    const int maxBlockCount = 9;

    Random m_random = new Random();

    void Start()
    {
        OnCreateBlock = CreateBlock();
        StartCoroutine(OnCreateBlock);
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
                // TestCode
                string thumbnailPath = "Skill/Stormy Waves_sprite";
                switch(random)
                {
                    case 0: thumbnailPath = "Skill/Stormy Waves_sprite"; break;
                    case 1: thumbnailPath = "Skill/Call of the Holy Sword_sprite"; break;
                    case 2: thumbnailPath = "Skill/Its light nyang_sprite"; break;
                }

                block.GetComponent<SkillBlock>().InitializeSkillBlock(maxBlockCount, (BlockColor)random, true, thumbnailPath);
            }
        }
    }

    void UseBlock(int index)
    {
        if (m_skillBlocks[index].transform.position.x != m_blockPosition[index].position.x) return;

        List<int> linkedBlockIndex = GetLinkedBlocksIndex(index);
        // 블록수만큼 스킬 

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
                m_blockPosition[blockIndex].position, 30.0f);
        }

        if (!m_skillBlocks[blockIndex].GetComponent<SkillBlock>().IsPrevBlockLinked() && CanBlockLink(blockIndex))
        {
            m_skillBlocks[blockIndex].GetComponent<SkillBlock>().PrevBlockLink();
            m_skillBlocks[blockIndex - 1].GetComponent<SkillBlock>().NextBlockLink();

            if(GetLinkedBlocksIndex(blockIndex).Count == 3)
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
                linkCount %= 3; // maxBlockChain
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

    void OnFinishedStage()
    {
        StopCoroutine(OnCreateBlock);
    }
}