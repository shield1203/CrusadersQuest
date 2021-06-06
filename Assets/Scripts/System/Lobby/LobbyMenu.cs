using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LobbyButton
{
    Shop,
    Contract,
    Bakery,
    Facilities,
    Storage,
    Equipment,
    Champion,
    Soldier,
    Start,
    Village,
    Fishing,
    Orchard,
    Adventure,
    Fight,
    Dungeon,
    Suppression,
}

public class LobbyMenu : MonoBehaviour
{
    [SerializeField]
    private HttpSystem m_httpSystem;

    [SerializeField]
    private Transform m_camera;

    [SerializeField]
    private bool m_inVillage;

    [SerializeField]
    private LobbyButton m_lobbyButton;

    public void TouchButton()
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);
        StartCoroutine(ButtonAction(m_inVillage, m_lobbyButton));
    }

    IEnumerator ButtonAction(bool isInVillage, LobbyButton lobbyButton)
    {
        if (isInVillage) yield return StartCoroutine(MoveCameraToObject(transform.position));

        switch(lobbyButton)
        {
            case LobbyButton.Shop: break;
            case LobbyButton.Contract: break;
            case LobbyButton.Bakery: break;
            case LobbyButton.Facilities: break;
            case LobbyButton.Storage: break;
            case LobbyButton.Equipment: break;
            case LobbyButton.Champion: break;
            case LobbyButton.Soldier: StartCoroutine(m_httpSystem.RequestSoldierListData(OpenSoldierListUI)); break;
            case LobbyButton.Start: break;
            case LobbyButton.Village: break;
            case LobbyButton.Fishing: break;
            case LobbyButton.Orchard: break;
            case LobbyButton.Adventure: UIManager.Instance.AddUI(UIPrefab.ADVENTURE); break;
            case LobbyButton.Fight: break;
            case LobbyButton.Dungeon: break;
            case LobbyButton.Suppression: break;
        }
    }

    IEnumerator MoveCameraToObject(Vector2 target)
    {
        while (target.x != m_camera.position.x)
        {
            target = new Vector2(target.x, m_camera.position.y);

            m_camera.position = Vector2.MoveTowards(m_camera.position, target, 1.5f);

            yield return null;
        }
    }

    public void OnMoveCameraToPoint(float xPos)
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);
        StartCoroutine(MoveCameraToPoint(xPos));
    }

    IEnumerator MoveCameraToPoint(float xPos)
    {
        while (xPos != m_camera.position.x)
        {
            Vector2 Point = m_camera.position;
            Point.x = xPos;

            m_camera.position = Vector2.MoveTowards(m_camera.position, Point, 1.5f);

            yield return null;
        }
    }

    void OpenSoldierListUI()
    {
        UIManager.Instance.AddUI(UIPrefab.SOLDIER_LIST);
    }
}
