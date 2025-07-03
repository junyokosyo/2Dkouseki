using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] int attackDamage = 2;
    [SerializeField] LayerMask enemyLayer;
    private float cooldown = 3f;
    float count = 5;
    Animator animator;
    public Transform attackPoint;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if (Input.GetButtonDown("Fire1"))
        {
            if (count > cooldown)
            {
                count = 0;
                animator.SetTrigger("Attack");
            }

        }
    }

    void attack()
    {
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enem>().TakeDamage(attackDamage);
            
            
        }
        
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;


        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
