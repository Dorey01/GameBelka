using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ���������� ������ ����� 1 �������
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
