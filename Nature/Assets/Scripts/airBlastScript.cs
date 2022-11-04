using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airBlastScript : MonoBehaviour
{
    public float lifespan = 40f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lifespan--;

        if (lifespan <= 0)
        {
            Destroy(gameObject);
        }
    }
}
