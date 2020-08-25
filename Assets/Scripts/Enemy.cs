using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator myAnimator = default;

    public int maxHealth = 100;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        myAnimator = GetComponent<Animator>();
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        //play hurt animation
        myAnimator.SetTrigger("Enemy1Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died");
        //die animation ili pošto nemamo die animaciju samo uništimo objekat
        Destroy(gameObject);
        
        //disable the enemy


    }
}
