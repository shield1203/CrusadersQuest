using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSystem : MonoBehaviour
{
    const float unitInitXPos = -5.71f;
    const float unitInitYPos = -2.35f;
    const float unitDistance = 2.68f;

    HttpSystem m_httpSystem;

    UserData m_prevUserData;
    List<SoldierData> m_prevSoldierTeam;

    [SerializeField]
    Text m_stageName;

    [SerializeField]
    Text m_userLevel;

    [SerializeField]
    Image m_userExpGage;

    [SerializeField]
    Text m_userExp;

    [SerializeField]
    Text[] m_soldierName;

    [SerializeField]
    Text[] m_soldierLevel;

    [SerializeField]
    Image[] m_soldierExpGage;

    [SerializeField]
    Text[] m_soldierExp;

    private void Awake()
    {
        m_httpSystem = gameObject.GetComponent<HttpSystem>();
    }

    void Start()
    {
        StageData stageData = StageManager.Instance.GetCurStageData();
        m_stageName.text = stageData.name;

        m_prevUserData = UserDataManager.Instance.GetUserData();
        SetUserExpData();

        m_prevSoldierTeam = SoldierManager.Instance.GetSoldierTeam();
        for (int index = 0; index < m_prevSoldierTeam.Count; index++)
        {
            GameObject soldierUnit = Instantiate(Resources.Load(m_prevSoldierTeam[index].prefabPath) as GameObject);
            soldierUnit.transform.position = new Vector2(unitInitXPos + (index * unitDistance), unitInitYPos);

            m_soldierName[index].text = m_prevSoldierTeam[index].name;
        }

        if (StageManager.Instance.IsClear())
        {
            StartCoroutine(m_httpSystem.RequestUpdateUserExp(stageData.userExp));
            StartCoroutine(ProgressUserExpBar());

            // 용사 유닛 경험치 
        }
    }
    
    void Update()
    {
        
    }

    void SetUserExpData()
    {
        m_userLevel.text = "LV" + m_prevUserData.lv.ToString();
        m_userExpGage.fillAmount = m_prevUserData.exp;
        float curExp = m_prevUserData.exp * UserDataManager.Instance.GetMaxExp(m_prevUserData.lv);
        m_userExp.text = ((int)curExp).ToString() + "/" + ((int)UserDataManager.Instance.GetMaxExp(m_prevUserData.lv)).ToString();
    }

    IEnumerator ProgressUserExpBar()
    {
        while(true)
        {
            yield return null;

            UserData userData = UserDataManager.Instance.GetUserData();
            if (m_prevUserData.lv == userData.lv && m_prevUserData.exp < userData.exp)
            {
                m_prevUserData.exp += 0.01f;
            }
            else if (m_prevUserData.lv < userData.lv)
            {
                m_prevUserData.exp += 0.01f;

                if (m_prevUserData.exp >= UserDataManager.Instance.GetMaxExp(m_prevUserData.lv))
                {
                    m_prevUserData.lv++;
                    m_prevUserData.exp = 0;
                }
            }

            SetUserExpData();
        }
    }

    public void StageSelect()
    {

    }

    public void ReStart()
    {
        SceneManager.LoadScene("Stage");
    }

    public void NextStage()
    {

    }

    public void BackLobby()
    {
        UIManager.Instance.RemoveAllUI();
        SceneManager.LoadScene("Lobby");
    }
}
