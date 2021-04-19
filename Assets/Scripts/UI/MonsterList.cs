using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterList : MonoBehaviour
{
    private MonsterCode m_curMonster;

    [SerializeField]
    private GameObject m_monsterList;

    private List<MonsterSlot> m_monsterSlots = new List<MonsterSlot>();

    void Start()
    {
        InitializeMonsterList();
    }

    void Update()
    {
        if(m_curMonster != MonsterManager.Instance.GetSelectedMonster().code)
        {
            m_curMonster = MonsterManager.Instance.GetSelectedMonster().code;
            for (int index = 0; index < m_monsterSlots.Count; index++)
            {
                m_monsterSlots[index].SlotSelected(m_curMonster);
            }
        }
    }

    void InitializeMonsterList()
    {
        foreach (MonsterData monsterData in MonsterManager.Instance.GetMonsterData())
        {
            GameObject monsterSlot = Instantiate(Resources.Load("UI/MonsterSlot") as GameObject);
            monsterSlot.GetComponent<MonsterSlot>().InitializeMonsterSlot(monsterData);
            monsterSlot.transform.SetParent(m_monsterList.transform);

            m_monsterSlots.Add(monsterSlot.GetComponent<MonsterSlot>());
        }

        m_monsterSlots[0].SlotSelected(m_curMonster);
    }
}
