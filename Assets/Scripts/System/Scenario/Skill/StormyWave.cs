using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormyWave : SkillBase
{
    private const float initXPos = 1.25f;
    private const float initYPos = 1.08f;

    public override void Action()
    {
        string prefabPath = "Skill/Stormy Wave/Stormy Wave";
        GameObject stormyWave = Instantiate(Resources.Load(prefabPath) as GameObject,
            new Vector2(gameObject.transform.position.x + initXPos, gameObject.transform.position.y + initYPos), new Quaternion());
        stormyWave.GetComponent<Wave>().InitializeWave(m_data.percent[m_linkCount - 1], 0.075f);
        Destroy(stormyWave, 2);
    }
}