using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementSlot : MonoBehaviour
{
    [SerializeField]
    private Text m_name;

    [SerializeField]
    private Text m_pos;

    private GameObject m_placementObject;

    public delegate void OnRemoveObject(GameObject placementObject);
    public OnRemoveObject onRemove;

    public void InitializePlacementSlot(GameObject placementObject, string name)
    {
        m_placementObject = placementObject;
        m_name.text = name;
    }

    public void Update()
    {
        m_pos.text = "x : " + m_placementObject.transform.position.x.ToString("N2") + ", y : " + m_placementObject.transform.position.y.ToString("N2");
    }

    public void OnRemovePlacementObject()
    {
        if(onRemove != null) onRemove(m_placementObject);

        Destroy(this.gameObject);
    }
}
