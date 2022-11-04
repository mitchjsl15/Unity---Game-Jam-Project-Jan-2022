using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newParallax : MonoBehaviour
{
    public float paraEffect;
    public Transform cameraT;
    public Transform background;
    public bool vert = true;
    public float vertOffset = 1f;

    public void Update()
    {
       
        Vector3 pos = cameraT.position * paraEffect;

        if (!vert) pos.y = 0;

        pos.y -= vertOffset;
        background.position = pos;
      
    }


}
