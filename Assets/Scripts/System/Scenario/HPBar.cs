using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    private UnitBase m_unit;
    private Image m_healthGage;

    void Start()
    {
        m_unit = gameObject.transform.parent.GetComponent<UnitBase>();
        Transform curHPImage = gameObject.transform.Find("Background").Find("curHP");
        m_healthGage = curHPImage.GetComponent<Image>();
    }

    void Update()
    {
        if(m_unit != null)
        {
            m_healthGage.fillAmount = m_unit.GetHPPercent();

            if (m_unit.IsDie()) Destroy(this);
        }
        else
        {
            Destroy(this);
        }
    }
}
