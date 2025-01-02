using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform followTransform;
    private bool isFollowing = true; // ���� ��� ��������, ������� �� ������ �� �������

    private void Awake()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        // ���� ������ �� ���� "Player"
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            followTransform = player.transform;
            Debug.Log("������ ������� �� �������");
        }
        else
        {
            Debug.LogError("������ � ����� 'Player' �� ������! �������� ��� 'Player' ������.");
            enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (!isFollowing) return; // ���� ������ �� ������ ���������, ����������

        if (followTransform == null)
        {
            FindPlayer();
            if (followTransform == null) return;
        }

        this.transform.position = new Vector3(
            followTransform.position.x,
            0,
            this.transform.position.z
        );
    }

    // ����� ��� ���������/���������� ���������� ������
    public void SetFollowing(bool follow)
    {
        isFollowing = follow;
    }
}
