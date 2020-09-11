using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform castPoint = default;
    public Enemy enemy;
    public float agroRange = 3f;

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer(agroRange))
        {
            CheckEnemyAndAttack();
        }
        else
        {
            CheckEnemyAndStopAttacking();
        }
    }

    private void CheckEnemyAndStopAttacking()
    {
        if (gameObject.CompareTag("Enemy1"))
        {
            Enemy1StopAttacking();
        }

        if (gameObject.CompareTag("Enemy2"))
        {
            Enemy2StopAttacking();
        }
    }

    private void CheckEnemyAndAttack()
    {
        if (gameObject.CompareTag("Enemy1"))
        {
            Enemy1Attack();
        }

        if (gameObject.CompareTag("Enemy2"))
        {
            Enemy2Attack();
        }
    }

    bool CanSeePlayer(float distance)
    {
        bool val = false;

        float castDistance = distance;
        if(transform.localScale.x <= 0)
        {
            castDistance = -distance;
        }

        Vector2 endPos = castPoint.position + Vector3.right * castDistance; // isto kao da smo napisali new Vector3(position.x + distance)
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));   // ovaj raycast gleda na to da li on ima kontakta sa layerom Action

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }

            Debug.DrawLine(castPoint.position, hit.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.blue);
        }

        return val;
    }

    private void Enemy1Attack()
    {
            enemy.myAnimator.SetBool("IsAttacking", true);
    }

    private void Enemy1StopAttacking()
    {
        enemy.myAnimator.SetBool("IsAttacking", false);
    }
    
    private void Enemy2Attack()
    {

            if (FindObjectOfType<Player>().transform.position.x < transform.position.x)
            {
                enemy.myAnimator.SetBool("IsAttackingRight", false);
                enemy.myAnimator.SetBool("IsAttackingLeft", true);
            }
            else
            {
                enemy.myAnimator.SetBool("IsAttackingRight", true);
                enemy.myAnimator.SetBool("IsAttackingLeft", false);
            }
      
    }

    private void Enemy2StopAttacking()
    { 
        enemy.myAnimator.SetBool("IsAttackingRight", false);
        enemy.myAnimator.SetBool("IsAttackingLeft", false);
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
