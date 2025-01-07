using UnityEngine;

public class Tabel : MonoBehaviour
{
    #region Переменные
    [SerializeField] private string tabelType; // Тип таблички (Jump, DoubleJump, Sheshka, Prig)
    private PlayerController player;

    public GameObject tabelAct;
    public GameObject jump;
    public GameObject jumpjump;
    public GameObject space;
    public GameObject e;
    public GameObject run;
    int _jump, _space, _jumpjump, _e;

    // Позиция чекпоинта
    private Vector3 checkpointPosition;
    #endregion

    private void Start()
    {
        checkpointPosition = transform.position;

        // Убедимся, что все подсказки выключены при старте
        if (_jump != 0) {
            jump.SetActive(false);
            _jump = 0;
        }
        if (_jumpjump != 0) { jumpjump.SetActive(false); _jumpjump = 0; }
        if (_space != 0) { space.SetActive(false); _space = 0; }
        if (_e != 0) { e.SetActive(false); _e = 0; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                ActivateAbility();
                SetCheckpoint();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Отключаем все подсказки при выходе из зоны
            if (jump != null) jump.SetActive(false); 
            if (jumpjump != null) jumpjump.SetActive(false);
            if (space != null) space.SetActive(false);
            if (e != null) e.SetActive(false);
            if (run != null) run.SetActive(false);
            tabelAct.SetActive(false);
        }
    }

    private void ActivateAbility()
    {
        switch (tabelType.ToLower())
        {
            case "jump":
                ActivateJump();
                break;
            case "doublejump":
                ActivateDoubleJump();
                break;
            case "sheshka":
                ActivateShooting();
                break;
            case "prig":
                ActivateWallJump();
                break;
            case "run":
                ActivateRun();
                break;
            default:
                Debug.LogWarning($"Неизвестный тип таблички: {tabelType}");
                break;
        }

    }

    private void ActivateJump()
    {
        jump.SetActive(true);
        _jump = 1;
        tabelAct.SetActive(true);
        if (player != null)
        {
            if (jump != null) jump.SetActive(true);
            player.UnlockJump();
            ShowActivationMessage("Прыжок разблокирован!");
        }
    }

    private void ActivateDoubleJump()
    {
        jumpjump.SetActive(true);
        _jumpjump = 1;
        tabelAct.SetActive(true);
        if (player != null)
        {
            if (jumpjump != null) jumpjump.SetActive(true);
            player.UnlockDoubleJump();
            ShowActivationMessage("Двойной прыжок разблокирован!");
        }
    }
    private void ActivateRun()
    {
        run.SetActive(true);
        tabelAct.SetActive(true);
        if (player != null)
        {
            if (run != null) run.SetActive(true);
        }
    }
    private void ActivateShooting()
    {
        e.SetActive(true);
        _e = 1;
        tabelAct.SetActive(true);
        if (player != null)
        {
            if (e != null) e.SetActive(true);
            player.UnlockShooting();
            ShowActivationMessage("Стрельба разблокирована!");
        }
    }

    private void ActivateWallJump()
    {
        space.SetActive(true);
        _space = 1;
        tabelAct.SetActive(true);
        if (player != null)
        {
            if (space != null) space.SetActive(true);
            player.UnlockWallJump();
            ShowActivationMessage("Прыжок от стены разблокирован!");
        }
    }

    private void SetCheckpoint()
    {
        if (player != null)
        {
            player.SetCheckpointPosition(checkpointPosition);
        }
    }

    private void ShowActivationMessage(string message)
    {
        Debug.Log(message);
    }
}
