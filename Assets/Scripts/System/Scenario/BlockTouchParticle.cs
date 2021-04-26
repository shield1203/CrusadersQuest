using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockTouchParticle : MonoBehaviour
{
    [SerializeField]
    Image m_circle;

    [SerializeField]
    Sprite[] m_circleSprite;

    [SerializeField]
    Image m_light;

    [SerializeField]
    Sprite[] m_lightSprite;

    [SerializeField]
    Image[] m_star;

    [SerializeField]
    Sprite[] m_starSprite;

    [SerializeField]
    Sprite m_grayStarSprite;

    public void InitializeParticle(BlockColor color, bool activeValue)
    {
        m_circle.sprite = m_circleSprite[(int)color];
        m_light.sprite = m_lightSprite[(int)color];

        for(int index = 0; index < m_star.Length; index++)
        {
            if (activeValue)
            {
                m_star[index].sprite = m_starSprite[(int)color];
            }
            else
            {
                m_star[index].sprite = m_grayStarSprite;
            }
        }
    }

    public void DestroyParticle()
    {
        Destroy(this.gameObject);
    }
}
