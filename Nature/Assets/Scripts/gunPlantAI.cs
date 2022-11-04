using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPlantAI : MonoBehaviour
{
    public float shootDelay = 20f; // bullet is fired once every ____ frames.
    public GameObject bulletPrefab;
    public Transform shootLocation;
    private float delayCount;

    // Start is called before the first frame update
    void Start()
    {
        delayCount = shootDelay;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (delayCount == shootDelay)
        {
            ShootBullet();
        }

        delayCount--;

        if (delayCount == 0)
        {
            delayCount = shootDelay;
        }

        
    }

    void ShootBullet()
    {
        Instantiate(bulletPrefab, shootLocation.position, Quaternion.Euler(0, 0, 0));
    }
}
