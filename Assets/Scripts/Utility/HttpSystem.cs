using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class HttpSystem : MonoBehaviour
{
    private const string m_serverIP = "http://203.232.193.169:3000";

    static private string m_userId;

    public delegate void OnDelegate();

    private void Start()
    {
        m_userId = PlayerPrefs.GetInt("UserId").ToString();
    }

    public void OnCheckServerOpen()
    {
        StartCoroutine(CheckServerOpen());
    }

    IEnumerator CheckServerOpen()
    {
        string strScheme = "/";
        string strURL = m_serverIP + strScheme;
        UnityWebRequest request = UnityWebRequest.Get(strURL);

        UIManager.Instance.AddUI(UIPrefab.LOADING);

        yield return request.SendWebRequest();

        UIManager.Instance.RemoveOneUI();

        if (request.isNetworkError || request.isHttpError)
        {
            UIManager.Instance.AddUI(UIPrefab.ERROR);
        }
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

    public void OnSignInGeust()
    {
        StartCoroutine(SignInGeust());
    }

    IEnumerator SignInGeust()
    {
        string strScheme = "/SignInGuest";
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
            PlayerPrefs.SetString("LoginType", "Guest");
            PlayerPrefs.SetInt("UserId", int.Parse(request.downloadHandler.text));

            UIManager.Instance.RemoveAllUI();
            SceneManager.LoadScene("Title");
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

    public IEnumerator RequestUpdateName(string name, OnDelegate action)
    {
        string strScheme = "/UpdateName?userId=" + m_userId + "&name=" + name;
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

    public IEnumerator RequestUpdateUserExp(float exp, OnDelegate action)
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

            if (action != null) action();
        }
    }

    public IEnumerator RequestUpdateSoldierExp(SoldierData soldierData, float exp, OnDelegate action)
    {
        SoldierData data = SoldierManager.Instance.AddSoldierExp(soldierData, exp);

        string strScheme = "/UpdateSoldierExp?userId=" + m_userId + "&soldierId=" + data.soldier_id.ToString() 
            + "&lv=" + data.level.ToString() + "&exp=" + data.exp.ToString();
        string strURL = m_serverIP + strScheme;
        UnityWebRequest request = UnityWebRequest.Get(strURL);

        yield return request.SendWebRequest();


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
            if (action != null) action();
        }
    }
}