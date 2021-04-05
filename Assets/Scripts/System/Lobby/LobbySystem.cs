using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySystem : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.ActiveUI(true);
    }

    void Update()
    {
        
    }

    public void OnSoldierListUI()
    {
        UIManager.Instance.AddUI(UIPrefab.SOLDIER_LIST);
    }
}
