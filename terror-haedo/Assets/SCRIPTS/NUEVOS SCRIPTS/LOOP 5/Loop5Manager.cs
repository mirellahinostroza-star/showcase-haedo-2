using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop5Manager : MonoBehaviour
{
    [Header("Referencias")]
    public LoopManager loopManager;
    public WeepingAngelAI angel;
    public GameObject startTrigger;
    public Transform loop5SpawnPoint;

    [Header("Configuración")]
    public bool autoActivateAngel = true;

    private void Start()
    {
        ResetLoop5Environment();
    }

    // 🔵 INICIAR LOOP 5
    public void StartLoop5()
    {
        Debug.Log("[Loop5] Iniciando Loop 5...");

        if (angel != null && autoActivateAngel)
            angel.ActivateAngel();

        if (startTrigger != null)
            startTrigger.SetActive(false);
    }

    // 🔴 DERROTA OFICIAL LOOP 5
    public void PlayerFailed()
    {
        Debug.Log("[Loop5] Derrota registrada");

        // 1️⃣ Registrar derrota GLOBAL
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddFail();
        }
        else
        {
            Debug.LogError("GameManager no existe en la escena.");
        }

        // 2️⃣ Resetear entorno completo
        ResetLoop5();
    }

    // 🔁 RESET COMPLETO DEL LOOP
    public void ResetLoop5()
    {
        // Reset del Angel
        if (angel != null)
        {
            angel.DeactivateAngel();

            var nav = angel.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (nav) nav.ResetPath();
        }

        // Ajustar spawn
        if (loop5SpawnPoint != null && loopManager != null)
            loopManager.SetSpawnPoint(loop5SpawnPoint.position);

        // Respawn centralizado
        if (loopManager != null)
            loopManager.RespawnPlayer();

        // Reactivar trigger
        if (startTrigger != null)
            startTrigger.SetActive(true);

        Debug.Log("[Loop5] Loop 5 reseteado correctamente");
    }

    private void ResetLoop5Environment()
    {
        if (startTrigger != null)
            startTrigger.SetActive(true);
    }
}
