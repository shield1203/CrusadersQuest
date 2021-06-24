using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableShot : SkillBase
{
    public override void Action()
    {
        string prefabPath = "Projectile/Bullet";
        Vector2 muzzle = gameObject.transform.Find("body").Find("right_a").Find("right_h")
            .Find("weapon").Find("muzzle").position;
        GameObject bullet = Instantiate(Resources.Load(prefabPath) as GameObject, muzzle, new Quaternion());
        bullet.GetComponent<ProjectileBase>().InitializeProjectile(m_data.percent[m_linkCount - 1]);
    }
}