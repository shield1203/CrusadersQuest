using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSystem : MonoBehaviour
{
    [SerializeField]
    GameObject m_;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnLoadLobbyScene()
    {
        SceneManager.LoadScene("Lobby");
    }
}
