using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIPrefab
{
    LOADING,
    LOGIN_TYPE
}

public class UIManager : MonoBehaviour
{
    private static UIManager m_instance = null;

    Stack<GameObject> m_ui = new Stack<GameObject>();

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
    
    void SetUIPrefabPath()
    {
        m_uiPrefabPath.Add("UI/Loading");
        m_uiPrefabPath.Add("UI/LoginTypeUI");
    }

    public void AddUI(int prefabPathIndex)
    {
        m_ui.Push(Instantiate(Resources.Load(m_uiPrefabPath[prefabPathIndex])) as GameObject);
        m_ui.Peek().GetComponent<Canvas>().sortingOrder = m_ui.Count;
    }

    public void RemoveOneUI()
    {
        if(m_ui.Count > 0) Destroy(m_ui.Pop());
    }

    public void RemoveAllUI()
    {
        while(m_ui.Count > 0)
        {
            Destroy(m_ui.Pop());
        }
    }
}
