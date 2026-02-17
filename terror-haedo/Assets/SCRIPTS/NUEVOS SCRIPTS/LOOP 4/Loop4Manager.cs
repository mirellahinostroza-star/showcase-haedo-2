using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop4Manager : MonoBehaviour
{
    [Header("Referencias")]
    public LoopManager loopManager;
    public Light[] lightsToControl;
    public GameObject[] hazardBlocks;
    public Transform loop4SpawnPoint;
    public GameObject startTrigger;

    [Header("Tiempos")]
    public float blackoutDuration = 2f;

    private bool blackoutRunning = false;

    void Start()
    {
        ResetEnvironment();
    }

    public void StartBlackout()
    {
        if (!blackoutRunning)
            StartCoroutine(BlackoutRoutine());
    }

    IEnumerator BlackoutRoutine()
    {
        blackoutRunning = true;

        foreach (var l in lightsToControl)
            if (l != null) l.enabled = false;

        yield return new WaitForSeconds(blackoutDuration);

        foreach (var l in lightsToControl)
            if (l != null) l.enabled = true;

        foreach (var b in hazardBlocks)
            SetBlockVisible(b, true);

        blackoutRunning = false;
    }

    // 🔴 MÉTODO OFICIAL DE DERROTA LOOP 4
    public void PlayerFailed()
    {
        Debug.Log("[Loop4] Derrota registrada");

        // 1️⃣ Registrar derrota GLOBAL
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddFail();
        }
        else
        {
            Debug.LogError("GameManager no existe en la escena.");
        }

        // 2️⃣ Resetear entorno
        ResetEnvironment();

        // 3️⃣ Ajustar spawn
        if (loop4SpawnPoint != null && loopManager != null)
            loopManager.SetSpawnPoint(loop4SpawnPoint.position);

        // 4️⃣ Respawn centralizado
        if (loopManager != null)
            loopManager.RespawnPlayer();
    }

    private void ResetEnvironment()
    {
        foreach (var l in lightsToControl)
            if (l != null) l.enabled = true;

        foreach (var b in hazardBlocks)
            SetBlockVisible(b, false);

        if (startTrigger != null)
            startTrigger.SetActive(true);
    }

    private void SetBlockVisible(GameObject block, bool visible)
    {
        if (block == null) return;

        var renderers = block.GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
            r.enabled = visible;
    }
}
