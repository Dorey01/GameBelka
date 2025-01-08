using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
Обоснование использования паттерна State (Состояние):

1. Четкое разделение поведения: В игре есть два четких состояния - нормальное и боевое,
   каждое со своей логикой и поведением.

2. Улучшение поддерживаемости: Каждое состояние инкапсулировано в отдельном классе,
   что делает код более организованным и легким для модификации.

3. Устранение сложных условных конструкций: Вместо множества проверок if/else,
   каждое состояние само определяет свое поведение.

4. Расширяемость: Легко добавить новые состояния (например, состояние победы или поражения)
   без изменения существующего кода.

5. Безопасные переходы: Гарантируется корректный вход и выход из каждого состояния
   благодаря методам EnterState и ExitState.
*/

public interface IBattleState
{
    void Update(PlayertBossF context);
    void EnterState(PlayertBossF context);
    void ExitState(PlayertBossF context);
}

public class PlayertBossF : MonoBehaviour
{
    // Состояния
    private IBattleState normalState;
    private IBattleState battleState;
    private IBattleState currentState;

    public PlayerController playerController;
    public CameraFollow cameraFollow;
    public Transform groundCheck;
    public GameObject belcka;
    public GameObject bullet;
    public GameObject sound;
    public GameObject soundBoss;
    public Transform shotPoint;
    public BossEnemy bossEnemy;
    public GameObject Boss;
    public GameObject scrol;
    public GameObject HUD;

    public float speed = 2f;
    public bool movingLeft = true;
    public bool fight = false;
    public bool bossDead = false;
    public Vector3 currentPosition;

    private void Start()
    {
        // Инициализация состояний
        normalState = new NormalState();
        battleState = new BattleState();
        currentState = normalState;

        scrol.SetActive(false);
        playerController = GetComponent<PlayerController>();
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }

    private void Update()
    {
        if (bossDead)
        {
            TransitionToState(normalState);
        }
        currentState.Update(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!bossDead && collision.CompareTag("Boss"))
        {
            TransitionToState(battleState);
            soundBoss.SetActive(true);
            sound.SetActive(false);
            Vector3 bossPosition = Boss.transform.position;
            bossPosition.x = playerController.transform.position.x + 2.4f;
            Boss.transform.position = bossPosition;
            Boss.SetActive(true);
            scrol.SetActive(true);
        }
    }

    public void BossDaead()
    {
        soundBoss.SetActive(false);
        sound.SetActive(true);
        TransitionToState(normalState);
        cameraFollow.ResetCam();
        scrol.SetActive(false);
        bossDead = true;
        if (SceneManager.GetActiveScene().name == "Level2.2")
        {
            SceneManager.LoadScene("TheEndHistory"); // или другую сцену
        }
    }

    private void TransitionToState(IBattleState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void CheckCurrentZone()
    {
        Collider2D[] overlappingColliders = Physics2D.OverlapCircleAll(belcka.transform.position, 0.1f);
        bool isInGreen = false;
        bool isInYellow = false;
        bool isInRed = false;

        foreach (Collider2D collider in overlappingColliders)
        {
            if (collider.CompareTag("Green"))
            {
                isInGreen = true;
            }
            else if (collider.CompareTag("Yellow"))
            {
                isInYellow = true;
            }
            else if (collider.CompareTag("Red"))
            {
                isInRed = true;
            }
        }

        if (!isInRed)
        {
            GameObject spawnedBullet = Instantiate(bullet, shotPoint.position, shotPoint.rotation);
            if (spawnedBullet.TryGetComponent<Bullet>(out Bullet bulletScript))
            {
                if (isInGreen)
                {
                    Debug.Log("Белка находится в зеленой зоне!");
                    bulletScript.SetZone("Green");
                }
                else if (isInYellow)
                {
                    Debug.Log("Белка находится в желтой зоне!");
                    bulletScript.SetZone("Yellow");
                }

                Vector2 shootDirection = Vector2.right;
                bulletScript.SetDirection(shootDirection);
            }
        }
        else
        {
            bossEnemy.AttackRed();
        }
    }
}

public class NormalState : IBattleState
{
    public void Update(PlayertBossF context)
    {
        context.playerController.blockMoveBoss = false;
        context.fight = false;
    }

    public void EnterState(PlayertBossF context)
    {
        context.fight = false;
        context.playerController.blockMoveBoss = false;
    }

}

public class BattleState : IBattleState
{
    private float nextFireTime = 0f;
    private float fireRate = 1f;

    public void Update(PlayertBossF context)
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            context.CheckCurrentZone();
        }

        Vector3 currentPosition = context.belcka.transform.position;
        float minX = context.HUD.transform.position.x - 330f;
        float maxX = context.HUD.transform.position.x + 390f;

        if (context.movingLeft)
        {
            currentPosition.x -= context.speed * Time.deltaTime;
            if (currentPosition.x <= minX)
            {
                currentPosition.x = minX;
                context.movingLeft = false;
            }
        }
        else
        {
            currentPosition.x += context.speed * Time.deltaTime;
            if (currentPosition.x >= maxX)
            {
                currentPosition.x = maxX;
                context.movingLeft = true;
            }
        }

        context.playerController.JumpAndSpeed();
        context.belcka.transform.position = currentPosition;
    }

    public void EnterState(PlayertBossF context)
    {
        context.fight = true;
        context.playerController.blockMoveBoss = true;
        context.cameraFollow.SetSmallCamera();
    }

    public void ExitState(PlayertBossF context)
    {
        context.fight = false;
        context.playerController.blockMoveBoss = false;
    }
}
