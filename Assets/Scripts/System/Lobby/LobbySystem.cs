using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbySystem : MonoBehaviour
{
    [SerializeField]
    private HttpSystem m_httpSystem;

    [SerializeField]
    private Text m_level;

    [SerializeField]
    private Image m_exp;

    [SerializeField]
    private Text m_name;

    [SerializeField]
    private Text m_honor;

    [SerializeField]
    private Text m_diamond;

    [SerializeField]
    private Text m_gold;

    [SerializeField]
    private Text m_meat;

    void Start()
    {
        UIManager.Instance.ActiveUI(true);

        StartCoroutine(m_httpSystem.RequestUserData(SetUserData));
    }

    void SetUserData()
    {
        UserData userData = UserDataManager.Instance.GetUserData();

        m_level.text = "LV " + userData.lv.ToString();
        m_exp.fillAmount = userData.exp;
        m_name.text = userData.name;
        m_honor.text = string.Format("{0:#,###}", userData.honor);
        m_diamond.text = string.Format("{0:#,###}", userData.diamond);
        m_gold.text = string.Format("{0:#,###}", userData.gold);
        m_meat.text = string.Format("{0:#,###}", userData.meat);
    }
}
