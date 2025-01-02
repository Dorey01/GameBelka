using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    private GameObject cam;
    public float prallaxEffect;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        FindCamera();

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer �� ������!");
            enabled = false;
            return;
        }

        startpos = transform.position.x;
        length = spriteRenderer.bounds.size.x;
    }

    private void FindCamera()
    {
        // ���� ������ �� ���� MainCamera
        cam = GameObject.FindWithTag("MainCamera");
        if (cam == null)
        {
            Debug.LogError("������ � ����� 'MainCamera' �� �������!");
            enabled = false;
        }
        else
        {
            Debug.Log("������ ������� ��� ����������");
        }
    }

    void FixedUpdate()
    {
        if (cam == null)
        {
            FindCamera();
            if (cam == null) return;
        }

        float temp = (cam.transform.position.x * (1 - prallaxEffect));
        float dist = (cam.transform.position.x * prallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}