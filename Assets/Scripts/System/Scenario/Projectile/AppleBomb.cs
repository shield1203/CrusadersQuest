using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleBomb : ProjectileBase
{
    private bool m_explosion = false;
    private const float m_heightArc = 2.82f;

    public override void InitializeProjectile(float damage, Vector2 targetPosition = new Vector2())
    {
        base.InitializeProjectile(damage, targetPosition);
        m_speed = 2.8f;
    }

    void Update()
    {
        if (m_explosion) return;

        float x0 = m_startPosition.x;
        float x1 = m_targetPosition.x;
        float distance = x1 - x0;
        float nextX = Mathf.MoveTowards(transform.position.x, x1, m_speed * Time.deltaTime);
        float baseY = Mathf.Lerp(m_startPosition.y, m_targetPosition.y, (nextX - x0) / distance);
        float arc = m_heightArc * (nextX - x0) * (nextX - x1) / (-0.25f * distance * distance);
        Vector2 nextPosition = new Vector2(nextX, baseY + arc);

        Transform effectTransform = gameObject.transform.Find("apple");
        Vector2 forward = (nextPosition -  new Vector2(transform.position.x , transform.position.y));
        effectTransform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);

        transform.position = nextPosition;

        if (nextPosition == m_targetPosition)
        {
            m_explosion = true;
            effectTransform.gameObject.SetActive(false);

            CheckOverlapUnit(UnitType.Soldier, Sound.hit_normal);

            gameObject.GetComponent<EffectSystem>().SpawnEffect(Effect.Explosion, 1);
            SoundSystem.Instance.PlaySound(Sound.explosion);

            Destroy(gameObject, 1);
        }
    }
}
