using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimScript : MonoBehaviour
{
    //Vector3 offset = new Vector3(Screen.width/2, Screen.height/2, 0f);
    Vector3 gunAngle = Vector3.zero;

    public Transform gunRotate;
    public Transform shootLocation;
    public Transform playerToFlip;
    public static float theta;
    public static bool flip;
    public static bool flameOn;
    public static int flameStrength;
    private int endFlame = 0;
    private int countDelay = 0;
    public int countAmount = 3;

    private Sprite[] firstSprites;
    private Sprite[] finalSprites;
    public Sprite test;
    public SpriteRenderer spriteR;
    //public GameObject bulletPrefab;


    // Start is called before the first frame update
    void Start()
    {
        flameStrength = 0;

        firstSprites = Resources.LoadAll<Sprite>("flame animation frames/first21");
        finalSprites = Resources.LoadAll<Sprite>("flame animation frames/last4");
    }

    // Update is called once per frame
    void Update()
    {
        // get cursor location
        Vector3 cursorLocationWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // get relative coordinates of cursor to pivot of gun
        Vector3 aimLocation = cursorLocationWorld - gunRotate.position;
        aimLocation.z = 0;

        //flip player sprite and children if aiming "behind"
        if (aimLocation.x <= 0)
        {
            flip = true;
            playerToFlip.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            flip = false;
            playerToFlip.localScale = new Vector3(1f, 1f, 1f);
        }

        //calculte angle using trig, and angle to rotate about z axis
        theta = Mathf.Rad2Deg * Mathf.Atan(aimLocation.y / aimLocation.x);
        gunAngle.z = theta;

        //rotate gun using public transform about z axis
        gunRotate.eulerAngles = gunAngle;
    }

    void Shoot()
    {
        flameOn = true;
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();

            if (flameStrength < 22)
            {
                flameStrength++;
            }
        }
        else
        {
            flameOn = false;

            if (flameStrength > 0)
            {
                flameStrength -= 2;
            }
        }

        if (flameStrength <= 0)
        {
            spriteR.sprite = null;
        }

        if (flameStrength > 0 && flameStrength < 22)
        {
            spriteR.sprite = firstSprites[flameStrength-1];
        }

        if (flameStrength == 22)
        {

            if (endFlame > 3)
            {
                endFlame = 0;
            }
                
            spriteR.sprite = finalSprites[endFlame];

            if (countDelay > countAmount)
            {
                endFlame += 1;
                countDelay = 0;
            }
            
            countDelay += 1;
        }

    }

}


    //void SpawnEnemy(Vector3 cursorLocationWorld)
    //{
    //    Instantiate(enemy, cursorLocationWorld, Quaternion.Euler(0, 0, 0));
    //}



