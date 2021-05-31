using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttackState : StateMachineBehaviour
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
        if (m_unit.GetTargetUnits() == null || m_unit.IsDie()) return;

        Collider2D[] overlapMonsters = Physics2D.OverlapCircleAll(m_transform.position, m_unit.GetAttackRange(), 1 << 8);
        m_unit.SetMainTarget(null);
        foreach(Collider2D monster in overlapMonsters)
        {
            if(!monster.gameObject.GetComponent<UnitBase>().IsDie())
            {
                m_unit.SetMainTarget(monster.gameObject);
                break;
            }
        }

        if (m_unit.GetMainTarget() != null)
        {
            int attackType = Random.Range(0, 2);
            animator.SetInteger("AttackType", attackType);
        }
        else
        {
            animator.SetBool("IsInRange", false);
            animator.SetBool("IsMove", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
