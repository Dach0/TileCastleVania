using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Player player;
    public Transform attackPoint;
    public Transform upAttackPoint;
    public LayerMask enemyLayers;

    
    public float attackRange = 0.5f;
    public float upAttackRangeX = 0.5f;
    public float upAttackRangeY = 0.5f;
    public int attackDamage = 40;

    public float fastAttackRate = 3f; //koliko puta možemo da napadnemo u sekundi
    public float slowAttackRate = 2f; //koliko puta možemo da napadnemo u sekundi
    float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            AttackStab();
            AttackStabUp();
        }
    }


    private void AttackStab()
    {
        if (Input.GetButtonDown("AttackStab"))
        {
            player.myAnimator.SetTrigger("IsAttackingStab");
            //ovaj physics 2d overlap detektuje sve ono sa ;im je u dodiru a mi smo mu definisali u zagradama. Ovo će biti onaj naš attackPoint koji je child 
            // našeg plejera a odnosi se na mjesto gdje treba mač da mu udari. Dali smo mu poziciju tog attackPointa, radijus kruga od centra te tačke i sa kojim lejerima da kolajduje
            // a sve to čuvamo u varijabli hitEnemies - ona će da sačuva sve kolajdere neprijatelja koje smo udarili
            Collider2D [] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                StartCoroutine(TakeDamage(enemy));
            }
            nextAttackTime = Time.time + 1f / fastAttackRate;
        }

    }
    private void AttackStabUp()
    {
        if (Input.GetButtonDown("AttackStabUp"))
        {
            player.myAnimator.SetTrigger("IsAttackingStabUp");
            Collider2D [] hitEnemies = Physics2D.OverlapBoxAll(upAttackPoint.position, new Vector3(upAttackRangeX, upAttackRangeY),0, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                StartCoroutine(TakeDamage(enemy));
            }
            nextAttackTime = Time.time + 1f / slowAttackRate;
        }
    }

    IEnumerator TakeDamage(Collider2D enemyCollider)
    {
        yield return new WaitForSeconds(0.3f);
        enemyCollider.GetComponent<Enemy>().takeDamage(attackDamage);
    }

    
    // da bi vidjeli u editoru naš krug po kojim udara naš junak moramo da napravimo f-ju koja će nam prikazivati to dok ne podesimo stvari
        void OnDrawGizmosSelected()
        {
            if (attackPoint == null) return;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
            if (upAttackPoint == null) return;
            Gizmos.DrawWireCube(upAttackPoint.position, new Vector3(upAttackRangeX, upAttackRangeY));
        }
}
