using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallOfTheHolySword : SkillBase
{
    private const float initXPos = 1.5f;
    private const float initYPos = 6.1f;

    public override void Action()
    {
        base.Action();

        string prefabPath = "Skill/Call of the Holy Sword/Call of the Holy Sword_lv" + m_linkCount.ToString();
        GameObject holySword = Instantiate(Resources.Load(prefabPath) as GameObject, new Vector2(gameObject.transform.position.x + initXPos, initYPos), new Quaternion());
        holySword.GetComponent<HolySword>().SetDamage(m_data.percent[m_linkCount - 1]);
    }
}
