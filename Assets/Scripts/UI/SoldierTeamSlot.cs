using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierTeamSlot : MonoBehaviour
{
    private SoldierData m_soldierData;

    private bool m_isEmpty = true;

    public delegate void OnButtonAction(int soldierId, int isTeam);
    public OnButtonAction m_buttonAction = null;

    [SerializeField]
    private GameObject m_contents;

    [SerializeField]
    private Text m_grade;

    [SerializeField]
    private Image[] m_type;

    [SerializeField]
    private Image m_thumbnail;

    public void InitializeSoldierTeamSlot(SoldierData data, OnButtonAction action, bool isEmpty = true)
    {
        m_soldierData = data;
        m_isEmpty = isEmpty;

        if (m_buttonAction == null) m_buttonAction = action;

        SetContentVisible();
        SetSoldierData();
    }

    void SetContentVisible()
    {
        m_contents.SetActive(!m_isEmpty);
    }

    void SetSoldierData()
    {
        if (m_isEmpty) return;

        m_grade.text = m_soldierData.grade.ToString();
        m_thumbnail.sprite = Resources.Load<Sprite>(m_soldierData.spritePath);

        for (int index = 0; index < m_type.Length; index++)
        {
            m_type[index].gameObject.SetActive(index == (int)m_soldierData.type);
        }
    }

    public void RemoveFromTeam()
    {
        SoundSystem.Instance.PlaySound(Sound.Button_Touch);

        if (m_isEmpty) return;

        if (m_buttonAction != null) m_buttonAction(m_soldierData.soldier_id, 0);
    }
}
