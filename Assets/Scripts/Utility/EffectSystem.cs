using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Effect
{
    Link3SkillStart,
    Swing_sol,
    Swing_mon,
    Hit_sol,
    Hit_mon,
}

public class EffectSystem : MonoBehaviour
{
    public void SpawnEffect(string effectInfo)
    {
        string[] info = effectInfo.Split('+');

        string prefabPath = "Effect/" + info[0];
        float lifeSecond = info.Length > 1 ? float.Parse(info[1]) : 1f;
        string socketName = info.Length > 2 ? info[2] : "";

        Transform effectTransform = (socketName != "" && gameObject.transform.Find(socketName)) ? gameObject.transform.Find(socketName) : gameObject.transform;
        GameObject effectPrefab = Instantiate(Resources.Load(prefabPath) as GameObject, effectTransform);
        Destroy(effectPrefab, lifeSecond);
    }

    public void SpawnEffect(Effect effect, float lifeSecond = 1f, string socketName = "")
    {
        Transform effectTransform = (socketName != "" && gameObject.transform.Find(socketName)) ? gameObject.transform.Find(socketName) : gameObject.transform;

        GameObject effectPrefab = Instantiate(Resources.Load("Effect/" + effect.ToString()) as GameObject, effectTransform);
        Destroy(effectPrefab, lifeSecond);
    }
}
