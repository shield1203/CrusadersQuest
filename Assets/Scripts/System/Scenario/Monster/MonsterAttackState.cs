using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : StateMachineBehaviour
{
    MonsterUnit m_unit;
    Transform m_transform;
    GameObject m_mainTarget;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_unit = animator.GetComponent<MonsterUnit>();
        m_transform = animator.GetComponent<Transform>();
        m_mainTarget = m_unit.GetMainTarget();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_mainTarget.GetComponent<UnitBase>().IsDie())
        {
            animator.SetBool("IsInRange", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
