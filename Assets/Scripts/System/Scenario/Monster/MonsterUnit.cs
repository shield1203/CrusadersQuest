using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUnit : UnitBase
{
    MonsterData m_data = new MonsterData();

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void InitializeMonsterUnit(MonsterData data, List<GameObject> targetUnits)
    {
        m_data = data;
        m_targetUnits = targetUnits;
    }

    public MonsterData GetMonsterData()
    {
        return m_data;
    }
}
