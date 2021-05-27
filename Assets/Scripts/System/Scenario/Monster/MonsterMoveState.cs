using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveState : StateMachineBehaviour
{
    MonsterUnit m_unit;
    Transform m_transform;
    List<GameObject> m_soldierUnits;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_unit = animator.GetComponent<MonsterUnit>();
        m_transform = animator.GetComponent<Transform>();
        m_soldierUnits = m_unit.GetTargetUnits();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int nearestSoldierIndex = -1;
        float unitMaxXPos = -1;

        for(int index = 0; index < m_soldierUnits.Count; index++)
        {
            if (m_soldierUnits[index].GetComponent<UnitBase>().IsDie()) continue;

            if(unitMaxXPos < m_soldierUnits[index].transform.position.x)
            {
                nearestSoldierIndex = index;
                unitMaxXPos = m_soldierUnits[index].transform.position.x;
            }
        }
        
        if(nearestSoldierIndex == -1)
        {
            animator.SetBool("IsMove", false); return;
        }

        m_transform.position = Vector2.MoveTowards(m_transform.position, m_soldierUnits[nearestSoldierIndex].transform.position, 
            m_unit.GetMonsterData().speed);

        if(Vector2.Distance(m_transform.position, m_soldierUnits[nearestSoldierIndex].transform.position) <= m_unit.GetMonsterData().attackRange)
        {
            m_unit.SetMainTarget(m_soldierUnits[nearestSoldierIndex]);
            animator.SetBool("IsInRange", true);
            animator.SetBool("IsMove", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
