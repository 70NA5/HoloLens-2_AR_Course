using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class LaserRunning : MonoBehaviour {
    private float nextActionTime = 0.0f;
    public float periodSeconds = 0.1f;
    public bool isStarted = false;
    public UnityEvent onChange;

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
                    bool newValue = Result.CreateFromJSON(webRequest.downloadHandler.text).isStarted;
                    isStarted = newValue;
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    if (onChange != null && newValue != isStarted) {
                        onChange.Invoke();
                    }
                    break;
            }
        }
    }
}
