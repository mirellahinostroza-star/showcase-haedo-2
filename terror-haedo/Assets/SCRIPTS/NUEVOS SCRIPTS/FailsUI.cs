using UnityEngine;
using TMPro;

public class FailsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI failsText;

    private int lastValue = -1;

    void Update()
    {
        if (GameManager.Instance == null) return;

        int currentFails = GameManager.Instance.totalFails;

        if (currentFails != lastValue)
        {
            lastValue = currentFails;
            failsText.text = "Muertes: " + currentFails;
        }
    }
}
