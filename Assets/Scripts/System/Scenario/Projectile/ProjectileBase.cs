using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    Monster = 8,
    Soldier = 10
}

public class ProjectileBase : MonoBehaviour
{
    protected float m_damage;
    protected float m_speed;
    protected Vector2 m_startPosition;
    protected Vector2 m_targetPosition;
    protected BoxCollider2D m_collider;

    public virtual void InitializeProjectile(float damage, Vector2 targetPosition = new Vector2())
    {
        m_damage = damage;
        m_startPosition = gameObject.transform.position;
        m_targetPosition = targetPosition;
        m_collider = GetComponent<BoxCollider2D>();
    }

    protected void MoveProjectile(float direction)
    {
        gameObject.transform.position += new Vector3(direction * m_speed, 0, 0);
    }

    protected void CheckOverlapUnit(UnitType unitType, Sound hitSound)
    {
        Collider2D[] overlapMonsters = Physics2D.OverlapBoxAll(gameObject.transform.position, m_collider.size, 1f, 1 << (int)unitType);
        for (int index = 0; index < overlapMonsters.Length; index++)
        {
            if (!overlapMonsters[index].gameObject.GetComponent<UnitBase>().IsDie())
            {
                overlapMonsters[index].gameObject.GetComponent<UnitBase>().TakeDamage(m_damage, Sound.hit_normal);
            }
        }
    }
}
