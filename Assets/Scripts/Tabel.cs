using UnityEngine;

public class Tabel : MonoBehaviour
{
    #region Переменные
    [SerializeField] private string tabelType; // Тип таблички (Jump, DoubleJump, Sheshka, Prig)
    private PlayerController player;
    private bool isActivated = false;
    
    // Позиция чекпоинта
    private Vector3 checkpointPosition;
    #endregion

    private void Start()
    {
        checkpointPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActivated)
        {
            player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                ActivateAbility();
                SetCheckpoint();
            }
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
            default:
                Debug.LogWarning($"Неизвестный тип таблички: {tabelType}");
                break;
        }
        isActivated = true;
    }

    private void ActivateJump()
    {
        if (player != null)
        {
            player.UnlockJump();
            ShowActivationMessage("Прыжок разблокирован!");
        }
    }

    private void ActivateDoubleJump()
    {
        if (player != null)
        {
            player.UnlockDoubleJump();
            ShowActivationMessage("Двойной прыжок разблокирован!");
        }
    }

    private void ActivateShooting()
    {
        if (player != null)
        {
            player.UnlockShooting();
            ShowActivationMessage("Стрельба разблокирована!");
        }
    }

    private void ActivateWallJump()
    {
        if (player != null)
        {
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