// Скрипт для следования камеры за игроком
using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Трансформ объекта, за которым следует камера
    public Transform followTransform;
    // Основная камера
    public Camera mainCamera;
    // Смещение камеры во время босс-файта
    public Vector3 bossOffset = new Vector3(2f, 1f, 0f);
    // Позиция для сброса
    public Vector3 reset = new Vector3(0f, 0f, 0f);
    // Стандартный размер камеры
    public float defaultSize = 5f;
    // Размер камеры во время босс-файта
    public float bossSize = 3f;
    // Флаг босс-файта
    public bool isBossFight = false;
    public bool isBossFight1 = false;
    public CameraMenu cameraMenu;

    private void Awake()
    {

        // Проверяем, есть ли другие камеры с этим скриптом
        CameraFollow[] cameras = FindObjectsOfType<CameraFollow>();
        if (cameras.Length > 1 && (SceneManager.GetActiveScene().name != "Menu" && SceneManager.GetActiveScene().name != "NewHistory" && SceneManager.GetActiveScene().name != "History2"))
        {
            // Если эта камера не первая найденная, удаляем её
            if (cameras[0] != this )
            {
                
                Destroy(gameObject);
                return;
            }
        }
        // Инициализация основной камеры
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            mainCamera = GetComponent<Camera>();
            if (mainCamera == null)
            {
                Debug.LogError("Камера не найдена!");
                enabled = false;
                return;
            }
        }

        // Инициализация начальных параметров
        mainCamera.orthographicSize = defaultSize;
    }
   

    private void Start()
    {
        cameraMenu = GetComponent<CameraMenu>();
        FindPlayer();
    }

    private void FindPlayer()
    {
        // Проверяем, что мы не в главном меню
        if (SceneManager.GetActiveScene().name != "Menu" && SceneManager.GetActiveScene().name != "NewHistory" && SceneManager.GetActiveScene().name != "History2" && SceneManager.GetActiveScene().name != "TheEndHistory")
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                followTransform = player.transform;
                Debug.Log("Игрок успешно найден");
            }
            else
            {
                Debug.LogError("Объект с тегом 'Player' не найден!");
                enabled = false;
            }
        }
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "TheEndHistory")
        {
            Destroy(gameObject);
            return;
        }
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            cameraMenu.enabled = false;
        }
        else
        {
            cameraMenu.enabled = true;
        }
        // Проверяем наличие игрока
        if (followTransform == null)
        {
            FindPlayer();
            if (followTransform == null) return;
        }

        Vector3 targetPosition;
        float targetSize;

        if (isBossFight)
        {
            targetPosition = new Vector3(
                followTransform.position.x,
                followTransform.position.y,
                transform.position.z
            ) + bossOffset;
            targetSize = bossSize;
        }
        else
        {
            targetPosition = new Vector3(
                followTransform.position.x,
                0,  // Теперь камера следит и за вертикальным движением
                transform.position.z
            );
            targetSize = defaultSize;
        }

        // Плавное перемещение камеры
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);

        // Плавное изменение размера камеры
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, Time.deltaTime * 3f);
    }

    // Добавляем метод для уменьшения камеры
    public void SetSmallCamera()
    {
        isBossFight = true;
    }

    // Модифицируем метод сброса камеры
    public void ResetCam()
    {
        mainCamera = Camera.main;
        isBossFight = false;
        FindPlayer();
    }

}
