using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMenu : MonoBehaviour
{
    public HttpSystem m_httpSystem;   
    public Transform m_camera;

    public void OnOpenSoldierList(bool isInVillage)
    {
        StartCoroutine(InitSoldierListUI(isInVillage));
    }

    IEnumerator InitSoldierListUI(bool isInVillage)
    {
        if (isInVillage) yield return StartCoroutine(MoveCameraToObject(transform.position));

        StartCoroutine(m_httpSystem.RequestSoldierListData(OpenSoldierListUI));
    }

    IEnumerator MoveCameraToObject(Vector3 target)
    {
        while (target.x != m_camera.position.x)
        {
            target = new Vector3(target.x, m_camera.position.y, m_camera.position.z);

            m_camera.position = Vector3.MoveTowards(m_camera.position, target, 1f);

            yield return null;
        }
    }

    void OpenSoldierListUI()
    {
        UIManager.Instance.AddUI(UIPrefab.SOLDIER_LIST);
    }

    //////////
    public void OnOpenMenuUI(bool isInVillage)
    {
        if (isInVillage) StartCoroutine(MoveCameraToObject(transform.position));
    }
}
