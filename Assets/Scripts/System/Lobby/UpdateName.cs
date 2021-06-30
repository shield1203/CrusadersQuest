using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateName : MonoBehaviour
{
    HttpSystem m_httpSystem;

    [SerializeField]
    Text m_name;

    [SerializeField]
    Button m_submission;

    [SerializeField]
    Text m_id;

    private void Start()
    {
        m_httpSystem = GetComponent<HttpSystem>();

        m_id.text = PlayerPrefs.GetInt("UserId").ToString();
    }

    private void Update()
    {
        m_submission.interactable = (m_name.text.Length >= 2 && m_name.text.Length <= 12);
    }

    public void Submission()
    {
        StartCoroutine(m_httpSystem.RequestUpdateName(m_name.text, QuitUI));
    }

    void QuitUI()
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);
        UIManager.Instance.RemoveOneUI();

        SceneManager.LoadScene("Lobby");
    }
}
