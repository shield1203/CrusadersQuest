using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFunction : MonoBehaviour
{
    public void RemoveFromUIManager()
    {
        UIManager.Instance.RemoveOneUI();
    }

    public void DestroyThisObject()
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);

        Destroy(gameObject);
    }

    public void BackIntro()
    {
        UIManager.Instance.RemoveOneUI();

        SceneManager.LoadScene("Intro");
    }
}
