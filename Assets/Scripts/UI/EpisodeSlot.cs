using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EpisodeSlot : MonoBehaviour
{
    [SerializeField]
    private int m_episode;

    [SerializeField]
    private GameObject m_stageList;

    [SerializeField]
    private Sprite m_mapThumbnail;

    [SerializeField]
    private GameObject m_defaultBox;

    [SerializeField]
    private GameObject m_selectedBox;

   
    void Update()
    {
        SlotSelected(StageManager.Instance.GetCurEpisode() == m_episode);
    }

    public void SlotSelected(bool value)
    {
        m_selectedBox.SetActive(value);
        m_defaultBox.SetActive(!value);
    }

    public void OnSelectEpisode()
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);

        if (StageManager.Instance.GetCurEpisode() != m_episode)
        {
            StageManager.Instance.SetCurEpisode(m_episode);
            StageManager.Instance.SetCurStage(1);

            m_stageList.GetComponent<StageList>().ChangeStageInfoBox(m_mapThumbnail, StageManager.Instance.GetEpisodeMapName(m_episode));
        }
    }
}
