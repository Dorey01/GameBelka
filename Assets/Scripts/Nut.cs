using UnityEngine;
using UnityEngine.UI;

public class NutS : MonoBehaviour
{
    private float Nut = 0;
    public Text NutText;
    private AudioSource NutSound;
    private int levelID = 1;
    private NutDatabase database;

    void Start()
    {
        database = new NutDatabase();
        database.InitializeTableData();
        LoadNutCount();
    }

    private void LoadNutCount()
    {
        Nut = database.LoadNutCount(levelID);
        NutText.text = Nut.ToString();
        Debug.Log($"Загружено орехов: {Nut}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Nut"))
        {
            Nut++;
            NutText.text = Nut.ToString();
            if (NutSound != null)
            {
                NutSound.Play();
            }
            database.SaveNutCount(levelID, Nut);
            Destroy(collision.gameObject);
        }
    }

    private void OnDestroy()
    {
        database.SaveNutCount(levelID, Nut);
    }
}