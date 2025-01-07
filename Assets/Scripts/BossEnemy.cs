using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Контроллер босса-противника.
/// </summary>
public class BossEnemy : MonoBehaviour
{
    /// <summary>
    /// Количество жизней босса.
    /// </summary>
    public int life;

    /// <summary>
    /// Компонент аниматора.
    /// </summary>
    public Animator anim;

    /// <summary>
    /// Ссылка на контроллер игрока.
    /// </summary>
    public PlayerController playerController;

    /// <summary>
    /// Ссылка на компонент PlayerBossF.
    /// </summary>
    public PlayertBossF playerBossF;

    /// <summary>
    /// Инициализация компонентов.
    /// </summary>
    private void Start()
    {
        this.anim = this.GetComponent<Animator>();
    }

    /// <summary>
    /// Обновление состояния босса.
    /// </summary>
    public void Update()
    {
        if (life <= 0)
        {
            Destroy(this.gameObject);
            playerBossF.BossDaead();
            
        }
    }

    /// <summary>
    /// Изменяет количество жизней босса.
    /// </summary>
    /// <param name="damage">Величина изменения жизней.</param>
    public void ChangeLife(int damage)
    {
        this.life += damage;
    }

    /// <summary>
    /// Активирует анимацию атаки босса.
    /// </summary>
    public void AttackRed()
    {
        this.anim.SetBool("Attack", true);
    }

    /// <summary>
    /// Обрабатывает попадание атаки босса по игроку.
    /// </summary>
    public void InEnemyAttack()
    {
        this.playerController.ChangeLife(-1);
        this.anim.SetBool("Attack", false);
    }

    /// <summary>
    /// Наносит урон боссу.
    /// </summary>
    /// <param name="damage">Величина урона.</param>
    public void TakeDamage(int damage)
    {
        this.life -= damage;
    }
}
