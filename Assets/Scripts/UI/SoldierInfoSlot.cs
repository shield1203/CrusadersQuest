using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierInfoSlot : MonoBehaviour
{
    private SoldierData m_soldierData;

    private bool m_isSelected = false;

    public delegate void OnButtonAction();
    public OnButtonAction m_buttonAction = null;

    [SerializeField]
    private Sprite m_defaultBox;

    [SerializeField]
    private Sprite m_selectedBox;

    [SerializeField]
    private Text m_levelText;

    [SerializeField]
    private Image m_thumbnail;

    [SerializeField]
    private Image m_skillThumbnail;

    [SerializeField]
    private Image[] m_grade;

    [SerializeField]
    private Image[] m_type;

    public void InitializeSoldierInfoSlot(SoldierData data, OnButtonAction action)
    {
        m_soldierData = data;
        m_isSelected = (data.team == 1);
        
        if(m_buttonAction == null) m_buttonAction = action;

        SetSoldierData();
        SetSelected(m_isSelected);
    }

    void SetSoldierData()
    {
        m_levelText.text = m_soldierData.level.ToString();
        m_thumbnail.sprite = Resources.Load<Sprite>(m_soldierData.spritePath);

        for (int index = 0; index < m_type.Length; index++)
        {
            m_grade[index].gameObject.SetActive(index + 1 > m_soldierData.grade);
        }

        for (int index = 0; index < m_type.Length; index++)
        {
            m_type[index].gameObject.SetActive(index == (int)m_soldierData.type);
        }

        SkillBase skill = new SkillBase();
        skill.InitializeSkillData(m_soldierData.skill);
        m_skillThumbnail.sprite = Resources.Load<Sprite>(skill.GetSkillData().thumbnailPath);
    }

    void SetSelected(bool isSelected)
    {
        m_isSelected = isSelected;
        if (m_isSelected)
        {
            gameObject.GetComponent<Image>().sprite = m_selectedBox;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = m_defaultBox;
        }
    }

    public void TeamInclusionCheck()
    {
        List<SoldierData> SoldierTeam = SoldierManager.Instance.GetSoldierTeam();
        bool isTeam = false;
        foreach (SoldierData soldierData in SoldierTeam)
        {
            if (soldierData.soldier_id == m_soldierData.soldier_id) isTeam = true;
        }

        m_isSelected = isTeam;
        if (m_isSelected)
        {
            gameObject.GetComponent<Image>().sprite = m_selectedBox;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = m_defaultBox;
        }

        SetSelected(isTeam);
    }

    public void ToggleSoldier()
    {
        if (SoldierManager.Instance.GetSoldierTeam().Count > 2 || !m_isSelected) return;

        int isTeam = m_isSelected ? 1 : 0;
        SoldierManager.Instance.UpdateSoldierTeam(m_soldierData.soldier_id, isTeam);

        if (m_buttonAction != null) m_buttonAction();
    }
}
