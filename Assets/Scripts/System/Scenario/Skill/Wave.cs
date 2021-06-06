using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private float m_damage;
    private float m_speed = 0.075f;
    private BoxCollider2D m_collider;

    public void InitializeWave(float damage, float speed)
    {
        m_damage = damage;
        m_speed = speed;
    }

    void Start()
    {
        m_collider = GetComponent<BoxCollider2D>();

        SoundSystem.Instance.PlaySound(Sound.swordwave);
    }

    void Update()
    {
        gameObject.transform.position += new Vector3(m_speed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            collision.gameObject.GetComponent<MonsterUnit>().TakeDamage(m_damage, Sound.hit_normal);
        }
    }
}
