using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : ProjectileBase
{
    public override void InitializeProjectile(float damage, Vector2 targetPosition = new Vector2())
    {
        base.InitializeProjectile(damage, targetPosition);
        m_speed = 0.18f;

        Destroy(gameObject, 1.2f);
    }

    private void Start()
    {
        SoundSystem.Instance.PlaySound(Sound.normal_attack);
    }

    void Update()
    {
        MoveProjectile(1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            collision.gameObject.GetComponent<MonsterUnit>().TakeDamage(m_damage, Sound.hit_magic);
            Destroy(gameObject);
        }
    }
}
