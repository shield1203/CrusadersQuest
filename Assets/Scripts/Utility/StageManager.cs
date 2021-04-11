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

public class StageManager : MonoBehaviour
{
    private static StageManager m_instance = null;

    private List<EpisodeData> m_episodeData = new List<EpisodeData>();

    private int m_curEpisode;

    private int m_curStage;

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
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        string episodeDataJSON = File.ReadAllText(Application.dataPath + "/Resources/JSON/EpisodeData.json");
        m_episodeData = JsonHelper.FromJson<EpisodeData>(episodeDataJSON);
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
}
