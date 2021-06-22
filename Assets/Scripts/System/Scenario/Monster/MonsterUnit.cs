using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUnit : UnitBase
{
    MonsterData m_data;

    protected override void Start()
    {
        base.Start();
        
    }

    void Update()
    {

    }

    public void InitializeMonsterUnit(MonsterData data, List<GameObject> targetUnits)
    {
        m_data = data;

        m_curHP = data.maxHP;
        m_maxHP = data.maxHP;

        m_targetUnits = targetUnits;
    }

    public MonsterData GetMonsterData()
    {
        return m_data;
    }

    public override void Attack()
    {
        base.Attack();

        if (m_mainTarget == null) return;

        if (m_projectile != null)
        {
            Transform muzzle = gameObject.transform.Find("muzzle");
            GameObject projectile = Instantiate(m_projectile, muzzle.transform.position, new Quaternion());
            projectile.GetComponent<ProjectileBase>().InitializeProjectile(m_data.attack, m_mainTarget.transform.position);
        }
        else
        {
            SoundSystem.Instance.PlaySound(Sound.normal_attack);

            m_mainTarget.GetComponent<UnitBase>().TakeDamage(m_data.attack, Sound.hit_normal);
        }
    }

    public override void TakeDamage(float damage, Sound sound)
    {
        base.TakeDamage(damage, sound);

        GameObject floatingText = Instantiate(Resources.Load("FloatingText/FloatingText") as GameObject);
        floatingText.transform.position = transform.position;
        floatingText.GetComponent<FloatingText>().InitializeFloatingText(((int)damage).ToString(), TextColor._white);

        gameObject.GetComponent<EffectSystem>().SpawnEffect(Effect.Hit_mon, 0.20f, "body");

        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(8.5f, 4.1f) * 10);
    }
}
