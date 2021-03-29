using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchMove : MonoBehaviour
{
    [SerializeField]
    Transform m_camera;

    [SerializeField]
    float m_maxPos = 0f;

    [SerializeField]
    float m_minPos = 0f;

    Rigidbody2D m_rigid;

    private float m_speed = 5.5f;

    private Vector2 m_acceleration = Vector2.zero;

    private Vector2 m_prevPos = Vector2.zero;

    private void Awake()
    {
        m_rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float objectXPos = Mathf.Clamp(gameObject.transform.position.x, m_minPos, m_maxPos);
        gameObject.transform.position = new Vector3(objectXPos, 0, 0);
        m_camera.position = new Vector3(objectXPos, m_camera.position.y, m_camera.position.z);
    }

    public void OnMouseDrag()
    {
        if (m_prevPos == Vector2.zero)
        {
            m_prevPos = Input.mousePosition;
            m_rigid.velocity = new Vector2(0f, 0f);
            return;
        }

        m_acceleration = ((Vector2)Input.mousePosition - m_prevPos);
        Vector3 vec = new Vector3(m_acceleration.x, 0, 0);
        Vector3 cameraLocation = m_camera.position - (vec * m_speed * Time.deltaTime);

        gameObject.transform.position = new Vector3(cameraLocation.x, 0, 0);

        m_prevPos = Vector2.zero;
    }

    public void OnMouseUp()
    {
        m_rigid.AddForce(Vector2.right * m_acceleration.x * -1, ForceMode2D.Impulse);
        m_prevPos = Vector2.zero;
    }
}
