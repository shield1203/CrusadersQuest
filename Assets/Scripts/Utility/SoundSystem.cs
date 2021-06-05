using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM
{
    Title,
    Lobby,
    Stage,
}

public enum Sound
{
    Button_Touch,
    GameStart,
    Victory,
    LevelUp,
}

public class SoundSystem : MonoBehaviour
{
    private static SoundSystem m_instance = null;

    private AudioSource m_audioSource;

    private List<AudioClip> m_bgm_intro = new List<AudioClip>();
    private List<AudioClip> m_bgm = new List<AudioClip>();

    private List<AudioClip> m_sound = new List<AudioClip>();

    public static SoundSystem Instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = FindObjectOfType(typeof(SoundSystem)) as SoundSystem;
            }
            return m_instance;
        }
    }

    private void Awake()
    {
        if (null == m_instance)
        {
            m_instance = this;
            m_audioSource = gameObject.GetComponent<AudioSource>();
            LoadData();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        this.transform.position = Camera.main.transform.position;
    }

    void LoadData()
    {
        List<string> bgmPath = new List<string>();
        bgmPath.Add("Audio/BGM/title");
        bgmPath.Add("Audio/BGM/lobby_thema");
        bgmPath.Add("Audio/BGM/stage_thema");
        foreach (string path in bgmPath)
        {
            AudioClip introAudioClip = Resources.Load(path + "_intro") as AudioClip;
            m_bgm_intro.Add(introAudioClip);
            AudioClip bgmAudioClip = Resources.Load(path) as AudioClip;
            m_bgm.Add(bgmAudioClip);
        }

        List<string> soundPath = new List<string>();
        soundPath.Add("Audio/button_touch");
        soundPath.Add("Audio/game_start"); 
        soundPath.Add("Audio/victory");
        soundPath.Add("Audio/levelup");
        foreach (string path in soundPath)
        {
            AudioClip audioClip = Resources.Load(path) as AudioClip;
            m_sound.Add(audioClip);
        }
    }

    public void StartBGM(BGM bgm)
    {
        m_audioSource.Stop();

        StartCoroutine(PlayBGM(bgm));
    }

    IEnumerator PlayBGM(BGM bgm)
    {
        m_audioSource.loop = false;
        m_audioSource.clip = m_bgm_intro[(int)bgm];
        m_audioSource.Play();

        yield return new WaitForSeconds(m_bgm_intro[(int)bgm].length);

        m_audioSource.loop = true;
        m_audioSource.clip = m_bgm[(int)bgm];
        m_audioSource.Play();
    }

    public void StopBGM()
    {
        m_audioSource.Stop();
    }

    public void PlaySound(Sound sound)
    {
        AudioSource.PlayClipAtPoint(m_sound[(int)sound], Camera.main.transform.position);
    }
}
