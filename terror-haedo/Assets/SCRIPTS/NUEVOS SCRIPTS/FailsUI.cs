using UnityEngine;
using TMPro;

public class FailsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI failsText;

    private void OnEnable()
    {
        LoopManager.OnFailUpdated += UpdateUI;
    }

    private void OnDisable()
    {
        LoopManager.OnFailUpdated -= UpdateUI;
    }

    private void UpdateUI(int newFailCount)
    {
        if (failsText != null)
            failsText.text = "Muertes: " + newFailCount;
    }
}
