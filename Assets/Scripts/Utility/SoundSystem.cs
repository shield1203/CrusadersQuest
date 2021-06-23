using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM
{
    title,
    lobby_thema,
    stage_thema,
}

public enum Sound
{
    button_touch,
    menu_goddess,
    menu_message,
    menu_party,
    menu_quest,
    menu_shop,
    menu_skillrune,
    menu_weapon,
    game_start,
    victory,
    levelup,
    normal_attack,
    hit_gun,
    hit_magic,
    hit_normal,
    hit_splash,
    justice,
    swordwave,
    coin,
    soldier_die,
    explosion,
    gun_shot0,
    gun_shot1,
    arrow_shot,
    arrow_rain_start,
    arrow_rain,
}

public class SoundSystem : MonoBehaviour
{
    private static SoundSystem m_instance = null;

    private AudioSource m_audioSource;

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

    public void StartBGM(BGM bgm, bool intro = true)
    {
        m_audioSource.Stop();

        StartCoroutine(PlayBGM(bgm, intro));
    }

    IEnumerator PlayBGM(BGM bgm, bool intro)
    {
        AudioClip bgmAudioClip;

        if (intro)
        {
            bgmAudioClip = Resources.Load("Audio/BGM/" + bgm.ToString() + "_intro") as AudioClip;

            m_audioSource.loop = false;
            m_audioSource.clip = bgmAudioClip;
            m_audioSource.Play();

            yield return new WaitForSeconds(bgmAudioClip.length);
        }

        bgmAudioClip = Resources.Load("Audio/BGM/" + bgm.ToString()) as AudioClip;

        m_audioSource.loop = true;
        m_audioSource.clip = bgmAudioClip;
        m_audioSource.Play();
    }

    public void StopBGM()
    {
        m_audioSource.Stop();
    }

    public void PlaySound(Sound sound)
    {
        AudioClip audioClip = Resources.Load("Audio/" + sound.ToString()) as AudioClip;
        AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
    }
}
