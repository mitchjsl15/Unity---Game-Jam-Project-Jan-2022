using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float speed;
    public Camera cam;
    public Transform subject;

    Vector2 startPosition;
    float startZ;

    Vector2 travel => (Vector2)cam.transform.position - startPosition;

    Vector2 parralaxFactor;

    public void Start()
    {
        startPosition = transform.position;
        startZ = transform.position.z;
    }

    public void Update()
    {
        transform.position = startPosition + travel * speed;
    }


}
