using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSystem : MonoBehaviour
{
    HttpSystem m_httpSystem;

    [SerializeField]
    GameObject m_introUI;

    bool m_checkLogin = false;

    private void Awake()
    {
        m_httpSystem = gameObject.GetComponent<HttpSystem>();
    }

    void Start()
    {
        m_httpSystem.OnCheckServerOpen();
    }
  
    void Update()
    {
        if (UIManager.Instance.GetUICount() > 0) return;

        if (!m_checkLogin && m_introUI.GetComponent<Intro_FadeInOut>().IsFinished())
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
            case "Google": SceneManager.LoadScene("Title"); break;
            case "Facebook": SceneManager.LoadScene("Title"); break;
            case "Guest": SceneManager.LoadScene("Title"); break;
            default: UIManager.Instance.AddUI(UIPrefab.LOGIN_TYPE); break;
        }
    }
}
