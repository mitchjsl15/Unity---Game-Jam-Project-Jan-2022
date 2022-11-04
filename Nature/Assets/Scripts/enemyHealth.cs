using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;

    public HealthBar healthBar;
    public Transform enemyLocation;
    public LayerMask layers;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space)) //for testing (should be fixed update)
    //    {
     //       TakeDamage(10);
    //    }
    //}

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void FixedUpdate()
    {
        Collider2D enemyCollidedWith = Physics2D.OverlapBox(new Vector2(enemyLocation.position.x - 0.2f, enemyLocation.position.y + 0.1f), new Vector2(2f,4.5f), 0f, layers);
        //Debug.Log(enemyCollidedWith);

        if (enemyCollidedWith)
        {
            if (enemyCollidedWith.name == "flame" && aimScript.flameOn)
            {
                TakeDamage(1); //if flames are working it should lower the health
                //currentHealth--;
            }
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
