using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enem : MonoBehaviour
{
    [SerializeField] int health = 0;
    // Start is called before the first frame update
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 1)
        {
            Die();
        }
    }
    private void Update()
    {
        Debug.Log(health);
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
