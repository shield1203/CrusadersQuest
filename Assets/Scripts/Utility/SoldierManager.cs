using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;

public enum SoldierCode
{
    Leon,
    Leon_Expert,
    Leon_Famous,
    Leon_Predestinate,
    Leon_Fate,
    Leon_Shine,
    Joan,
    Joan_Knight,
    Joan_SteelKnight,
    GachaRanger_R,
    GachaRanger_K,
    GachaRanger_Y,
    R_9999,
    R_1,
    GachaRanger_G,
    Louisilla,
    GachaRanger_B,
    Gwihyang,
    Gwihyang_Reaper,
    Gwihyang_Yeomla,
}

public enum SoldierType
{
    Warrior,
    Paladin,
    Archer,
    Hunter,
    Wizard,
    Priest,
    All,
}

[System.Serializable]
public struct SoldierInfo
{
    public int soldier_id;
    public bool leader;
    public SoldierCode code;
    public int level;
    public float exp;
    public bool team;
}

[System.Serializable]
public struct SoldierAbility
{
    public SoldierCode code;
    public SoldierType type;
    public int grade;
    public string name;
    public float startAttackPower;
    public float maxAttackPower;
    public float healthPoint;
    public float fatalHitProbability; // 치명타 확률
    public float physicalDefensePower;
    public float magicalDefensePower;

    public string prefabPath;
    public string spritePath;
}

[System.Serializable]
public struct SoldierData
{
    public int soldier_id;
    public bool leader;
    public SoldierCode code;
    public int level;
    public float exp;
    public bool team;

    public SoldierType type;
    public int grade;
    public string name;
    public float startAttackPower;
    public float maxAttackPower;
    public float healthPoint;
    public float fatalHitProbability; // 치명타 확률
    public float physicalDefensePower;
    public float magicalDefensePower;

    public string prefabPath;
    public string spritePath;
}

public class SoldierManager : MonoBehaviour
{
    private static SoldierManager m_instance = null;

    public Dictionary<SoldierCode, SoldierAbility> m_soldierAbility = new Dictionary<SoldierCode, SoldierAbility>();
    public List<SoldierData> m_soldierData = new List<SoldierData>();
    public List<SoldierData> m_soldierTeam = new List<SoldierData>();

    public static SoldierManager Instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = FindObjectOfType(typeof(SoldierManager)) as SoldierManager;
            }
            return m_instance;
        }
    }

    private void Awake()
    {
        if (null == m_instance)
        {
            m_instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        string soldierAbilityJSON = File.ReadAllText(Application.dataPath + "/Resources/JSON/SoldierAbility.json");

        List<SoldierAbility> soldierAbilities = JsonHelper.FromJson<SoldierAbility>(soldierAbilityJSON);
        foreach(SoldierAbility ability in soldierAbilities)
        {
            m_soldierAbility.Add(ability.code, ability);
        }
    }

    public void InitializeSoldierData(string strSoldierInfo)
    {
        m_soldierData.Clear();
        m_soldierTeam.Clear();

        var soldierInfo = JsonHelper.FromJson<SoldierInfo>(strSoldierInfo);
        
        foreach (var info in soldierInfo)
        {
            SoldierData soldierData = new SoldierData();
            soldierData.soldier_id = info.soldier_id;
            soldierData.leader = info.leader;
            soldierData.code = info.code;
            soldierData.level = info.level;
            soldierData.exp = info.exp;
            soldierData.team = info.team;

            soldierData.name = m_soldierAbility[info.code].name;
            soldierData.type = m_soldierAbility[info.code].type;
            soldierData.grade = m_soldierAbility[info.code].grade;
            soldierData.startAttackPower = m_soldierAbility[info.code].startAttackPower;
            soldierData.maxAttackPower = m_soldierAbility[info.code].maxAttackPower;
            soldierData.healthPoint = m_soldierAbility[info.code].healthPoint;
            soldierData.fatalHitProbability = m_soldierAbility[info.code].fatalHitProbability;
            soldierData.physicalDefensePower = m_soldierAbility[info.code].physicalDefensePower;
            soldierData.magicalDefensePower = m_soldierAbility[info.code].magicalDefensePower;

            soldierData.prefabPath = m_soldierAbility[info.code].prefabPath;
            soldierData.spritePath = m_soldierAbility[info.code].spritePath;

            m_soldierData.Add(soldierData);
            if (soldierData.team) m_soldierTeam.Add(soldierData);
        }
    }

    public List<SoldierData> GetSoldierData(SoldierType type)
    {
        if (type == SoldierType.All) return m_soldierData;

        List<SoldierData> soldierData = new List<SoldierData>();

        foreach(SoldierData soldier in m_soldierData)
        {
            if (soldier.type == type) soldierData.Add(soldier);
        }

        return soldierData;
    }

    public SoldierCode GetLeaderSoldierCode()
    {
        foreach(SoldierData soldierData in m_soldierData)
        {
            if (soldierData.leader) return soldierData.code;
        }

        return SoldierCode.Leon;
    }

    public List<SoldierData> GetSoldierTeam()
    {
        return m_soldierTeam;
    }
}
