using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSystem : MonoBehaviour
{
    [SerializeField]
    GameObject m_introUI;

    bool m_checkLogin = false;

    void Start()
    {
        // 서버연결 체크
    }
  
    void Update()
    {
        if(!m_checkLogin && m_introUI.GetComponent<Intro_FadeInOut>().IsFinished())
        {
            CheckLoginData();
        }
    }

    void CheckLoginData()
    {
        m_checkLogin = true;

        string strLoginType = PlayerPrefs.GetString("LoginType");
        switch(strLoginType)
        {
            case "Google": break;
            case "Facebook": break;
            case "Guest": break;
            default: UIManager.Instance.AddUI(UIPrefab.LOGIN_TYPE); break;
        }
    }
}
