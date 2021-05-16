using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum MonsterCode
{
    Slime,
    Goblin_Warrior,
    Goblin_Archer,
    Werewolf,
    Ent,
    Ogre,
    Sandman,
    Lizardman,
    Anubis,
    Horus
}

public struct MonsterData
{
    public MonsterCode code;
    public int level;
    public string name;
    public float attack;
    public float attackRange;
    public float speed;
    public float maxHP;

    public string thumbnailPath;
    public string prefabPath;
}

public class MonsterManager : MonoBehaviour
{
    private static MonsterManager m_instance = null;

    private List<MonsterData> m_monsterData = new List<MonsterData>();

    private MonsterData m_selectedMonster;

    public static MonsterManager Instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = FindObjectOfType(typeof(MonsterManager)) as MonsterManager;
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
        string monsterDataJSON = File.ReadAllText(Application.dataPath + "/Resources/JSON/MonsterData.json");
        m_monsterData = JsonHelper.FromJson<MonsterData>(monsterDataJSON);
        m_selectedMonster = m_monsterData[0];
    }

    public List<MonsterData> GetMonsterData()
    {
        return m_monsterData;
    }

    public MonsterData GetMonsterData(MonsterCode code)
    {
        return m_monsterData[(int)code];
    }

    public void SetSelectedMonster(MonsterData data)
    {
        m_selectedMonster = data;
    }

    public MonsterData GetSelectedMonster()
    {
        return m_selectedMonster;
    }
}
