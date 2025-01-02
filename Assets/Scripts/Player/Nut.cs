using UnityEngine;
using UnityEngine.UI;

public class NutS : MonoBehaviour
{
    private float Nut = 0;
    public Text NutText;
    private AudioSource NutSound;

    private void Awake()
    {
        // Попробуем найти NutText, если он не присвоен
        if (NutText == null)
        {
            NutText = GetComponent<Text>();
        }
        
        // Попробуем найти AudioSource, если он не присвоен
        if (NutSound == null)
        {
            NutSound = GetComponent<AudioSource>();
        }
    }

    void Start()
    {

        // Инициализируем количество орехов для текущего уровня
        Nut = 0;
        LevelManager.Instance.SaveNutCount(Nut);
        
        UpdateNutText();
    }

    private void UpdateNutText()
    {
        float totalNuts = LevelManager.Instance.GetTotalNutsExceptCurrent();
        NutText.text = ((int)totalNuts + Nut).ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Nut"))
        {
            Nut++;
            UpdateNutText();
            
            if (NutSound != null)
            {
                NutSound.Play();
            }
            Exit.Save(Nut);
            Destroy(collision.gameObject);
        }
    }
}