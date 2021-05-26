using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public enum SkillCode
{
    Call_of_the_Holy_sword,
    Stormy_Waves,
    Earth_Crush,
    Arrow_Storm,
    Unstable_Shot,
    Soul_Suppressing_Amulet,
    Its_Light_Nyang
}
public struct SkillData
{
    public SkillCode code;
    public string name;
    public float[] percent;
    public string thumbnailPath;
}

public class SkillBase : MonoBehaviour
{
    private SkillData m_data;
    private SoldierData m_soldierData;

    public void InitializeSkillData(SkillCode code, SoldierData data = new SoldierData())
    {
        string skillDataJSON = File.ReadAllText(Application.dataPath + "/Resources/JSON/SkillData.json");

        List<SkillData> skillData = JsonHelper.FromJson<SkillData>(skillDataJSON);

        m_data.code = skillData[(int)code].code;
        m_data.name = skillData[(int)code].name;
        m_data.percent = skillData[(int)code].percent;
        m_data.thumbnailPath = skillData[(int)code].thumbnailPath;

        m_soldierData = data;
    }

    public SkillData GetSkillData()
    {
        return m_data;
    }

    virtual public void Action(int linkCount) { }
}
