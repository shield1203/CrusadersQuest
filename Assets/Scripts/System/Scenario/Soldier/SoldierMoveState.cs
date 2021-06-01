using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMoveState : StateMachineBehaviour
{
    SoldierUnit m_unit;
    Transform m_transform;

    const float soldierSpeed = 0.039f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_unit = animator.GetComponent<SoldierUnit>();
        m_transform = animator.GetComponent<Transform>();

        animator.SetBool("IsInRange", false);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_unit.GetTargetUnits() == null || m_unit.IsDie()) return;

        Collider2D[] overlapMonsters = Physics2D.OverlapCircleAll(m_transform.position, m_unit.GetAttackRange(), 1 << 8);
        GameObject target= null;
        for(int index = 0; index < overlapMonsters.Length; index++)
        {
            if(!overlapMonsters[index].gameObject.GetComponent<UnitBase>().IsDie())
            {
                target = overlapMonsters[index].gameObject;
                break;
            }
        }

        bool stageClear = true;
        for (int index = 0; index < m_unit.GetTargetUnits().Count; index++)
        {
            if(!m_unit.GetTargetUnits()[index].GetComponent<UnitBase>().IsDie())
            {
                stageClear = false; 
                break;
            }
        }

        if (target != null)
        {
            m_unit.SetMainTarget(target);

            animator.SetBool("IsInRange", true);
            animator.SetBool("IsMove", false);

            Random random = new Random();
            int attackType = Random.Range(0, 2);
            animator.SetInteger("AttackType", attackType);
        }
        else if(stageClear)
        {
            Vector2 goalPoint = new Vector2(m_unit.GetGoalPoint(), m_transform.position.y);
            m_transform.position = Vector2.MoveTowards(m_transform.position, goalPoint, soldierSpeed);

            if(m_transform.position.x == m_unit.GetGoalPoint())
            {
                animator.SetBool("IsMove", false);
                m_unit.SetGoal(true);
            }
        }
        else
        {
            m_transform.position += new Vector3(soldierSpeed, 0, 0);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
