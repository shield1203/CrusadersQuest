using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwap : MonoBehaviour
{
    private Transform m_camera;

    [SerializeField]
    public float m_speed;

    [SerializeField]
    Transform m_uiSlot1;

    [SerializeField]
    Transform m_uiSlot2;

    private float m_slot1StartPos;
    private float m_slot2StartPos;

    [SerializeField]
    public float m_controlPoint;

    private void Awake()
    {
        m_camera = Camera.main.transform;
    }

    private void Start()
    {
        m_slot1StartPos = m_uiSlot1.localPosition.y;
        m_slot2StartPos = m_uiSlot2.localPosition.y;
    }

    void Update()
    {
        Vector3 targetLocation;
        if (m_controlPoint > m_camera.position.x) 
        {
            targetLocation = m_uiSlot1.localPosition;
            targetLocation.y = m_slot1StartPos;
            m_uiSlot1.localPosition = Vector3.MoveTowards(m_uiSlot1.localPosition, targetLocation, m_speed);

            targetLocation = m_uiSlot2.localPosition;
            targetLocation.y = m_slot2StartPos;
            m_uiSlot2.localPosition = Vector3.MoveTowards(m_uiSlot2.localPosition, targetLocation, m_speed);
        }
        else
        {
            targetLocation = m_uiSlot1.localPosition;
            targetLocation.y = m_slot2StartPos;
            m_uiSlot1.localPosition = Vector3.MoveTowards(m_uiSlot1.localPosition, targetLocation, m_speed);

            targetLocation = m_uiSlot2.localPosition;
            targetLocation.y = m_slot1StartPos;
            m_uiSlot2.localPosition = Vector3.MoveTowards(m_uiSlot2.localPosition, targetLocation, m_speed);
        }
    }
}
