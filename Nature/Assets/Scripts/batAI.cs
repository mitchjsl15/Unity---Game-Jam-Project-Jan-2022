using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batAI : MonoBehaviour
{
    public Transform player;
    public Transform batLoc;
    public CharacterController bat;
    private Vector3 gap;
    private bool freeze;


    // Start is called before the first frame update
    void Start()
    {
        freeze = true;
    }

    // Update is called once per frame
    void Update()
    {
        gap = player.position - batLoc.position;
        
        if (Mathf.Abs(gap.x) < 10) freeze = false;   

        if (!freeze) bat.Move(gap / 100);

 

    }
}
