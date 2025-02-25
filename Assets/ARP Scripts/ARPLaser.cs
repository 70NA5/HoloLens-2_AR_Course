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
    Material mat1, mat2;
    AudioSource laserSound;


    public ARPLaser(Vector3 pos, Vector3 dir, Material red, Material green, string name, AudioSource laserSound)
    {
        this.laser = new LineRenderer();
        this.pointerName = name;
        this.laserObj = new GameObject();
        this.laserObj.name = "Laser Beam-" + name;
        this.pos = pos;
        this.dir = dir;
        this.mat1 = red;
        this.mat2 = green;
        this.laserSound = laserSound;

        this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = 0.02f;
        this.laser.endWidth = 0.02f;
        this.laser.material = red; // Set the initial material of the LineRenderer to pos
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
        }
        else
        {
            // If the raycast doesn't hit any objects, set the ending point of the line renderer to be 20 units away from the starting point
            laser.SetPosition(1, dir * 20 + pos);
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
            case "AmpTest":
                hitInfo.collider.gameObject.GetComponent<ARPLaserStart>().SetLaserActive(true);
                //UnityEngine.Debug.Log("Hit object with tag: " + hitInfo.collider.gameObject.tag);
                break;
            case "PortalTest":
                hitInfo.collider.gameObject.GetComponent<ARPLaserStart>().ActivateSecondPortal();
                //UnityEngine.Debug.Log("Activated Laser from: " + hitInfo.collider.gameObject.tag);
                break;

        }

    }
}
