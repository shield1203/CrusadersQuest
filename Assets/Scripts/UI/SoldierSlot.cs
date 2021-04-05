using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SoldierSlot : MonoBehaviour
{
    private int m_slotIndex;

    private SoldierData m_soldierData;

    [SerializeField]
    private GameObject m_levelText;

    [SerializeField]
    private GameObject m_leaderMark;

    [SerializeField]
    private GameObject[] m_grade;

    [SerializeField]
    private GameObject[] m_type;

    [SerializeField]
    private GameObject m_thumbnail;

    public void SetSoldierData(SoldierData soldierData)
    {
        m_soldierData = soldierData;

        InitializeSoldierSolt();
    }

    void InitializeSoldierSolt()
    {
        m_levelText.GetComponent<Text>().text = GetLevelText();
        m_leaderMark.SetActive(m_soldierData.leader);
        SetGrade(m_soldierData.grade);
        SetType(m_soldierData.type);
        m_thumbnail.GetComponent<Image>().sprite = Resources.Load<Sprite>(m_soldierData.spritePath);
    }

    string GetLevelText()
    {
        int maxLevel = m_soldierData.grade * 10;

        return "LV" + m_soldierData.level.ToString() + "/" + maxLevel.ToString();
    }

    void SetGrade(int grade)
    {
        for (int index = 0; index < m_grade.Length; index++)
        {
            m_grade[index].SetActive(grade == (index + 1));
        }
    }

    void SetType(SoldierType type)
    {
        for(int index = 0; index < m_type.Length; index++)
        {
            m_type[index].SetActive((int)type == index);
        }
    }
}
