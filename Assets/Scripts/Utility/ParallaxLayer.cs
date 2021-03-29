using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float m_movementScale = 1f;

    Vector3 m_objectLocation;

    Transform m_camera;

    private void Awake()
    {
        m_objectLocation = transform.position;
        m_camera = Camera.main.transform;
    }

    void Update()
    {
        transform.position = new Vector3(m_objectLocation.x + (m_camera.position.x * m_movementScale), m_objectLocation.y, m_objectLocation.z);
    }
}
