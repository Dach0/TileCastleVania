using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Player player;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    
    public float attackRange = 0.5f;
    public int attackDamage = 40;

    // Update is called once per frame
    void Update()
    {
        AttackStab();
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
                enemy.GetComponent<Enemy>().takeDamage(attackDamage);
            }
        }

    }
        // da bi vidjeli u editoru naš krug po kojim udara naš junak moramo da napravimo f-ju koja će nam prikazivati to dok ne podesimo stvari
        void OnDrawGizmosSelected()
        {
            if (attackPoint == null) return;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
}
