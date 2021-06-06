using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioList : MonoBehaviour
{
    [SerializeField]
    private GameObject m_prevUI;

    [SerializeField]
    private Image m_ScenarioThumbnail;

    [SerializeField]
    private GameObject[] m_scenarioSlot;

    private int m_selectedEpisode = 0;

    public void OnSelecteEpisode(int episodeIndex)
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);

        if (m_selectedEpisode == episodeIndex) return;

        m_selectedEpisode = episodeIndex;

        m_ScenarioThumbnail.sprite = m_scenarioSlot[episodeIndex].GetComponent<ScenarioSlot>().GetBigThumbnail();
        for (int index = 0; index < m_scenarioSlot.Length; index++)
        {
            m_scenarioSlot[index].GetComponent<ScenarioSlot>().SlotSelected(index == m_selectedEpisode);
        }
    }

    public void OnOpenEpisodeUI()
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);

        this.gameObject.SetActive(false);
        GameObject episodeUI = m_scenarioSlot[m_selectedEpisode].GetComponent<ScenarioSlot>().GetStageListUI();
        episodeUI.SetActive(true);
        episodeUI.GetComponent<StageList>().InitializeEpisode();
    }

    public void OnExit()
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);

        this.gameObject.SetActive(false);
        m_prevUI.SetActive(true);
    }
}
