using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enem : MonoBehaviour
{
    [SerializeField] int health = 0;
    Animator animator;
    BoxCollider2D BoxCollider2D;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 1)
        {
            Dieanim();
        }
    }
    void Dieanim()
    {
        animator.SetTrigger("Dead");
    }
   void Fall()
    {
        BoxCollider2D.enabled = false;
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
