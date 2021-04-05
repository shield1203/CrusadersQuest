﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SoldierList : MonoBehaviour
{
    private SoldierType m_selectedType = SoldierType.All;

    [SerializeField]
    private GameObject m_soldierList;

    [SerializeField]
    private Text[] m_soldierAmount;

    private int m_soldierCount = 0;

    void Start()
    {
        InitializeSoldierList();
        m_soldierAmount[(int)m_selectedType].text = m_soldierCount.ToString() + "/300";
    }

    public void ExitUI()
    {
        UIManager.Instance.RemoveOneUI();
    }

    public void SetActiveType_All(bool value)
    {
        if (value)
        {
            m_selectedType = SoldierType.All;
            InitializeSoldierList();
            m_soldierAmount[(int)m_selectedType].text = m_soldierCount.ToString() + "/300";
        }
    }

    public void SetActiveType_Warrior(bool value)
    {
        if (value)
        {
            m_selectedType = SoldierType.Warrior;
            InitializeSoldierList();
            m_soldierAmount[(int)m_selectedType].text = m_soldierCount.ToString();
        }
    }

    public void SetActiveType_Paladin(bool value)
    {
        if (value)
        {
            m_selectedType = SoldierType.Paladin;
            InitializeSoldierList();
            m_soldierAmount[(int)m_selectedType].text = m_soldierCount.ToString();
        }
    }

    public void SetActiveType_Archer(bool value)
    {
        if (value)
        {
            m_selectedType = SoldierType.Archer;
            InitializeSoldierList();
            m_soldierAmount[(int)m_selectedType].text = m_soldierCount.ToString();
        }
    }

    public void SetActiveType_Hunter(bool value)
    {
        if (value)
        {
            m_selectedType = SoldierType.Hunter;
            InitializeSoldierList();
            m_soldierAmount[(int)m_selectedType].text = m_soldierCount.ToString();
        }
    }

    public void SetActiveType_Wizard(bool value)
    {
        if (value)
        {
            m_selectedType = SoldierType.Wizard;
            InitializeSoldierList();
            m_soldierAmount[(int)m_selectedType].text = m_soldierCount.ToString();
        }
    }

    public void SetActiveType_Priest(bool value)
    {
        if (value)
        {
            m_selectedType = SoldierType.Priest;
            InitializeSoldierList();
            m_soldierAmount[(int)m_selectedType].text = m_soldierCount.ToString();
        }
    }

    void InitializeSoldierList()
    {
        foreach (Transform child in m_soldierList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        List<SoldierData> soldierList = SoldierManager.Instance.GetSoldierData(m_selectedType);
        for(int index = 0; index < soldierList.Count; index++)
        {
            GameObject soldierSlot = Instantiate(Resources.Load("UI/SoldierSlot") as GameObject);
            soldierSlot.GetComponent<SoldierSlot>().SetSoldierData(soldierList[index]);
            soldierSlot.transform.SetParent(m_soldierList.transform);
        }
        m_soldierCount = soldierList.Count;
    }

    
}
