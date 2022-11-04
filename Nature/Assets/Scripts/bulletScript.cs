using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public Transform bulletRotate;
    public Rigidbody2D bullet;
    public Collider2D bulletCollider;
    public float speed = 10;
    public float duration = 200;
    public LayerMask Ground;
    public float bulletSpread = 0.1f;
    public bool canDestroybullet;

    // Start is called before the first frame update
    void Start()
    {
        float yRand = bulletSpread * (Random.value - 0.5f);
        bullet.velocity = new Vector3(-1f,yRand,0f) * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        duration--;
        if (duration <= 0)
        {
            Destroy(gameObject);
        }

        if (bulletCollider.IsTouchingLayers(Ground))
        {
            Destroy(gameObject);
        }

        if (playerHealth.playerGotHit)
        {
            canDestroybullet = true;
        }
        else
        {
            canDestroybullet = false;
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "airBlastBox" && PlayerMovement.isAirblast)
        {
            bulletRotate.eulerAngles = new Vector3(0f, 0f, aimScript.theta);

            if (aimScript.flip)
            {
                bullet.velocity = new Vector3(Mathf.Cos(Mathf.Deg2Rad * aimScript.theta), Mathf.Sin(Mathf.Deg2Rad * aimScript.theta), 0f) * -speed;
                bulletRotate.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                bullet.velocity = new Vector3(Mathf.Cos(Mathf.Deg2Rad * aimScript.theta), Mathf.Sin(Mathf.Deg2Rad * aimScript.theta), 0f) * speed;
                bulletRotate.localScale = new Vector3(-1f, 1f, 1f);
            }

        }

        if (collision.name == "PlayerSprite" && canDestroybullet)
        {

            Destroy(gameObject);
        }
    }
}
