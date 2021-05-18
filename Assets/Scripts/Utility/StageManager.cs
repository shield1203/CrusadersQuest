using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;

[System.Serializable]
public struct EpisodeData
{
    public int episode;
    public string mapName;
    public StageData[] stage;
}

[System.Serializable]
public struct StageData
{
    public int number;
    public int consumption;
    public string name;
    public SoldierCode boss;
    public int bossLevel;
    public int reward;
}

[System.Serializable]
public struct Stage
{
    public string mapPath;
    public MonsterPlacement[] monsters;
}

[System.Serializable]
public struct MonsterPlacement
{
    public MonsterCode code;
    public float x;
    public float y;
}

public class StageManager : MonoBehaviour
{
    private static StageManager m_instance = null;

    private List<EpisodeData> m_episodeData = new List<EpisodeData>();

    private int m_curEpisode = 1;

    private int m_curStage = 1;

    private List<string> m_mapPath = new List<string>();

    public static StageManager Instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = FindObjectOfType(typeof(StageManager)) as StageManager;
            }
            return m_instance;
        }
    }

    private void Awake()
    {
        if (null == m_instance)
        {
            m_instance = this;
            LoadData();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void LoadData()
    {
        string episodeDataJSON = File.ReadAllText(Application.dataPath + "/Resources/JSON/EpisodeData.json");
        m_episodeData = JsonHelper.FromJson<EpisodeData>(episodeDataJSON);

        m_mapPath.Add("Map/Forest");
        m_mapPath.Add("Map/Desert");
        m_mapPath.Add("Map/DeepSea");
        m_mapPath.Add("Map/Volcano");
    }

    public void SetCurEpisode(int episode)
    {
        m_curEpisode = episode;
    }

    public void SetCurStage(int stage)
    {
        m_curStage = stage;
    }

    public int GetCurEpisode()
    {
        return m_curEpisode;
    }

    public int GetCurStage()
    {
        return m_curStage;
    }

    public string GetEpisodeMapName(int episode)
    {
        return m_episodeData[episode].mapName;
    }

    public StageData[] GetStageData(int episode)
    {
        return m_episodeData[episode - 1].stage;
    }

    public Stage GetStage()
    {
        Stage curStage = new Stage();
        string stageMonsterJSON = File.ReadAllText(Application.dataPath + 
            "/Resources/JSON/Stage" + m_curEpisode.ToString() + "-" + m_curStage.ToString() + ".json");

        curStage = JsonUtility.FromJson<Stage>(stageMonsterJSON);

        return curStage;
    }

    public string GetMapPathString(int mapIndex)
    {
        return m_mapPath[mapIndex];
    }
}
