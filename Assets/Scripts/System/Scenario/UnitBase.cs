using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitBase : MonoBehaviour
{
    protected bool m_die = false;

    protected Animation m_animation;

    protected List<GameObject> m_targetUnits;

    protected GameObject m_mainTarget;

    public GameObject m_healthBar;
    public Image m_healthGage;
    protected float m_curHP;

    //IEnumerator OnChangeDamageColor;

    void Start()
    {
        m_animation = GetComponent<Animation>();
    }

    public virtual void Attack() { }

    public virtual void TakeDamage(float damage) 
    {
        m_curHP -= damage;

        //OnChangeDamageColor.
    }

    IEnumerator ChangeDamageColor()
    {
        yield return null;
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

    public void DisableHPBar()
    {
        m_healthBar.SetActive(false);
    }
}
