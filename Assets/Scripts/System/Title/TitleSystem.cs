using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSystem : MonoBehaviour
{
    private bool m_ready = false;

    public void CutTitle()
    {
        SoundSystem.Instance.StartBGM(BGM.title);

        m_ready = true;
    }

    public void OnLoadLobbyScene()
    {
        if(m_ready) SceneManager.LoadScene("Lobby");
    }
}
