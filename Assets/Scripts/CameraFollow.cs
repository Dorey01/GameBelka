// ������ ��� ���������� ������ �� �������
using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // ��������� �������, �� ������� ������� ������
    public Transform followTransform;
    // �������� ������
    public Camera mainCamera;
    // �������� ������ �� ����� ����-�����
    public Vector3 bossOffset = new Vector3(2f, 1f, 0f);
    // ������� ��� ������
    public Vector3 reset = new Vector3(0f, 0f, 0f);
    // ����������� ������ ������
    public float defaultSize = 5f;
    // ������ ������ �� ����� ����-�����
    public float bossSize = 3f;
    // ���� ����-�����
    public bool isBossFight = false;
    public bool isBossFight1 = false;

    private void Awake()
    {

        // ���������, ���� �� ������ ������ � ���� ��������
        CameraFollow[] cameras = FindObjectsOfType<CameraFollow>();
        if (cameras.Length > 1 && (SceneManager.GetActiveScene().name != "Menu" && SceneManager.GetActiveScene().name != "NewHistory" && SceneManager.GetActiveScene().name != "History2" && SceneManager.GetActiveScene().name != "TheEndHistory"))
        {
            // ���� ��� ������ �� ������ ���������, ������� �
            if (cameras[0] != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        // ������������� �������� ������
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            mainCamera = GetComponent<Camera>();
            if (mainCamera == null)
            {
                Debug.LogError("������ �� �������!");
                enabled = false;
                return;
            }
        }

        // ������������� ��������� ����������
        mainCamera.orthographicSize = defaultSize;
    }
   

    private void Start()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        // ���������, ��� �� �� � ������� ����
        if (SceneManager.GetActiveScene().name != "Menu" && SceneManager.GetActiveScene().name != "NewHistory" && SceneManager.GetActiveScene().name != "History2" && SceneManager.GetActiveScene().name != "TheEndHistory")
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                followTransform = player.transform;
                Debug.Log("����� ������� ������");
            }
            else
            {
                Debug.LogError("������ � ����� 'Player' �� ������!");
                enabled = false;
            }
        }
    }
    private void Update()
    {
        // ��������� ������� ������
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
                0,  // ������ ������ ������ � �� ������������ ���������
                transform.position.z
            );
            targetSize = defaultSize;
        }

        // ������� ����������� ������
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);

        // ������� ��������� ������� ������
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, Time.deltaTime * 3f);
    }

    // ��������� ����� ��� ���������� ������
    public void SetSmallCamera()
    {
        isBossFight = true;
    }

    // ������������ ����� ������ ������
    public void ResetCam()
    {
        mainCamera = Camera.main;
        isBossFight = false;
        FindPlayer();
    }

}