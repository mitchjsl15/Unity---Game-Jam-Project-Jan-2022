using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batHealth : MonoBehaviour
{
    public float maxHealth2 = 100;
    public float currentHealth2;

    public HealthBar healthBar2;
    public Transform enemyLocation2;
    public LayerMask layers2;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth2 = maxHealth2;
        healthBar2.SetMaxHealth(maxHealth2);

    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space)) //for testing (should be fixed update)
    //    {
    //       TakeDamage(10);
    //    }
    //}

    void TakeDamage2(int damage)
    {
        currentHealth2 -= damage;
        healthBar2.SetHealth(currentHealth2);
    }

    void FixedUpdate()
    {
        Collider2D enemyCollidedWith = Physics2D.OverlapCircle(enemyLocation2.position, 3, layers2);
        //Debug.Log(enemyCollidedWith);

        if (enemyCollidedWith)
        {
            if (enemyCollidedWith.name == "flame" && aimScript.flameOn)
            {
                TakeDamage2(1); //if flames are working it should lower the health
                //currentHealth--;
            }
        }

        if (currentHealth2 <= 0)
        {
            Destroy(gameObject);
        }
    }

}
