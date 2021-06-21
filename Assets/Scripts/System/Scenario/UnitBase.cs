using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitBase : MonoBehaviour
{
    protected bool m_die = false;

    protected Animator m_animator;

    protected List<GameObject> m_targetUnits;
    protected GameObject m_mainTarget;

    public GameObject m_healthBar;
    public Image m_healthGage;
    protected float m_curHP = 1.0f;
    protected float m_maxHP;

    public Material m_material;
    const float maxIntencity = 1.76f;

    public GameObject m_projectile;

    protected virtual void Start()
    {
        m_animator = GetComponent<Animator>();

        m_material = new Material(m_material);
        foreach (SpriteRenderer unitSprite in GetComponentsInChildren<SpriteRenderer>())
        {
            unitSprite.material = m_material;
        }

        StartCoroutine(SetCurHealthPoint());
        StartCoroutine(ChangeDamageColor());
    }

    public virtual void Attack() { }

    public virtual void TakeDamage(float damage, Sound hit) 
    {
        if (m_die) return;

        m_curHP = Mathf.Clamp(m_curHP - damage, 0, m_maxHP);
        if (m_curHP == 0)
        {
            m_die = true;
            m_animator.SetTrigger("Die");
        }
        else
        {
            SoundSystem.Instance.PlaySound(hit);

            m_material.SetFloat("_Intensity", maxIntencity);
        }
    }

    public bool IsDie()
    {
        return m_die;
    }

    // HP, Damage
    public float GetCurHP()
    {
        return m_curHP;
    }

    public float GetHPPercent()
    {
        return m_curHP / m_maxHP;
    }

    IEnumerator SetCurHealthPoint()
    {
        while (m_curHP > 0)
        {
            yield return null;

            m_healthGage.fillAmount = GetHPPercent();
        }

        m_healthGage.fillAmount = GetHPPercent();
    }

    IEnumerator ChangeDamageColor()
    {
        while (m_curHP > 0)
        {
            yield return null;

            if(m_material.GetFloat("_Intensity") > 1.0f)
            {
                m_material.SetFloat("_Intensity", Mathf.Clamp(m_material.GetFloat("_Intensity") - 0.08f, 1.0f, maxIntencity));
            }
        }
    }

    // AI
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
