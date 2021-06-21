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
        UIManager.Instance.RemoveOneUI();

        StageData stageData = StageManager.Instance.GetCurStageData();
        m_stageName.text = stageData.name;

        m_prevUserData = UserDataManager.Instance.GetUserData();
        SetUserExpData();

        m_prevSoldierTeam = SoldierManager.Instance.GetSoldierTeam();
        for (int index = 0; index < m_prevSoldierTeam.Count; index++)
        {
            GameObject soldierUnit = Instantiate(Resources.Load(m_prevSoldierTeam[index].prefabPath) as GameObject);
            soldierUnit.transform.position = new Vector2(unitInitXPos + (index * unitDistance), unitInitYPos);
            soldierUnit.GetComponent<SoldierUnit>().m_healthBar.GetComponent<Canvas>().enabled = false;

            SetSoldierExpData(index);
        }

        if (StageManager.Instance.IsClear()) StartCoroutine(UpdateGameResult(stageData));
    }

    IEnumerator UpdateGameResult(StageData stageData)
    {
        yield return StartCoroutine(m_httpSystem.RequestUpdateUserExp(stageData.userExp, UpdateUserExp));

        for (int index = 0; index < m_prevSoldierTeam.Count; index++)
        {
            yield return StartCoroutine(m_httpSystem.RequestUpdateSoldierExp(m_prevSoldierTeam[index], stageData.soldierExp, null));
        }

        yield return StartCoroutine(m_httpSystem.RequestSoldierListData(UpdateSoldierExp));
    }

    // UserExp
    void SetUserExpData()
    {
        m_userLevel.text = "LV" + m_prevUserData.lv.ToString();
        m_userExpGage.fillAmount = m_prevUserData.exp;
        float curExp = m_prevUserData.exp * UserDataManager.Instance.GetMaxExp(m_prevUserData.lv);
        m_userExp.text = ((int)curExp).ToString() + "/" + ((int)UserDataManager.Instance.GetMaxExp(m_prevUserData.lv)).ToString();
    }

    void UpdateUserExp()
    {
        StartCoroutine(ProgressUserExpBar(UserDataManager.Instance.GetUserData()));
    }

    IEnumerator ProgressUserExpBar(UserData userData)
    {
        bool progress = userData.lv >= m_prevUserData.lv;
        if(progress && userData.lv == m_prevUserData.lv && userData.exp <= m_prevUserData.exp)
        {
            progress = false;
        }

        while (progress)
        {
            yield return null;

            if (m_prevUserData.lv == userData.lv && m_prevUserData.exp < userData.exp)
            {
                m_prevUserData.exp += 0.01f;

                if (m_prevUserData.exp >= userData.exp) progress = false;
            }
            else if (m_prevUserData.lv < userData.lv)
            {
                m_prevUserData.exp += 0.01f;

                float curExp = m_prevUserData.exp * UserDataManager.Instance.GetMaxExp(m_prevUserData.lv);
                if (curExp >= UserDataManager.Instance.GetMaxExp(m_prevUserData.lv))
                {
                    SoundSystem.Instance.PlaySound(Sound.levelup);

                    m_prevUserData.lv++;
                    m_prevUserData.exp = 0;
                }
            }

            SetUserExpData();
        }
    }

    // UnitExp
    void SetSoldierExpData(int index)
    {
        m_soldierName[index].text = m_prevSoldierTeam[index].name;
        m_soldierLevel[index].text ="LV" + m_prevSoldierTeam[index].level.ToString() + "/" + ((int)(m_prevSoldierTeam[index].grade * 10)).ToString();
        m_soldierExpGage[index].fillAmount = m_prevSoldierTeam[index].exp;
        float curExp = m_prevSoldierTeam[index].exp * SoldierManager.Instance.GetMaxExp(m_prevSoldierTeam[index].level);
        m_soldierExp[index].text = ((int)curExp).ToString() + "/" + ((int)SoldierManager.Instance.GetMaxExp(m_prevSoldierTeam[index].level)).ToString();
    }

    void UpdateSoldierExp()
    {
        for(int index = 0; index < m_prevSoldierTeam.Count; index++)
        {
            StartCoroutine(ProgressSoldierExpBar(index));
        }
    }

    IEnumerator ProgressSoldierExpBar(int index)
    {
        SoldierData soldier = SoldierManager.Instance.GetSoldierTeam()[index];
        bool progress = true;

        while (progress)
        {
            yield return null;

            SoldierData soldierData = m_prevSoldierTeam[index];
            if (m_prevSoldierTeam[index].level == soldier.level && m_prevSoldierTeam[index].exp < soldier.exp)
            {
                soldierData.exp += 0.01f;

                if (soldierData.exp >= soldier.exp) progress = false;
            }
            else if (m_prevSoldierTeam[index].level < soldier.level)
            {
                soldierData.exp += 0.01f;

                float curExp = soldierData.exp * SoldierManager.Instance.GetMaxExp(soldierData.level);
                if (curExp >= SoldierManager.Instance.GetMaxExp(soldierData.level))
                {
                    SoundSystem.Instance.PlaySound(Sound.levelup);

                    soldierData.level++;
                    soldierData.exp = 0;
                }
            }

            m_prevSoldierTeam[index] = soldierData;
            SetSoldierExpData(index);
        }
    }

    public void StageSelect()
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);
        UIManager.Instance.ActiveLastUI(true);
    }

    public void TeamSelect()
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);
        UIManager.Instance.AddUI(UIPrefab.TEAM_SELECT);
    }

    public void ReStart()
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);
        SceneManager.LoadScene("Stage");
    }

    public void NextStage()
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);

        if (StageManager.Instance.GetCurStage() < 6)
        {
            StageManager.Instance.SetCurStage(StageManager.Instance.GetCurStage() + 1);
            SceneManager.LoadScene("Stage");
        }
    }

    public void BackLobby()
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);

        UIManager.Instance.RemoveAllUI();
        SceneManager.LoadScene("Lobby");
    }
}
