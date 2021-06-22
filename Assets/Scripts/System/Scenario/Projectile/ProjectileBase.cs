using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    protected float m_damage;
    protected Vector2 m_targetPosition;
    protected BoxCollider2D m_collider;

    public virtual void InitializeProjectile(float damage, Vector2 targetPosition = new Vector2())
    {
        m_damage = damage;
        m_targetPosition = targetPosition;
        m_collider = GetComponent<BoxCollider2D>();
    }

    protected void MoveProjectile(float direction, float speed)
    {
        gameObject.transform.position += new Vector3(direction * speed, 0, 0);
    }
}
