using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownUpUI : MonoBehaviour
{
    private Transform m_camera;

    private float m_speed = 11f;

    public float m_maxCameraPoint;
    public float m_minCameraPoint;

    public float m_maxMovePos;
    public float m_minMovePos;

    private void Awake()
    {
        m_camera = Camera.main.transform;
    }

    void Update()
    {
        float addValue = m_camera.position.x >= m_minCameraPoint && m_camera.position.x <= m_maxCameraPoint ? m_speed : -m_speed;

        float yPos = Mathf.Clamp(transform.position.y + addValue, m_minMovePos, m_maxMovePos);
        
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }
}
