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

    public IEnumerator RequestUserData(OnDelegate action)
    {
        string strScheme = "/UserData?userId=" + m_userId;
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
            UserDataManager.Instance.InitializeUserData(request.downloadHandler.text);

            if (action != null) action();
        }
    }

    public IEnumerator RequestSoldierListData(OnDelegate action)
    {
        string strScheme = "/SoldierList?userId=" + m_userId;
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

    public IEnumerator RequestUpdateUserExp(float exp)
    {
        UserData userData = UserDataManager.Instance.AddUserExp(exp);

        string strScheme = "/UpdateUserExp?userId=" + m_userId + "&lv=" + userData.lv.ToString() + "&exp=" + userData.exp.ToString();
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
            UserDataManager.Instance.InitializeUserData(userData);
        }
    }
}