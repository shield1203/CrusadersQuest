using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierUnit : UnitBase
{
    protected SoldierData m_data;
    protected SkillBase m_skill;

    List<int> m_skillLink = new List<int>();
    bool m_finishedSkill = true;

    protected bool m_goal = false;
    protected float m_goalPoint;

    protected float m_totalDamage = 0;
    protected float m_totalHeal = 0;

    protected override void Start()
    {
        base.Start();

        Transform head = gameObject.transform.Find("body").Find("head");
        for (int index = 0; index < head.childCount; index++)
        {
            head.GetChild(index).GetComponent<SpriteRenderer>().material = m_material;
        }

        StartCoroutine(CheckSkillLink());
    }

    public void InitializeSoldierUnit(SoldierData soldierData, List<GameObject> targetUnits)
    {
        m_data = soldierData;

        m_skill = gameObject.GetComponent<SkillBase>();
        m_skill.InitializeSkillData(m_data.skill);

        m_curHP = soldierData.healthPoint;
        m_maxHP = soldierData.healthPoint;

        m_targetUnits = targetUnits;
    }

    public SoldierData GetSoldierData()
    {
        return m_data;
    }

    public override void Attack()
    {
        base.Attack();

        if (m_mainTarget == null) return;

        float damage = m_data.startAttackPower + ((m_data.maxAttackPower - m_data.startAttackPower) / 10) * (m_data.level % 10);

        if(m_projectile != null)
        {
            Transform muzzle = gameObject.transform.Find("muzzle");
            GameObject projectile = Instantiate(m_projectile, muzzle.transform.position, new Quaternion());
            projectile.GetComponent<ProjectileBase>().InitializeProjectile(damage / 5, m_mainTarget.transform.position);
        }
        else
        {
            SoundSystem.Instance.PlaySound(Sound.normal_attack);

            m_totalDamage += damage;
            m_mainTarget.GetComponent<UnitBase>().TakeDamage(damage, Sound.hit_normal);
        }
    }

    public float GetAttackRange()
    {
        float distance = 100;

        switch(m_data.type)
        {
            case SoldierType.Warrior: distance = 0.55f; break;
            case SoldierType.Paladin: distance = 0.5f; break;
            case SoldierType.Archer: distance = 7.5f; break;
            case SoldierType.Hunter: distance = 6.35f; break;
            case SoldierType.Wizard: distance = 6.8f; break;
            case SoldierType.Priest: distance = 6.9f; break;
        }

        return distance;
    }

    public override void TakeDamage(float damage, Sound sound)
    {
        if (m_die) return;

        base.TakeDamage(damage, sound);

        GameObject floatingText = Instantiate(Resources.Load("FloatingText/FloatingText") as GameObject);
        floatingText.transform.position = transform.position;
        floatingText.GetComponent<FloatingText>().InitializeFloatingText(((int)damage).ToString(), TextColor._black);

        gameObject.GetComponent<EffectSystem>().SpawnEffect(Effect.Hit_sol, 0.20f, "body");

        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10.2f, 5.5f) * 10);

        if (m_die) SoundSystem.Instance.PlaySound(Sound.soldier_die);
    }

    public string GetSkillThumbnail()
    {
        return GetComponent<SkillBase>().GetSkillData().thumbnailPath;
    }

    public void AddSkillLink(int linkCount)
    {
        m_skillLink.Add(linkCount);
    }

    public void FinishedSkill()
    {
        m_finishedSkill = true;
    }

    IEnumerator CheckSkillLink()
    {
        while (m_curHP > 0)
        {
            yield return null;

            if(m_skillLink.Count > 0 && m_finishedSkill)
            {
                if (m_skillLink[0] == 3)
                {
                    GetComponent<EffectSystem>().SpawnEffect(Effect.Link3SkillStart, 0.25f, "body");
                }

                m_animator.SetTrigger("Skill");
                m_skill.SetLinkCount(m_skillLink[0]);
                m_skillLink.RemoveAt(0);
                m_finishedSkill = false;
            }
        }
    }

    public void SetGoalPoint(float point)
    {
        m_goalPoint = point;
    }

    public float GetGoalPoint()
    {
        return m_goalPoint;
    }

    public void SetGoal(bool value)
    {
        m_goal = value;
    }

    public bool IsGoal()
    {
        return m_goal;
    }

    public float GetTotalDamage() 
    {
        return m_totalDamage;
    }

    public float GetTotalHeal()
    {
        return m_totalHeal;
    }
}
