using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class ARPLaserStart : MonoBehaviour
{
    public Material mat1, mat2, mat3;
    ARPLaser beam;
    public bool laserActive;
    public Vector3 direction;
    public AudioSource laserSound;
    Material matUsed;
    

    void Start()
    {
        matUsed = mat1;
        UnityEngine.Debug.Log(gameObject.transform.position);
    }


    void Update()
    {
        if (laserActive)
        {
            Destroy(GameObject.Find("Laser Beam-" + gameObject.name));
            beam = new ARPLaser(gameObject.transform.position, direction, matUsed, gameObject.name, laserSound); 
        }
        else
        {
            Destroy(GameObject.Find("Laser Beam-" + gameObject.name));
        }
    }

    public void SetLaserActive(bool input)
    {
        laserActive = input;
    }

    public void ActivateLaserStart(string targetobject)
    {
        switch (targetobject)
        {
            case "portal1":
                GameObject.FindWithTag("Portal1").GetComponent<ARPLaserStart>().SetLaserActive(true);
                //UnityEngine.Debug.Log("Activated Laser for: " + GameObject.Find("Portal2").name);
                break;
            case "portal2":
                GameObject.FindWithTag("Portal2").GetComponent<ARPLaserStart>().SetLaserActive(true);
                //UnityEngine.Debug.Log("Activated Laser for: " + GameObject.Find("Portal2").name);
                break;
            case "amp1":
                GameObject.FindWithTag("Amp1").GetComponent<ARPLaserStart>().SetLaserActive(true);
                //UnityEngine.Debug.Log("Activated Laser for: " + GameObject.Find("Portal2").name);
                break;
            case "amp2":
                GameObject.FindWithTag("Amp2").GetComponent<ARPLaserStart>().SetLaserActive(true);
                //UnityEngine.Debug.Log("Activated Laser for: " + GameObject.Find("Portal2").name);
                break;
        }


    }

    public void DeactivateLaserStart(string targetobject)
    {
        switch (targetobject)
        {
            case "portal1":
                GameObject.FindWithTag("Portal1").GetComponent<ARPLaserStart>().SetLaserActive(false);
                //UnityEngine.Debug.Log("Activated Laser for: " + GameObject.Find("Portal2").name);
                break;
            case "portal2":
                GameObject.FindWithTag("Portal2").GetComponent<ARPLaserStart>().SetLaserActive(false);
                //UnityEngine.Debug.Log("Activated Laser for: " + GameObject.Find("Portal2").name);
                break;
            case "amp1":
                GameObject.FindWithTag("Amp1").GetComponent<ARPLaserStart>().SetLaserActive(false);
                //UnityEngine.Debug.Log("Activated Laser for: " + GameObject.Find("Portal2").name);
                break;
            case "amp2":
                GameObject.FindWithTag("Amp2").GetComponent<ARPLaserStart>().SetLaserActive(false);
                //UnityEngine.Debug.Log("Activated Laser for: " + GameObject.Find("Portal2").name);
                break;
        }
    }

    //Changes material input for laser
    public void SwitchColor(string color)
    {
        switch (color)
        {
            case "red":
                matUsed = mat1;
                break;
            case "green":
                matUsed = mat2;
                break;
            case "blue":
                matUsed = mat3;
                break;
        }
    }

    public void SwitchColorPortal(int portal, string color) {
        if (portal == 1)
        {
            switch (color)
            {
                case "red":
                    GameObject.Find("Portal1").GetComponent<ARPLaserStart>().SwitchColor("red");
                    break;
                case "green":
                    GameObject.Find("Portal1").GetComponent<ARPLaserStart>().SwitchColor("green");
                    break;
                case "blue":
                    GameObject.Find("Portal1").GetComponent<ARPLaserStart>().SwitchColor("blue");
                    break;
            }
        }
        else if (portal == 2)
        {
            switch (color)
            {
                case "red":
                    GameObject.Find("Portal2").GetComponent<ARPLaserStart>().SwitchColor("red");
                    break;
                case "green":
                    GameObject.Find("Portal2").GetComponent<ARPLaserStart>().SwitchColor("green");
                    break;
                case "blue":
                    GameObject.Find("Portal2").GetComponent<ARPLaserStart>().SwitchColor("blue");
                    break;
            }
        }
        
        
    }
}