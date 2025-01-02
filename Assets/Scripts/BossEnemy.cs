using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : MonoBehaviour
{
    public int life = 4;

    public Animator anim;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void ChangeLife(int damage)
    {
        life += damage;
    }
    public void AttackRed() {
        anim.SetBool("Attack", true);
    }
    public void InEnemyAttack()
    {
        playerController.ChangeLife(-3);
        anim.SetBool("Attack", false);

    }
    public void TakeDamage(int damage)
    {
        life -= damage; 
    }

}
