using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureList : MonoBehaviour
{
    [SerializeField]
    private GameObject m_scenarioUI;

    public void OnExit()
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);
        UIManager.Instance.RemoveOneUI();
    }

    public void OnOpenScenarioUI()
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);

        this.gameObject.SetActive(false);
        m_scenarioUI.SetActive(true);
    }

    public void OnOpenApocryphaUI()
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);
    }

    public void OnOpenPracticeUI()
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);
    }
}
