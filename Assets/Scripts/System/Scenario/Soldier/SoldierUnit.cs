﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierUnit : UnitBase
{
    protected SoldierData m_data;
    //protected SkillBase

    protected bool m_goal = false;
    protected float m_goalPoint;

    void Start()
    {
        
    }

    void Update()
    {
        m_healthGage.fillAmount = GetHPPercent();
    }

    public void InitializeSoldierUnit(SoldierData soldierData, List<GameObject> targetUnits)
    {
        m_data = soldierData;

        //switch((SkillCode)m_data.skill)
        //{
        //    case SkillCode.Call_of_the_Holy_sword: gameObject.AddComponent<Call_Of_The_Holy_Sword>(); break;
        //    case SkillCode.: gameObject.AddComponent<>(); break;
        //}
        gameObject.GetComponent<SkillBase>().InitializeSkillData(m_data.skill);

        m_curHP = soldierData.healthPoint;

        m_targetUnits = targetUnits;
    }

    public SoldierData GetSoldierData()
    {
        return m_data;
    }

    public float GetCurHP()
    {
        return m_curHP;
    }

    public float GetHPPercent()
    {
        return m_curHP / m_data.healthPoint;
    }

    public float GetAttackRange()
    {
        float distance = 100;

        switch(m_data.type)
        {
            case SoldierType.Warrior: distance = 0.55f; break;
            case SoldierType.Paladin: distance = 0.5f; break;
            case SoldierType.Archer: distance = 7.35f; break;
            case SoldierType.Wizard: distance = 7.2f; break;
            case SoldierType.Priest: distance = 6.9f; break;
        }

        return distance;
    }

    public string GetSkillThumbnail()
    {
        return GetComponent<SkillBase>().GetSkillData().thumbnailPath;
    }

    public virtual void ActiveSkill(int linkCount)
    {

    }

    public void SetGoalPoint(float point)
    {
        m_goalPoint = point;
    }

    public float GetGoalPoint()
    {
        return m_goalPoint;
    }

    public void SetGoal(bool value)
    {
        m_goal = value;
    }

    public bool IsGoal()
    {
        return m_goal;
    }
}
