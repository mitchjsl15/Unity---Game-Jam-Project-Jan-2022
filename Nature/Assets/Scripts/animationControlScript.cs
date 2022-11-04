using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationControlScript : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((PlayerMovement.isGrounded && PlayerMovement.movingX > 0 && !aimScript.flip) || (PlayerMovement.isGrounded && PlayerMovement.movingX < 0 && aimScript.flip))
        {
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }

        if ((PlayerMovement.isGrounded && PlayerMovement.movingX > 0 && aimScript.flip) || (PlayerMovement.isGrounded && PlayerMovement.movingX < 0 && !aimScript.flip))
        {
            animator.SetBool("walkBack", true);
        }
        else
        {
            animator.SetBool("walkBack", false);
        }

        if (!animator.GetBool("walk") && !animator.GetBool("walkBack"))
        {
            animator.SetBool("idle", true);
        }
        else
        {
            animator.SetBool("idle",false);
        }

    }
}
