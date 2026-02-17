using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Progreso Global")]
    public int totalFails = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddFail()
    {
        totalFails++;
        Debug.Log("Derrotas acumuladas: " + totalFails);
    }

    // Alias opcional por coherencia futura
    public void RegisterFail()
    {
        AddFail();
    }

    public void ResetFails()
    {
        totalFails = 0;
    }
}
