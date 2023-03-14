using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blendShapeLoop : MonoBehaviour
{
    SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh skinnedMesh;
    int blendShapeCount;

    int playIndex = 0;

    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        skinnedMesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        blendShapeCount = skinnedMesh.blendShapeCount;
    }

    void Update ()
    {
        if(playIndex > 0) skinnedMeshRenderer.SetBlendShapeWeight(playIndex-1, 0f);
        if(playIndex == 0)  skinnedMeshRenderer.SetBlendShapeWeight(blendShapeCount, 0f);
        skinnedMeshRenderer.SetBlendShapeWeight(0, 100f);
        playIndex++;
        if(playIndex > blendShapeCount-1) playIndex = 0;
    }

}