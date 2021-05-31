using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageTitle : MonoBehaviour
{
    [SerializeField]
    private Text m_number;

    [SerializeField]
    private Text m_name;

    void Start()
    {
        m_number.text = "STAGE " + StageManager.Instance.GetCurStage().ToString();
        m_name.text = StageManager.Instance.GetCurStageName();
    }
}
