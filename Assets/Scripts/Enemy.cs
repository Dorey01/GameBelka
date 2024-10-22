using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

    }
}
