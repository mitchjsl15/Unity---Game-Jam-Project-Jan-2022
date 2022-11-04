using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform PlayerT;
    public Transform MiddleCheck;
    public LayerMask Ground;
    public Transform shootLocation;
    public GameObject airBlastEffect;
    
    public static bool isGrounded;
    public static bool isTouchingWallOnRight;
    public static bool isTouchingWallOnLeft;
    public static bool isCeilinged;

    private bool airBlastAvailable;

    public float jumpHeight = 23f;
    public float Speed = 8f;
    public float Gravity = -66f;
    public float airResistance = -66f;
    private float theta;
    public float blastPower = 28f;
    public float airBlastCooldown = 200f;
    private float airBlastCount;
    public float blastLinger = 10f;
    [Tooltip("IMPORTANT: airControlSpeed should ALWAYS be HIGHER than Speed")]
    public float airControlSpeed = 9f;

    public static bool isAirblast;
    public static float movingX;

    Vector3 Move = Vector3.zero;

    private bool freeze;

    void Start()
    {
        Application.targetFrameRate = 144;
        airBlastAvailable = true;
        airBlastCount = airBlastCooldown;
        freeze = false;
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetButton("Jump"))
        {
            freeze = false;
        }
        if (!freeze)
        {

            //Debug.Log(Input.GetAxisRaw("Horizontal"));

            if (Mathf.Abs(Move.x) < airControlSpeed && Input.GetAxisRaw("Horizontal") != 0)
            {
                Move.x = Input.GetAxisRaw("Horizontal") * Speed;
            }
            else if (Move.x >= airControlSpeed && Move.x > 0)
            {
                Move.x += airResistance * 0.005f;
            }
            else if (Move.x <= -airControlSpeed && Move.x < 0)
            {
                Move.x -= airResistance * 0.005f;
            }
            else
            {
                Move.x = Input.GetAxisRaw("Horizontal") * Speed;
            }
           

            isGrounded = Physics2D.BoxCast(MiddleCheck.position + new Vector3(0f, -0.75f, 0f), new Vector2(0.6f, 0.1f), 0, new Vector2(0f, 1f), 0.1f, Ground);
            isTouchingWallOnRight = Physics2D.BoxCast(MiddleCheck.position + new Vector3(0.3f, -0.2f, 0f), new Vector2(0.1f, 1f), 0, new Vector2(-1f, 0f), 0.1f, Ground);
            isTouchingWallOnLeft = Physics2D.BoxCast(MiddleCheck.position + new Vector3(-0.3f, -0.2f, 0f), new Vector2(0.1f, 1f), 0, new Vector2(1f, 0f), 0.1f, Ground);
            isCeilinged = Physics2D.BoxCast(MiddleCheck.position + new Vector3(0f, 0.35f, 0f), new Vector2(0.6f, 0.1f), 0, new Vector2(0f, -1f), 0.1f, Ground);

            //Debug.Log(isGrounded);
            //Debug.Log(isTouchingWallOnRight);
            //Debug.Log(isTouchingWallOnLeft);
            //Debug.Log(isCeilinged);


            if (Input.GetButton("Jump") && isGrounded)
            {
                Move.y = jumpHeight;
            }

            Vector3 airBlast = Vector3.zero;

            theta = aimScript.theta;
            if (Input.GetButtonDown("Fire2") && airBlastAvailable)
            {
                airBlast = AirBlast(theta);
                Move.x = 0f;
                Move.y = 0f;

                airBlastAvailable = false;
                isAirblast = true;

                if (aimScript.flip)
                {
                    Instantiate(airBlastEffect, shootLocation.position, Quaternion.Euler(0f, 0f, aimScript.theta + 180f));
                }
                else
                {
                    Instantiate(airBlastEffect, shootLocation.position, Quaternion.Euler(0f, 0f, aimScript.theta));
                }
                
            }

            Move.x += airBlast.x;
            Move.y += airBlast.y;

            if (isGrounded && Move.y < 0)
            {
                Move.y = 0;
            }

            if (isTouchingWallOnRight && Move.x > 0)
            {
                Move.x = 0;
            }

            if (isTouchingWallOnLeft && Move.x < 0)
            {
                Move.x = 0;
            }

            if (isCeilinged && Move.y > 0)
            {
                Move.y = 0;
            }

            if (!isGrounded)
            {
                Move.y += Gravity * 0.005f;
            }

            if(Input.GetKeyDown("q"))
            {
                Move.y = 4;
            }

            controller.Move(Move * 0.005f);

            movingX = Move.x;


            if (Input.GetKeyDown("p"))
            {
                SceneManager.LoadSceneAsync("level 1");
            }
            
        }

     

    }

    void FixedUpdate()
    {
        if (isGrounded && isTouchingWallOnLeft && isTouchingWallOnRight)
        {
            PlayerT.position += new Vector3(0f, 0.1f, 0f);
        }


        if (!airBlastAvailable)
        {
            airBlastCount--;

            if (airBlastCount <= airBlastCooldown - blastLinger)
            {
                isAirblast = false;
            }

            if (airBlastCount == 0)
            {
                airBlastAvailable = true;
                airBlastCount = airBlastCooldown;
            }

        }
    }

    private Vector3 AirBlast(float theta)
    {
        theta *= Mathf.Deg2Rad;
        Vector3 airBlast = Vector3.zero;
        airBlast.x = Mathf.Cos(theta);
        airBlast.y = Mathf.Sin(theta);

        if (aimScript.flip)
        {
            airBlast = -airBlast;
        }

        return -blastPower*airBlast;
    }
}


