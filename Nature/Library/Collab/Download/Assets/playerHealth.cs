using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public float maxHealth = 200;
    public float currentHealth;

    public HealthBar healthBar;

    public Transform playerLocation;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D playerCollidedWith = Physics2D.OverlapBox(new Vector2(playerLocation.position.x - 0.2f, playerLocation.position.y + 0.1f), new Vector2(2f, 4.5f), 0f);
        if (playerCollidedWith)
        {
            Debug.Log(playerCollidedWith.name);
            if (playerCollidedWith.name == "bulletPrefab(Clone)") //if player is hit by bullet, lose hp
            {
                currentHealth = (float)(currentHealth - 0.1);
                healthBar.SetHealth(currentHealth);
                Debug.Log("bulletPrefab" + currentHealth);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) //for testing (should be fixed update)
        {
            TakeDamage(10);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void FixedUpdate()
    {
        Collider2D playerCollidedWith = Physics2D.OverlapBox(new Vector2(playerLocation.position.x - 0.2f, playerLocation.position.y + 0.1f), new Vector2(2f, 4.5f), 0f);

        if (playerCollidedWith)
        {
            if (playerCollidedWith.name == "bulletPrefab") //if player is hit by bullet, lose hp
            {
                TakeDamage(1); 
                currentHealth--;
                Debug.Log(currentHealth);
            }
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
