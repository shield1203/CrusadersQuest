using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageList : MonoBehaviour
{
    [SerializeField]
    private HttpSystem m_httpSystem;

    [SerializeField]
    private GameObject m_prevUI;

    [SerializeField]
    private int m_startEpisode;

    [SerializeField]
    private Text m_mapName;

    [SerializeField]
    private Image m_mapThumbnail;

    [SerializeField]
    private GameObject[] m_reward;

    [SerializeField]
    private GameObject m_stageList;

    private List<StageSlot> m_stageSlots = new List<StageSlot>();

    public void InitializeEpisode()
    {
        StageManager.Instance.SetCurEpisode(m_startEpisode);
        StageManager.Instance.SetCurStage(1);

        InitializeStageList();
    }

    public void InitializeStageList()
    {
        m_stageSlots.Clear();
        foreach (Transform child in m_stageList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        int curEpisode = StageManager.Instance.GetCurEpisode();
        foreach (StageData stageData in StageManager.Instance.GetStageData(curEpisode))
        {
            GameObject stageSlot = Instantiate(Resources.Load("UI/StageSlot") as GameObject);
            stageSlot.GetComponent<StageSlot>().InitializeStageSlot(stageData, this);
            stageSlot.transform.SetParent(m_stageList.transform);
            m_stageSlots.Add(stageSlot.GetComponent<StageSlot>());

            if (StageManager.Instance.GetCurStage() == stageData.number) ChangeStage(stageData);
        }
    }

    public void ChangeStageInfoBox(Sprite mapThumbnail, string mapName)
    {
        m_mapThumbnail.sprite = mapThumbnail;
        m_mapName.text = mapName;

        InitializeStageList();
    }

    public void ChangeStage(StageData stageData)
    {
        for(int index = 0; index < m_stageSlots.Count; index++)
        {
            m_stageSlots[index].SlotSelected(index + 1 == stageData.number);
        }

        for (int index = 0; index < m_reward.Length; index++)
        {
            m_reward[index].SetActive(stageData.reward > index);
        }
    }

    public void OnExit()
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            this.gameObject.SetActive(false);
            m_prevUI.SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name == "Result")
        {
            UIManager.Instance.ActiveUI(false);
        }
    }

    public void OnPrepare()
    {
        StartCoroutine(m_httpSystem.RequestSoldierListData(OpenTeamSelectUI));
    }

    void OpenTeamSelectUI()
    {
        UIManager.Instance.AddUI(UIPrefab.TEAM_SELECT);
    }
}
