using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpSystem : MonoBehaviour
{
    private const string m_serverIP = "http://203.232.193.169:3000";
    
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

        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }
    }

    public IEnumerator RequestSoldierList(int userId)
    {
        yield return null;
    }
}
