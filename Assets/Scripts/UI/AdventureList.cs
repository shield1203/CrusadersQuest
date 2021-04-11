using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureList : MonoBehaviour
{
    [SerializeField]
    private GameObject m_scenarioUI;

    public void OnExit()
    {
        UIManager.Instance.RemoveOneUI();
    }

    public void OnOpenScenarioUI()
    {
        this.gameObject.SetActive(false);
        m_scenarioUI.SetActive(true);
    }
}
