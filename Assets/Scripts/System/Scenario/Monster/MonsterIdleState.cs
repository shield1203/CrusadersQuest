using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : StateMachineBehaviour
{
    MonsterUnit m_unit;
    Transform m_transform;
    const float detectDistance = 10.2f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_unit = animator.GetComponent<MonsterUnit>();
        m_transform = animator.GetComponent<Transform>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_unit.GetTargetUnits() == null) return;

        if (m_unit.IsDie())
        {
            animator.SetBool("IsDie", true);
            return;
        }

        foreach(GameObject soldier in m_unit.GetTargetUnits())
        {
            if (soldier.GetComponent<UnitBase>().IsDie()) continue;

            float distance = Vector2.Distance(soldier.transform.position, m_transform.position);

            if(distance <= m_unit.GetMonsterData().attackRange)
            {
                animator.SetBool("IsInRange", true); return;
            }
            else if(distance <= detectDistance)
            {
                m_unit.SetMainTarget(soldier);
                animator.SetBool("IsMove", true); return;
            }
        }
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
