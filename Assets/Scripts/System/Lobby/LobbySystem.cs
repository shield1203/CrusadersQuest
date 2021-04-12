using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySystem : MonoBehaviour
{

    void Start()
    {
        UIManager.Instance.ActiveUI(true);
        // 유저 데이터 받아오기
    }

    public void OnSoldierListUI()
    {
        // 서버로부터 솔저 데이터 받아오기
        UIManager.Instance.AddUI(UIPrefab.SOLDIER_LIST);
    }

    public void OnAdventureUI()
    {
        UIManager.Instance.AddUI(UIPrefab.ADVENTURE);
    }

    //IEnumerator 
}
