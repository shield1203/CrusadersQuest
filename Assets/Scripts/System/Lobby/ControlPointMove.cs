using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPointMove : MonoBehaviour
{
    private Transform m_camera;

    public bool m_isUp;
    public float m_speed;

    private float m_maxPos;
    public float m_minPos;

    public float m_controlPoint;

    private void Awake()
    {
        m_camera = Camera.main.transform;
    }

    private void Start()
    {
        //m_slot1StartPos = m_uiSlot1.position.y;
        //m_slot2StartPos = m_uiSlot2.position.y;
        //m_uiSlot2.position = new Vector3(m_uiSlot2.position.x, m_uiSlot2.position.y - m_maxMoveLength, m_uiSlot2.position.z);
    }

    void Update()
    {
        //float addValue = m_camera.position.x >= m_minCameraPoint && m_camera.position.x <= m_maxCameraPoint ? m_speed : -m_speed;
        //retc
        //if(m_controlPoint >)
        //float yPos = Mathf.Clamp(transform.position.y + addValue, m_minMovePos, m_maxMovePos);

        //transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        //if (m_controlPoint > m_camera.position.x) // left
        //{
        //    float fMove = Mathf.Clamp(m_curMoveLength + Time.deltaTime, 0, m_maxMoveLength);
        //    m_uiSlot1.position = new Vector3(m_uiSlot1.position.x, m_uiSlot1.position.y + fMove, m_uiSlot1.position.z);
        //    m_uiSlot2.position = new Vector3(m_uiSlot2.position.x, m_uiSlot2.position.y - fMove, m_uiSlot2.position.z);
        //}
        //else
        //{

        //}
    }
}
