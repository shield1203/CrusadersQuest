using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeamSelect : MonoBehaviour
{
    private SoldierType m_selectedType = SoldierType.All;

    private HttpSystem m_httpSystem;

    [SerializeField]
    private Text m_title;

    [SerializeField]
    private GameObject[] m_teamSlots;

    [SerializeField]
    private GameObject m_soldierList;

    [SerializeField]
    private GameObject m_orderByButton;

    [SerializeField]
    private GameObject m_orderByDescendingButton;

    private bool m_orderByDescending = true;

    [SerializeField]
    private Image m_blackBoard;

    void Start()
    {
        m_httpSystem = gameObject.GetComponent<HttpSystem>();

        m_title.text = StageManager.Instance.GetTitleText();
        InitializeSoldierTeam();
        InitializeSoldierList();
    }

    public void ExitUI()
    {
        UIManager.Instance.RemoveOneUI();
    }

    void InitializeSoldierTeam()
    {
        List<SoldierData> soldiers = SoldierManager.Instance.GetSoldierTeam();
        for(int index = 0; index < m_teamSlots.Length; index++)
        {
            SoldierData soldierData = new SoldierData();
            if(soldiers.Count > index)
            {
                soldierData = soldiers[index];
            }

            m_teamSlots[index].GetComponent<SoldierTeamSlot>().InitializeSoldierTeamSlot(soldierData, OnUpdateSoldierTeamSlot, soldiers.Count <= index);
        }
    }

    void InitializeSoldierList()
    {
        foreach (Transform child in m_soldierList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        List<SoldierData> soldierList = SoldierManager.Instance.GetSoldierData(m_selectedType);

        if (m_orderByDescending)
        {
            soldierList.Sort((x1, x2) => x1.grade.CompareTo(x2.grade));
        }
        else
        {
            soldierList.Sort((x1, x2) => x2.grade.CompareTo(x1.grade));
        }

        for (int index = 0; index < soldierList.Count; index++)
        {
            GameObject soldierSlot = Instantiate(Resources.Load("UI/SoldierInfoSlot") as GameObject);
            soldierSlot.GetComponent<SoldierInfoSlot>().InitializeSoldierInfoSlot(soldierList[index], OnUpdateSoldierTeamSlot);
            soldierSlot.transform.SetParent(m_soldierList.transform);
        }
    }

    void OnUpdateSoldierTeamSlot(int soldierId, int isTeam)
    {
        StartCoroutine(UpdateSoldierTeamSlot(soldierId, isTeam));
    }

    IEnumerator UpdateSoldierTeamSlot(int soldierId, int isTeam)
    {
        yield return StartCoroutine(m_httpSystem.RequestUpdateTeamData(soldierId, isTeam));

        InitializeSoldierTeam();

        foreach (Transform child in m_soldierList.transform)
        {
            child.gameObject.GetComponent<SoldierInfoSlot>().TeamInclusionCheck();
        }
    }

    public void ToggleOrder()
    {
        m_orderByDescending = !m_orderByDescending;

        m_orderByDescendingButton.SetActive(m_orderByDescending);
        m_orderByButton.SetActive(!m_orderByDescending);

        InitializeSoldierList();
    }

    // Type
    public void SetActiveType_All(bool value)
    {
        if (value)
        {
            m_selectedType = SoldierType.All;
            InitializeSoldierList();
        }
    }

    public void SetActiveType_Warrior(bool value)
    {
        if (value)
        {
            m_selectedType = SoldierType.Warrior;
            InitializeSoldierList();
        }
    }

    public void SetActiveType_Paladin(bool value)
    {
        if (value)
        {
            m_selectedType = SoldierType.Paladin;
            InitializeSoldierList();
        }
    }

    public void SetActiveType_Archer(bool value)
    {
        if (value)
        {
            m_selectedType = SoldierType.Archer;
            InitializeSoldierList();
        }
    }

    public void SetActiveType_Hunter(bool value)
    {
        if (value)
        {
            m_selectedType = SoldierType.Hunter;
            InitializeSoldierList();
        }
    }

    public void SetActiveType_Wizard(bool value)
    {
        if (value)
        {
            m_selectedType = SoldierType.Wizard;
            InitializeSoldierList();
        }
    }

    public void SetActiveType_Priest(bool value)
    {
        if (value)
        {
            m_selectedType = SoldierType.Priest;
            InitializeSoldierList();
        }
    }

    // GameStart
    public void GameStart()
    {
        if (SoldierManager.Instance.GetSoldierTeam().Count != 3) return;
        
        m_blackBoard.gameObject.SetActive(true);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float alphaValue = 0f;

        while(alphaValue < 1)
        {
            yield return null;

            alphaValue += 0.005f;
            m_blackBoard.color = new Color(0, 0, 0, alphaValue);
        }

        SceneManager.LoadScene("Stage");
    }
}
