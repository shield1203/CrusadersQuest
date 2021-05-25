using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    protected bool m_die = false;

    protected Animation m_animation;

    protected List<GameObject> m_targetUnits;

    protected GameObject m_mainTarget;

    protected float m_curHP;
    

    void Start()
    {
        m_animation = GetComponent<Animation>();
    }

    public virtual void Attack() { }

    public virtual void TakeDamage(float damage) 
    {
        m_curHP -= damage;
    }

    public bool IsDie()
    {
        return m_die;
    }

    public void SetTargetUnits(List<GameObject> targetUnits)
    {
        m_targetUnits = targetUnits;
    }

    public List<GameObject> GetTargetUnits()
    {
        return m_targetUnits;
    }

    public void SetMainTarget(GameObject target)
    {
        m_mainTarget = target;
    }

    public GameObject GetMainTarget()
    {
        return m_mainTarget;
    }
}
