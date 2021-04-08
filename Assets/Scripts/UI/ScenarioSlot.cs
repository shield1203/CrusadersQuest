using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioSlot : MonoBehaviour
{
    [SerializeField]
    private Sprite m_defaultBox;

    [SerializeField]
    private Sprite m_selectedBox;

    [SerializeField]
    private Sprite m_bigThumbnail;

    [SerializeField]
    private GameObject m_stateListUI;

    public void SlotSelected(bool value)
    {
        if(value)
        {
            gameObject.GetComponent<Image>().sprite = m_selectedBox;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = m_defaultBox;
        }
    }

    public Sprite GetBigThumbnail()
    {
        return m_bigThumbnail;
    }

    public GameObject GetStageListUI()
    {
        return m_stateListUI;
    }
}
