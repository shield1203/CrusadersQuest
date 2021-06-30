using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logout : MonoBehaviour
{
    public void OnCancel()
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);
        UIManager.Instance.RemoveOneUI();
    }

    public void OnLogout()
    {
        PlayerPrefs.SetString("LoginType", "None");

        SoundSystem.Instance.PlaySound(Sound.button_touch);
        UIManager.Instance.RemoveOneUI();

        SoundSystem.Instance.StopBGM();
        SceneManager.LoadScene("Intro");
    }
}
