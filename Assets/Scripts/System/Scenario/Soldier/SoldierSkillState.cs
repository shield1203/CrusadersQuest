using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSkillState : StateMachineBehaviour
{
    SoldierUnit m_unit;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_unit = animator.GetComponent<SoldierUnit>();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_unit.FinishedSkill();

        animator.SetBool("IsInRange", false);
        animator.SetBool("IsMove", false);
    }
}
