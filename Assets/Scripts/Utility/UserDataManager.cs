using System.Collections;
using System.Collections.Generic;
using System.IO;
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

public struct ExpData
{
    public int level;
    public float maxExp;
}

public class UserDataManager : MonoBehaviour
{
    private static UserDataManager m_instance = null;

    private Dictionary<int, float> m_userExp = new Dictionary<int, float>();

    private UserData m_userData = new UserData();
    private float m_curExp = 0;

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
            LoadData();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void LoadData()
    {
        string userExpJSON = File.ReadAllText(Application.dataPath + "/Resources/JSON/UserExpData.json");

        List<ExpData> userExp = JsonHelper.FromJson<ExpData>(userExpJSON);
        foreach (ExpData expData in userExp)
        {
            m_userExp.Add(expData.level, expData.maxExp);
        }
    }

    public void InitializeUserData(string strUserData)
    {
        var userData = JsonHelper.FromJson<UserData>(strUserData);

        foreach(UserData data in userData)
        {
            m_userData = data;
        }

        m_curExp = m_userData.exp * GetMaxExp(m_userData.lv);
    }

    public void InitializeUserData(UserData userData)
    {
        m_userData = userData;
    }

    public UserData GetUserData()
    {
        return m_userData;
    }

    public float GetCurExp()
    {
        return m_curExp;
    }

    public float GetMaxExp(float level)
    {
        return m_userExp[m_userData.lv];
    }

    public UserData AddUserExp(float exp)
    {
        if (m_userData.lv >= m_userExp.Count) return m_userData;

        UserData addResult = m_userData;
        float curExp = m_curExp;
        curExp += exp;

        while(curExp >= GetMaxExp(addResult.lv))
        {
            curExp -= GetMaxExp(addResult.lv);
            addResult.lv++;

            if (addResult.lv >= m_userExp.Count)
            {
                curExp = 0;
                addResult.exp = 0;
                break;
            }
        }

        addResult.exp = curExp / GetMaxExp(addResult.lv);

        return addResult;
    }
}
