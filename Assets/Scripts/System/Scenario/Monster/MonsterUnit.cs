﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUnit : UnitBase
{
    MonsterData m_data;

    protected override void Start()
    {
        base.Start();
        
    }

    void Update()
    {

    }

    public void InitializeMonsterUnit(MonsterData data, List<GameObject> targetUnits)
    {
        m_data = data;

        m_curHP = data.maxHP;
        m_maxHP = data.maxHP;

        m_targetUnits = targetUnits;
    }

    public MonsterData GetMonsterData()
    {
        return m_data;
    }
}
