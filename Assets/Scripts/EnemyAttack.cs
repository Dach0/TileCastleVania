using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;
    public Enemy enemy;
    public float agroRange = 3f;

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (gameObject.CompareTag("Enemy1"))
        {
            Enemy1Attack(distToPlayer);
        }
        
        if (gameObject.CompareTag("Enemy2"))
        {
            Enemy2Attack(distToPlayer);
        }



    }

    private void Enemy1Attack(float distanceToPlayer)
    {
        if (distanceToPlayer < agroRange && ((transform.position.x < player.position.x && transform.localScale.x >0) || (transform.position.x > player.position.x && transform.localScale.x < 0)))
        {
            enemy.myAnimator.SetBool("IsAttacking", true);
        }
        else
        {
            enemy.myAnimator.SetBool("IsAttacking", false);
        }
    }
    
    private void Enemy2Attack(float distanceToPlayer)
    {
        if (distanceToPlayer < agroRange)
        {
            if (transform.position.x < player.position.x)
            {
                enemy.myAnimator.SetBool("IsAttackingRight", true);
                enemy.myAnimator.SetBool("IsAttackingLeft", false);
            }
            else
            {
                enemy.myAnimator.SetBool("IsAttackingRight", false);
                enemy.myAnimator.SetBool("IsAttackingLeft", true);
            }
        }
        else
        {
            enemy.myAnimator.SetBool("IsAttackingRight", false);
            enemy.myAnimator.SetBool("IsAttackingLeft", false);
        }
    }

    public void TurnOnLeftBiteCollider()
    {
        GetComponent<PolygonCollider2D>().enabled = true;
    }
    public void TurnOffLeftBiteCollider()
    {
        GetComponent<PolygonCollider2D>().enabled = false;
    }
    
    public void TurnOnRightBiteCollider()
    {
        GetComponent<EdgeCollider2D>().enabled = true;
    }
    public void TurnOffRightBiteCollider()
    {
        GetComponent<EdgeCollider2D>().enabled = false;
    }
}
