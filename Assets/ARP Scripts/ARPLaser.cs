using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class ARPLaser
{
    Vector3 pos, dir;
    GameObject laserObj;
    LineRenderer laser;
    string pointerName;
    Material mat1;
    AudioSource laserSound;
    public GameObject laserTarget;


    public ARPLaser(Vector3 pos, Vector3 dir, Material mat1, string name, AudioSource laserSound)
    {
        this.laser = new LineRenderer();
        this.pointerName = name;
        this.laserObj = new GameObject();
        this.laserTarget = laserObj;
        this.laserObj.name = "Laser Beam-" + name;
        this.pos = pos;
        this.dir = dir;
        this.mat1 = mat1;
        this.laserSound = laserSound;
        this.laserObj.tag = "Laser";

        this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = 0.02f;
        this.laser.endWidth = 0.02f;
        this.laser.material = mat1; // Set the initial material of the LineRenderer to pos
        this.laser.SetPosition(0, pos); // Set the initial position of the LineRenderer to pos

        // Set loop to true and play the sound
        this.laserSound.loop = true;
        this.laserSound.Play();

        CastRay(pos, dir);

        //UnityEngine.Debug.Log("Starting laser: pos = " + pos + ", dir = " + dir);
    }

    void CastRay(Vector3 pos, Vector3 dir)
    {
        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Update the position of the ending point of the line renderer to the point where the ray hits an object
            laser.SetPosition(1, hit.point);

            // Check if the object hit by the raycast is a valid target and take appropriate action
            CheckHit(hit, dir, laser);

            AsssignLaserTarget(hit.collider.gameObject);
            UnityEngine.Debug.Log("Lasertarget:" + laserTarget.name);
        }
        else
        {
            // If the raycast doesn't hit any objects, set the ending point of the line renderer to be 20 units away from the starting point
           // if (laserTarget.name == laserObj.name)
           // {
                //UnityEngine.Debug.Log("Lasertarget No hit:" + laserTarget.name);
                laser.SetPosition(1, dir * 20 + pos);

            // This Block checks the GameObjects in the script controler and initates actions if necessary
            if(GameObject.Find("ScriptController").GetComponent<Test>().laserTargetPortal1 != GameObject.Find("Dummy"))
            {
       
                GameObject.Find("ScriptController").GetComponent<Test>().laserTargetPortal1.GetComponent<ARPLaserStart>().DeactivateLaserStart("portal2");
                GameObject.Find("ScriptController").GetComponent<Test>().laserTargetPortal1 = GameObject.Find("Dummy");

             } else if (GameObject.Find("ScriptController").GetComponent<Test>().laserTargetPortal2 != GameObject.Find("Dummy"))
            {

                GameObject.Find("ScriptController").GetComponent<Test>().laserTargetPortal2.GetComponent<ARPLaserStart>().DeactivateLaserStart("portal1");
                GameObject.Find("ScriptController").GetComponent<Test>().laserTargetPortal2 = GameObject.Find("Dummy");

            } else if (GameObject.Find("ScriptController").GetComponent<Test>().laserTargetAmp1 != GameObject.Find("Dummy"))
            {

                GameObject.Find("ScriptController").GetComponent<Test>().laserTargetAmp1.GetComponent<ARPLaserStart>().DeactivateLaserStart("amp1");
                GameObject.Find("ScriptController").GetComponent<Test>().laserTargetAmp1 = GameObject.Find("Dummy");

            } else if (GameObject.Find("ScriptController").GetComponent<Test>().laserTargetAmp2 != GameObject.Find("Dummy"))
            {

                GameObject.Find("ScriptController").GetComponent<Test>().laserTargetAmp2.GetComponent<ARPLaserStart>().DeactivateLaserStart("amp2");
                GameObject.Find("ScriptController").GetComponent<Test>().laserTargetAmp2 = GameObject.Find("Dummy");

            }
        }
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser)
    {


        switch (hitInfo.collider.gameObject.tag)
        {
            case "ColliderTest":
                laser.SetPosition(1, hitInfo.point);
                //UnityEngine.Debug.Log("Hit object with tag: " + hitInfo.collider.gameObject.tag);
                break;
            case "Amp1":
                hitInfo.collider.gameObject.GetComponent<ARPLaserStart>().SetLaserActive(true);
                hitInfo.collider.gameObject.GetComponent<ARPLaserStart>().SwitchColor("green");
                //UnityEngine.Debug.Log("Hit object with tag: " + hitInfo.collider.gameObject.tag);
                break;
            case "Amp2":
                hitInfo.collider.gameObject.GetComponent<ARPLaserStart>().SetLaserActive(true);
                hitInfo.collider.gameObject.GetComponent<ARPLaserStart>().SwitchColor("blue");
                //UnityEngine.Debug.Log("Hit object with tag: " + hitInfo.collider.gameObject.tag);
                break;
            case "Portal1":
                if (laserObj.name == "Laser Beam-Amp1")
                {
                    hitInfo.collider.gameObject.GetComponent<ARPLaserStart>().SwitchColorPortal(2, "green");
                    hitInfo.collider.gameObject.GetComponent<ARPLaserStart>().ActivateLaserStart("portal2");
                    //UnityEngine.Debug.Log("Activated Laser from: " + hitInfo.collider.gameObject.tag);
                } else if (laserObj.name == "Laser Beam-Amp2")
                {
                    hitInfo.collider.gameObject.GetComponent<ARPLaserStart>().SwitchColorPortal(2, "blue");
                    hitInfo.collider.gameObject.GetComponent<ARPLaserStart>().ActivateLaserStart("portal2");
                    //UnityEngine.Debug.Log("Activated Laser from: " + hitInfo.collider.gameObject.tag);
                } else
                {
                    hitInfo.collider.gameObject.GetComponent<ARPLaserStart>().ActivateLaserStart("portal2");
                    //UnityEngine.Debug.Log("Activated Laser from: " + hitInfo.collider.gameObject.tag);
                   
                }

                break;
            case "Portal2":
                if (laserObj.name == "Laser Beam-Amp1")
                {
                    hitInfo.collider.gameObject.GetComponent<ARPLaserStart>().SwitchColorPortal(1, "green");
                    hitInfo.collider.gameObject.GetComponent<ARPLaserStart>().ActivateLaserStart("portal1");
                    //UnityEngine.Debug.Log("Activated Laser from: " + hitInfo.collider.gameObject.tag);
                }
                else if (laserObj.name == "Laser Beam-Amp2")
                {
                    hitInfo.collider.gameObject.GetComponent<ARPLaserStart>().SwitchColorPortal(1, "blue");
                    hitInfo.collider.gameObject.GetComponent<ARPLaserStart>().ActivateLaserStart("portal1");
                    //UnityEngine.Debug.Log("Activated Laser from: " + hitInfo.collider.gameObject.tag);
                }
                else
                {
                    hitInfo.collider.gameObject.GetComponent<ARPLaserStart>().ActivateLaserStart("portal1");
                    //UnityEngine.Debug.Log("Activated Laser from: " + hitInfo.collider.gameObject.tag);

                }

                break;


        }

    }

    void AsssignLaserTarget(GameObject laserHit)
    {
        GameObject.Find("ScriptController").GetComponent<Test>().laserTargetPortal1 = laserHit;
    }

    void DeleteLaserTarget()
    {
        this.laserTarget = laserObj;
    }
    
}
