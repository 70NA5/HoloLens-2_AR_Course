using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LaserIsRunning : MonoBehaviour {
    private float nextActionTime = 0.0f;
    public float periodSeconds = 0.1f;
    public bool isStarted = false;
    
    void Start() {}

    void Update () {
        if (Time.time > nextActionTime ) {
            nextActionTime = Time.time + periodSeconds;
            check();
        }
    }

    public void check() {
        StartCoroutine(getRequest("http://192.168.1.1:3000/api/isStarted"));
    }

    IEnumerator getRequest(string uri) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for result
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    isStarted = Result.CreateFromJSON(webRequest.downloadHandler.text).isStarted;
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }
}

[System.Serializable]
class Result
{
    public bool isStarted;

    public static Result CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Result>(jsonString);
    }
}