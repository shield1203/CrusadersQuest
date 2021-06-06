using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolySword : MonoBehaviour
{
    private const float floorPos = -2.46f;

    private float m_damage;
    private BoxCollider2D m_collider;
    private Animator m_animation;

    public void SetDamage(float damage)
    {
        m_damage = damage;
    }

    void Start()
    {
        SoundSystem.Instance.PlaySound(Sound.justice);

        m_collider = GetComponent<BoxCollider2D>();
        m_animation = GetComponent<Animator>();

        StartCoroutine(FallingSword());
    }

    IEnumerator FallingSword()
    {
        while(transform.position.y > floorPos)
        {
            yield return null;

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, floorPos), 1.1f);
        }

        m_animation.SetTrigger("SkillActive");
        CheckOverlapMonster();

        SoundSystem.Instance.PlaySound(Sound.hit_splash);
    }

    public void CheckOverlapMonster()
    {
        Collider2D[] overlapMonsters = Physics2D.OverlapBoxAll(gameObject.transform.position, m_collider.size, 1f, 1 << 8);
        for(int index = 0; index < overlapMonsters.Length; index++)
        {
            if(!overlapMonsters[index].gameObject.GetComponent<UnitBase>().IsDie())
            {
                overlapMonsters[index].gameObject.GetComponent<UnitBase>().TakeDamage(m_damage, Sound.hit_normal);
            }
        }
    }

    public void RemoveSkillObject()
    {
        Destroy(gameObject);
    }
}
