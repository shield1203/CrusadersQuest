using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSlot : MonoBehaviour
{
    [SerializeField]
    private Sprite m_defaultBox;

    [SerializeField]
    private Sprite m_selectedBox;

    [SerializeField]
    private Text m_number;

    [SerializeField]
    private Text m_name;

    [SerializeField]
    private Text m_consumption;

    private StageData m_stageData;

    private StageList m_stageList;

    public void InitializeStageSlot(StageData stageData, StageList stageList)
    {
        m_stageData = stageData;
        m_stageList = stageList;
        
        SetNumberText(stageData.number);
        SetNameText(stageData.name);
        SetConsumptionText(stageData.consumption);
    }

    public void SlotSelected(bool value)
    {
        if (value)
        {
            gameObject.GetComponent<Image>().sprite = m_selectedBox;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = m_defaultBox;
        }
    }

    void SetNumberText(int number)
    {
        string strNumber = "";
        if (number < 10) strNumber = "0";

        strNumber += number.ToString();
        m_number.text = strNumber;
    }

    void SetNameText(string stageName)
    {
        m_name.text = stageName;
    }

    void SetConsumptionText(int consumption)
    {
        m_consumption.text = consumption.ToString();
    }

    public void OnSelectStage()
    {
        SoundSystem.Instance.PlaySound(Sound.Button_Touch);

        if (StageManager.Instance.GetCurStage() != m_stageData.number)
        {
            StageManager.Instance.SetCurStage(m_stageData.number);
            m_stageList.ChangeStage(m_stageData);
        }
    }
}
