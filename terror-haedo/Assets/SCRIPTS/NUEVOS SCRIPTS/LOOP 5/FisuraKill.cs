using UnityEngine;

public class FisuraKill : MonoBehaviour
{
    [Header("Referencias")]
    public Loop5Manager loop5Manager; // 🔴 Asignar desde inspector

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("☠ El jugador tocó la Fisura");

        if (loop5Manager != null)
            loop5Manager.PlayerFailed();
    }
}
