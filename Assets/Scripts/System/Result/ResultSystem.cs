using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSystem : MonoBehaviour
{
    const float unitInitXPos = -5.71f;
    const float unitDistance = 2.68f;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void StageSelect()
    {

    }

    public void ReStart()
    {
        SceneManager.LoadScene("Stage");
    }

    public void NextStage()
    {

    }

    public void BackLobby()
    {
        UIManager.Instance.RemoveAllUI();
        SceneManager.LoadScene("Lobby");
    }
}
