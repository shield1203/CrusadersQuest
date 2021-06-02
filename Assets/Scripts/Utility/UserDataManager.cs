using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct UserData
{
    public int lv;
    public float exp;
    public string name;
    public int honor;
    public int diamond;
    public int gold;
    public int meat;
}

public class UserDataManager : MonoBehaviour
{
    private static UserDataManager m_instance = null;

    private UserData m_userData = new UserData();

    public static UserDataManager Instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = FindObjectOfType(typeof(UserDataManager)) as UserDataManager;
            }
            return m_instance;
        }
    }

    private void Awake()
    {
        if (null == m_instance)
        {
            m_instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void InitializeUserData(string strUserData)
    {
        var userData = JsonHelper.FromJson<UserData>(strUserData);

        foreach(UserData data in userData)
        {
            m_userData = data;
        }
    }

    public UserData GetUserData()
    {
        return m_userData;
    }
}
