using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapToolSystem : MonoBehaviour
{
    [SerializeField]
    Text m_episodeText;

    int m_episodeNumber = 1;

    [SerializeField]
    Text m_stageText;

    int m_stageNumber = 1;

    GameObject m_map;

    bool m_visibleMonsterList = false;

    GameObject m_placementObject;

    Dictionary<GameObject, MonsterCode> m_monsterPlacement = new Dictionary<GameObject, MonsterCode>();

    [SerializeField]
    GameObject m_placementSlots;

    void Start()
    {
        m_map = Instantiate(Resources.Load("Map/Forest") as GameObject);
    }

    public void AddStageNumber()
    {
        m_stageNumber %= 6;
        m_stageNumber++;

        m_stageText.text = " Stage " + m_stageNumber.ToString();
    }

    public void OnMouseDown()
    {
        if (UIManager.Instance.GetUICount() > 0)
        {
            Vector3 mouseLocation = Input.mousePosition;
            mouseLocation = Camera.main.ScreenToWorldPoint(mouseLocation);
            mouseLocation.z = 0;

            m_placementObject = Instantiate(Resources.Load(MonsterManager.Instance.GetSelectedMonster().prefabPath) as GameObject,
                mouseLocation, transform.rotation);
        }
        
    }

    public void OnMouseDrag()
    {
        if(UIManager.Instance.GetUICount() > 0)
        {
            Vector3 mouseLocation = Input.mousePosition;
            mouseLocation = Camera.main.ScreenToWorldPoint(mouseLocation);
            mouseLocation.z = 0;

            m_placementObject.transform.position = mouseLocation;
        }
    }

    public void OnMouseUp()
    {
        if (UIManager.Instance.GetUICount() > 0)
        {
            if(Input.mousePosition.x <= 822 && Input.mousePosition.x >= 133)
            {
                GameObject placementSlot = Instantiate(Resources.Load("UI/PlacementSlot") as GameObject);
                placementSlot.GetComponent<PlacementSlot>().InitializePlacementSlot(m_placementObject, 
                    MonsterManager.Instance.GetSelectedMonster().name);
                placementSlot.GetComponent<PlacementSlot>().onRemove += RemovePlacementMonster;
                placementSlot.transform.SetParent(m_placementSlots.transform);

                m_monsterPlacement.Add(m_placementObject, MonsterManager.Instance.GetSelectedMonster().code);
            }
            else
            {
                Destroy(m_placementObject);
            }
        }
    }

    public void RemovePlacementMonster(GameObject placementObject)
    {
        Destroy(placementObject);
        m_monsterPlacement.Remove(placementObject);
    }

    public void ChangeMap(int mapIndex)
    {
        if(m_map) Destroy(m_map);

        m_episodeNumber = mapIndex + 1;
        m_map = Instantiate(Resources.Load(StageManager.Instance.GetMapPathString(mapIndex)) as GameObject);
        m_map.transform.position = new Vector3(0, 0, 0);

        m_episodeText.text = "Episode " + (m_episodeNumber).ToString();
    }

    public void OnToggleMonsterListUI()
    {
        m_visibleMonsterList = m_visibleMonsterList ? false : true;

        if(m_visibleMonsterList)
        {
            UIManager.Instance.AddUI(UIPrefab.MONSTER_LIST);
        }
        else
        {
            UIManager.Instance.RemoveOneUI();
        }
    }

    public void LoadStageData()
    {

    }

    public void SaveStageData()
    {
        string fileName = "/Resources/JSON/Stage" + m_episodeNumber.ToString() + "-" + m_stageNumber.ToString();
        Stage stageData = new Stage();
        stageData.mapPath = StageManager.Instance.GetMapPathString(m_episodeNumber - 1);
        stageData.monsters = new MonsterPlacement[m_monsterPlacement.Count];
        
        int index = 0;
        foreach(KeyValuePair<GameObject, MonsterCode> data in m_monsterPlacement)
        {
            stageData.monsters[index].code = data.Value;
            stageData.monsters[index].x = data.Key.transform.position.x;
            stageData.monsters[index].y = data.Key.transform.position.y;
            index++;
            Debug.Log("s" + index.ToString());
        }

        JsonHelper.SaveJaon<Stage>(stageData, fileName);
        Debug.Log(fileName);
        Debug.Log(stageData.monsters.Length);
    }
}
