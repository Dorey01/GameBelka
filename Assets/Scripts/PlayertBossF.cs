using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayertBossF : MonoBehaviour
{
    private PlayerController playerController;
    private CameraFollow cameraFollow;
    public Transform groundCheck;
    public Camera camera;
    public GameObject belcka;
    public GameObject bullet;
    public Transform shotPoint;
    public BossEnemy bossEnemy;

    public float speed = 2f;
    private float startX;
    private float endX;
    private bool movingLeft = true;


    void Start()
    {
        startX = belcka.transform.position.x;
        endX = 560;
        playerController = GetComponent<PlayerController>();
        cameraFollow = camera.GetComponent<CameraFollow>();
    

        if (cameraFollow == null)
        {
            Debug.LogError("Не найден компонент скрипта CameraFollow!");
        }
    }

    private void Update()
    {
        // Движение белки
        Vector3 currentPosition = belcka.transform.position;

        if (movingLeft)
        {
            currentPosition.x -= speed * Time.deltaTime;
            if (currentPosition.x <= endX)
            {
                movingLeft = false;
            }
        }
        else
        {
            currentPosition.x += speed * Time.deltaTime;
            if (currentPosition.x >= startX)
            {
                movingLeft = true;
            }
        }

        belcka.transform.position = currentPosition;

        // Проверка нажатия Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckCurrentZone();
        }

        // Проверка столкновения с боссом
        Collider2D[] collidersGround = Physics2D.OverlapCircleAll(groundCheck.position, 0.1f);
        foreach (Collider2D collider in collidersGround)
        {
            if (collider.CompareTag("Boss"))
            {
            
                cameraFollow.SetFollowing(false);
                StartCoroutine(AdjustCameraSizeAndPosition(3f, 3f));
            }
        }
    }

    private void CheckCurrentZone()
    {
        Collider2D[] overlappingColliders = Physics2D.OverlapCircleAll(belcka.transform.position, 0.1f);
        bool isInGreen = false;
        bool isInYellow = false;
        bool isInRed = false;

        // Проверяем наличие обеих зон
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
        
        // Создаем пулю и устанавливаем её зону
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            if (isInRed)
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

                    // Устанавливаем направление пули (предполагается, что оно нужно)
                    Vector2 shootDirection = Vector2.right; // или любое другое направление
                    bulletScript.SetDirection(shootDirection);
                }
            }
            else
            {
                bossEnemy.AttackRed();
            }
        }
 
    }

    private IEnumerator AdjustCameraSizeAndPosition(float targetSize, float duration)
    {
        float startSize = camera.orthographicSize;
        Vector3 startPosition = camera.transform.position;
        Vector3 offset = new Vector3(2f, 1f, 0f);

        Vector3 targetPosition = new Vector3(
            playerController.transform.position.x,
            playerController.transform.position.y,
            camera.transform.position.z
        ) + offset;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            camera.orthographicSize = Mathf.Lerp(startSize, targetSize, elapsed / duration);
            camera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            yield return null;
        }

        camera.orthographicSize = targetSize;
        camera.transform.position = targetPosition;
    }
}