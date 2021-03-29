using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour
{
    [SerializeField]
    Sprite[] m_sprites;

    [SerializeField]
    float m_delayTime;

    private float m_curTime = 0f;

    private int m_spriteIndex = 0;

    void Start()
    {
        //m_sprites.Length
    }

    void Update()
    {
        if(m_curTime >= m_delayTime)
        {
            m_curTime = 0f;
            m_spriteIndex++;
            m_spriteIndex %=  m_sprites.Length;

            this.GetComponent<Image>().sprite = m_sprites[m_spriteIndex];
        }
        else
        {
            m_curTime += Time.deltaTime;
        }
    }
}
