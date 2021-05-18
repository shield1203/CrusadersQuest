using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIPrefab
{
    LOADING,
    LOGIN_TYPE,
    ERROR,
    MAP_SAVE,
    SOLDIER_LIST,
    ADVENTURE,
    MONSTER_LIST,
}

public class UIManager : MonoBehaviour
{
    private static UIManager m_instance = null;

    List<GameObject> m_ui = new List<GameObject>();

    List<string> m_uiPrefabPath = new List<string>();

    public static UIManager Instance
    {
        get
        {
            if(!m_instance)
            {
                m_instance = FindObjectOfType(typeof(UIManager)) as UIManager;
            }
            return m_instance;
        }
    }

    private void Awake()
    {
        if(null == m_instance)
        {
            m_instance = this;
            DontDestroyOnLoad(this.gameObject);
            SetUIPrefabPath();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public int GetUICount()
    {
        return m_ui.Count;
    }

    void SetUIPrefabPath()
    {
        m_uiPrefabPath.Add("UI/Loading");
        m_uiPrefabPath.Add("UI/LoginTypeUI");
        m_uiPrefabPath.Add("UI/ErrorUI");
        m_uiPrefabPath.Add("UI/MapSaveUI");
        m_uiPrefabPath.Add("UI/SoldierListUI");
        m_uiPrefabPath.Add("UI/AdventureUI");
        m_uiPrefabPath.Add("UI/MonsterListUI");
    }

    public void AddUI(UIPrefab uiPrefab)
    {
        m_ui.Add(Instantiate(Resources.Load(m_uiPrefabPath[(int)uiPrefab])) as GameObject);
        m_ui[m_ui.Count - 1].GetComponent<Canvas>().sortingOrder = m_ui.Count;
        m_ui[m_ui.Count - 1].transform.SetParent(this.gameObject.transform);
    }

    public void RemoveOneUI()
    {
        if (m_ui.Count > 0)
        {
            Destroy(m_ui[m_ui.Count - 1]);
            m_ui.RemoveAt(m_ui.Count - 1);
        }
    }

    public void RemoveAllUI()
    {
        for(int index = 0; index < m_ui.Count; index++)
        {
            Destroy(m_ui[index]);
        }
        m_ui.Clear();
    }

    public void ActiveUI(bool value)
    {
        for (int index = 0; index < m_ui.Count; index++)
        {
            m_ui[index].SetActive(value);
        }
    }
}
