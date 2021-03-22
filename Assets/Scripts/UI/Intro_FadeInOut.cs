using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro_FadeInOut : MonoBehaviour
{
    [SerializeField]
    Sprite[] m_images;

    private int m_curImageIndex = 0;

    private float m_delayTime = 0f;

    private float m_color = 0f;

    private FadeStep m_fadeStep = FadeStep.IN;

    private bool m_finished = false;

    private void Start()
    {
        if (m_images.Length > m_curImageIndex)
        {
            this.gameObject.GetComponent<Image>().sprite = m_images[m_curImageIndex];
        }
    }

    void Update()
    {
        if (m_finished) return;

        switch (m_fadeStep)
        {
            case FadeStep.IN: FadeIn(); break;
            case FadeStep.IDLE: Delay(Time.deltaTime); break;
            case FadeStep.OUT: FadeOut(); break;
            case FadeStep.DONE: FinishFadeStep(); break;
        }
    }

    public bool IsFinished()
    {
        return m_finished;
    }

    void FadeIn()
    {
        m_color += 0.01f;
        this.gameObject.GetComponent<Image>().color = new Color(m_color, m_color, m_color, 1);

        if (m_color >= 1f) m_fadeStep = FadeStep.IDLE;
    }

    void FadeOut()
    {
        m_color -= 0.01f;
        this.gameObject.GetComponent<Image>().color = new Color(m_color, m_color, m_color, 1);

        if (m_color <= 0f) m_fadeStep = FadeStep.DONE;
    }

    void Delay(float fDelaTime)
    {
        m_delayTime += fDelaTime;
        if(m_delayTime >= 2.0f)
        {
            m_fadeStep = FadeStep.OUT;
            m_delayTime = 0f;
        }
    }

    void FinishFadeStep()
    {
        m_curImageIndex++;
        if (m_images.Length > m_curImageIndex)
        {
            this.gameObject.GetComponent<Image>().sprite = m_images[m_curImageIndex];
        }

        m_fadeStep = FadeStep.IN;

        if (m_images.Length <= m_curImageIndex + 1)
        {
            m_finished = true;
            this.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }
}
