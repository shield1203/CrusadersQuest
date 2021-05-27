using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierUnit : UnitBase
{
    protected SoldierData m_data;
    protected SkillBase m_skill;

    protected bool m_goal = false;
    protected float m_goalPoint;

    protected override void Start()
    {
        base.Start();

        Transform head = gameObject.transform.Find("body").Find("head");
        for (int index = 0; index < head.childCount; index++)
        {
            head.GetChild(index).GetComponent<SpriteRenderer>().material = m_material;
        }

    }

    void Update()
    {
        
    }

    public void InitializeSoldierUnit(SoldierData soldierData, List<GameObject> targetUnits)
    {
        m_data = soldierData;

        switch ((SkillCode)m_data.skill)
        {
            case SkillCode.Call_of_the_Holy_sword: gameObject.AddComponent<CallOfTheHolySword>(); break;
            case SkillCode.Stormy_Waves: gameObject.AddComponent<CallOfTheHolySword>(); break;
        }
        m_skill = gameObject.GetComponent<SkillBase>();
        m_skill.InitializeSkillData(m_data.skill);

        m_curHP = soldierData.healthPoint;
        m_maxHP = soldierData.healthPoint;

        m_targetUnits = targetUnits;
    }

    public SoldierData GetSoldierData()
    {
        return m_data;
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
