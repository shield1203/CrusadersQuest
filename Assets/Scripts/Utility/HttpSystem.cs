using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpSystem : MonoBehaviour
{
    private const string m_serverIP = "http://203.232.193.169:3000";

    private string m_userId;

    public delegate void OnDelegate();

    private void Start()
    {
        m_userId = PlayerPrefs.GetInt("UserId").ToString();
    }

    //public void OnGoogleLogin()
    //{

    //}

    //IEnumerator GoogleLogin()
    //{
    //    yield return null;
    //}

    //public void OnFaceBookLogin()
    //{

    //}

    //IEnumerator FacebookLogin()
    //{
    //    yield return null;
    //}

    public void OnTest()
    {
        StartCoroutine(Test());
    }

    public IEnumerator Test()
    {
        string strScheme = "/Test";
        string strURL = m_serverIP + strScheme;
        Debug.Log(strURL);
        UnityWebRequest request = UnityWebRequest.Get(strURL);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            
            if(PlayerPrefs.GetString("Login") == "true")
            {
                Debug.Log("Good");
            }
            else
            {
                Debug.Log("Fail");
            }
        }
    }

    public void OnGuestLogin(int userId)
    {
        StartCoroutine(GuestLogin(userId));
    }

    IEnumerator GuestLogin(int userId)
    {
        string strScheme = "/GuestLogin?userId=" + userId.ToString();
        string strURL = m_serverIP + strScheme;
        UnityWebRequest request = UnityWebRequest.Get(strURL);

        UIManager.Instance.AddUI(UIPrefab.LOADING);

        yield return request.SendWebRequest();

        UIManager.Instance.RemoveOneUI();

        if (request.isNetworkError || request.isHttpError)
        {
            UIManager.Instance.AddUI(UIPrefab.ERROR);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            // request.downloadHandler.text를 아이디에 저장
        }
    }

    public IEnumerator RequestSoldierListData(OnDelegate action)
    {
        string strScheme = "/soldierList?userId=" + m_userId;
        string strURL = m_serverIP + strScheme;
        UnityWebRequest request = UnityWebRequest.Get(strURL);

        UIManager.Instance.AddUI(UIPrefab.LOADING);

        yield return request.SendWebRequest();

        UIManager.Instance.RemoveOneUI();

        if (request.isNetworkError || request.isHttpError)
        {
            UIManager.Instance.AddUI(UIPrefab.ERROR);
        }
        else if(request.downloadHandler.text == "fail")
        {
            UIManager.Instance.AddUI(UIPrefab.ERROR);
            // 해당 아이디가 없을 경우(나중에 해당 UI만들던지 따로 처리)
        }
        else
        {
            SoldierManager.Instance.InitializeSoldierData(request.downloadHandler.text);

            if (action != null) action();
        }
    }

    public IEnumerator RequestUpdateTeamData(int soldeirId, int isTeam)
    {
        string strScheme = "/UpdateTeam?userId=" + m_userId + "&soldierId=" + soldeirId.ToString() + "&isTeam=" + isTeam.ToString();
        string strURL = m_serverIP + strScheme;
        UnityWebRequest request = UnityWebRequest.Get(strURL);

        UIManager.Instance.AddUI(UIPrefab.LOADING);

        yield return request.SendWebRequest();

        UIManager.Instance.RemoveOneUI();

        if (request.isNetworkError || request.isHttpError)
        {
            UIManager.Instance.AddUI(UIPrefab.ERROR);
        }
        else if (request.downloadHandler.text == "fail")
        {
            UIManager.Instance.AddUI(UIPrefab.ERROR); 
        }
        else
        {
            SoldierManager.Instance.UpdateSoldierTeam(soldeirId, isTeam);
        }
    }
}