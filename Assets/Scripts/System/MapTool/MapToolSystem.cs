using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Stage
{
    public string mapPath;
    public MonsterPlacement[] monsters;
}

public struct MonsterPlacement
{
    public MonsterCode code;
    public float x;
    public float y;
}

public class MapToolSystem : MonoBehaviour
{
    [SerializeField]
    Text m_episodeText; 

    [SerializeField]
    Text m_stageText;

    int m_stageNumber = 1;

    List<string> m_mapPath = new List<string>();

    GameObject m_map;

    bool m_visibleMonsterList = false;

    GameObject m_placementObject;

    Dictionary<GameObject, MonsterPlacement> m_monsterPlacement = new Dictionary<GameObject, MonsterPlacement>();

    [SerializeField]
    GameObject m_placementSlots;

    void Start()
    {
        m_mapPath.Add("Map/Forest");
        m_mapPath.Add("Map/Desert");
        m_mapPath.Add("Map/DeepSea");
        m_mapPath.Add("Map/Volcano");

        m_map = Instantiate(Resources.Load(m_mapPath[0]) as GameObject);
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
                MonsterPlacement monsterPlacement;
                monsterPlacement.code = MonsterManager.Instance.GetSelectedMonster().code;
                monsterPlacement.x = Input.mousePosition.x;
                monsterPlacement.y = Input.mousePosition.y;

                GameObject placementSlot = Instantiate(Resources.Load("UI/PlacementSlot") as GameObject);
                placementSlot.GetComponent<PlacementSlot>().InitializePlacementSlot(m_placementObject, 
                    MonsterManager.Instance.GetSelectedMonster().name, monsterPlacement.x, monsterPlacement.y);
                placementSlot.GetComponent<PlacementSlot>().onRemove += RemovePlacementMonster;
                placementSlot.transform.SetParent(m_placementSlots.transform);

                m_monsterPlacement.Add(m_placementObject, monsterPlacement);
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

        m_map = Instantiate(Resources.Load(m_mapPath[mapIndex]) as GameObject);
        m_map.transform.position = new Vector3(0, 0, 0);

        m_episodeText.text = "Episode " + (mapIndex + 1).ToString();
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

    public void SaveData()
    {

    }
}
