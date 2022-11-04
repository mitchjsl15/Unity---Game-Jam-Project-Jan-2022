using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Collider2D playerCollider;
    public Transform GroundCheck;
    public LayerMask Ground;
    public Transform shootLocation;
    public GameObject airBlastEffect;
    
    public static bool isGrounded;
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
    Vector2 boxSize = new Vector2(0.7f, 0.1f);

    bool freeze = true;

    void Start()
    {
        airBlastAvailable = true;
        airBlastCount = airBlastCooldown;
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
                Move.x += airResistance * Time.deltaTime;
            }
            else if (Move.x <= -airControlSpeed && Move.x < 0)
            {
                Move.x -= airResistance * Time.deltaTime;
            }
            else
            {
                Move.x = Input.GetAxisRaw("Horizontal") * Speed;
            }
            

            Move.y += Gravity * Time.deltaTime;


            isGrounded = Physics2D.BoxCast(GroundCheck.position, boxSize, 0, Vector2.up, 0.1f, Ground);
            


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

            controller.Move(Move * Time.deltaTime);

            movingX = Move.x;
        }

     

    }

    void FixedUpdate()
    {

        

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


