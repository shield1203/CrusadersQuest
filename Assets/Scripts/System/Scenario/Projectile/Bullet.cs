using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ProjectileBase
{
    public override void InitializeProjectile(float damage, Vector2 targetPosition = new Vector2())
    {
        base.InitializeProjectile(damage, targetPosition);
        m_speed = 0.25f;

        Destroy(gameObject, 0.9f);
    }

    private void Start()
    {
        SoundSystem.Instance.PlaySound(Sound.gun_shot0);
    }

    void Update()
    {
        MoveProjectile(1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster" && !collision.gameObject.GetComponent<UnitBase>().IsDie())
        {
            collision.gameObject.GetComponent<MonsterUnit>().TakeDamage(m_damage, Sound.hit_gun);
            Destroy(gameObject);
        }
    }
}
