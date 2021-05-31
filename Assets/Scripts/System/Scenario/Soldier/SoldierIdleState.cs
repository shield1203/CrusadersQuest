using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierIdleState : StateMachineBehaviour
{
    SoldierUnit m_unit;
    Transform m_transform;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_unit = animator.GetComponent<SoldierUnit>();
        m_transform = animator.GetComponent<Transform>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_unit.GetTargetUnits() == null || m_unit.IsDie() || m_unit.IsGoal()) return;

        Collider2D[] overlapMonsters = Physics2D.OverlapCircleAll(m_transform.position, m_unit.GetAttackRange(), 1 << 8);
        if (overlapMonsters.Length > 0)
        {
            animator.SetBool("IsInRange", true);

            Random random = new Random();
            int attackType = Random.Range(0, 2);
            animator.SetInteger("AttackType", attackType);
        }
        else if(m_unit.GetTargetUnits().Count > 0 || !m_unit.IsGoal())
        {
            animator.SetBool("IsMove", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
