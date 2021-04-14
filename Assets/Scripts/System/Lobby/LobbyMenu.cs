using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMenu : MonoBehaviour
{
    public HttpSystem m_httpSystem;   
    public Transform m_camera;

    public void OnOpenSoldierList(bool isInVillage)
    {
        StartCoroutine(OpenSoldierListUI(isInVillage));
    }

    IEnumerator OpenSoldierListUI(bool iSInVillage)
    {
        if (iSInVillage) yield return StartCoroutine(MoveCameraToObject(transform.position));

        // 서버로부터 데이터 받아옴

        // 성공하든 실패하든 로딩UI 끄고
        // 각자에 맞는 UI출력
    }

    void asdad()
    {

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
}
