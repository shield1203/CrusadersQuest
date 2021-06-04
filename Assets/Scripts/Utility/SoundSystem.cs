using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM
{
    Title,
    Lobby,
    Episode1,
}

public enum Sound
{
    Button_Touch,
    LevelUp,
}

public class SoundSystem : MonoBehaviour
{
    AudioSource m_audioSource;

    private List<AudioClip> m_bgm_intro = new List<AudioClip>();
    private List<AudioClip> m_bgm = new List<AudioClip>();

    private List<AudioClip> m_sound = new List<AudioClip>();

    void LoadData()
    {
        List<string> bgmPath = new List<string>();
        bgmPath.Add("Audio/BGM/title_thema");
        bgmPath.Add("Audio/BGM/lobby_thema");
        bgmPath.Add("Audio/BGM/ep1_thema");
        foreach (string path in bgmPath)
        {
            AudioClip introAudioClip = Resources.Load(path + "_intro") as AudioClip;
            AudioClip bgmAudioClip = Resources.Load(path) as AudioClip;
            m_sound.Add(bgmAudioClip);
        }

        List<string> soundPath = new List<string>();
        soundPath.Add("Audio/button_touch");
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
