using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutTitleAnimation : MonoBehaviour
{
    [SerializeField]
    GameObject m_nextObject = null;

    void OnAnimationFinished()
    {
        if (m_nextObject)
        {
            m_nextObject.SetActive(true);
        }
        else
        {
            TitleSystem titleSystem = FindObjectOfType(typeof(TitleSystem)) as TitleSystem;
            titleSystem.CutTitle();
        }

        this.gameObject.SetActive(false);
    }
}
