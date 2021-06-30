using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSystem : MonoBehaviour
{
    private bool m_ready = false;

    private HttpSystem m_httpSystem;

    [SerializeField]
    Text m_troopName;

    private void Start()
    {
        m_httpSystem = GetComponent<HttpSystem>();
        StartCoroutine(m_httpSystem.RequestUserData(SetTroopName));
    }

    void SetTroopName()
    {
        m_troopName.text = UserDataManager.Instance.GetUserData().name;
    }

    public void CutTitle()
    {
        SoundSystem.Instance.StartBGM(BGM.title);

        m_ready = true;
    }

    public void OnLoadLobbyScene()
    {
        if(m_ready) SceneManager.LoadScene("Lobby");
    }

    public void OnLogOut()
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);

        UIManager.Instance.AddUI(UIPrefab.LOGOUT);
    }
}
