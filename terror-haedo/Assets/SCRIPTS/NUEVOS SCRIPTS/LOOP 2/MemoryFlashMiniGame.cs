using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryFlashMiniGame : MonoBehaviour
{
    [Header("Referencias")]
    public LoopManager loopManager;
    public MemoryFlashBlocker blocker;
    public MemoryFlashLight[] lights;

    [Header("Configuración")]
    public float flashTime = 0.8f;
    public float intervalTime = 0.4f;
    public int sequenceLength = 4;

    private List<MemoryFlashLight> sequence = new();
    private List<MemoryFlashLight> playerInput = new();

    private bool inputEnabled = false;
    private bool inGame = false;

    public bool IsInputEnabled()
    {
        return inputEnabled;
    }

    private void Start()
    {
        ResetMiniGame();
    }

    public void StartMiniGame()
    {
        if (!inGame)
            StartCoroutine(RunMiniGame());
    }

    IEnumerator RunMiniGame()
    {
        inGame = true;
        inputEnabled = false;

        sequence.Clear();
        playerInput.Clear();

        for (int i = 0; i < sequenceLength; i++)
        {
            sequence.Add(lights[Random.Range(0, lights.Length)]);
        }

        foreach (var light in sequence)
        {
            if (light.gameObject.activeInHierarchy)
                light.Flash(flashTime);

            yield return new WaitForSeconds(flashTime + intervalTime);
        }

        inputEnabled = true;
    }

    public void RegisterPlayerClick(MemoryFlashLight clickedLight)
    {
        if (!inputEnabled) return;

        playerInput.Add(clickedLight);
        int index = playerInput.Count - 1;

        if (playerInput[index] != sequence[index])
        {
            OnLose();
            return;
        }

        if (playerInput.Count == sequence.Count)
        {
            OnWin();
        }
    }

    private void OnWin()
    {
        inputEnabled = false;
        inGame = false;

        if (blocker != null)
            blocker.UnlockPath();
    }

    private void OnLose()
    {
        Debug.Log("[MemoryFlash] Secuencia incorrecta. Reiniciando Loop-2.");

        inputEnabled = false;
        inGame = false;

        // 🔴 Registrar derrota global (actualiza UI automáticamente)
        if (loopManager != null)
        {
            loopManager.RegisterFail();
        }
        else
        {
            Debug.LogWarning("MemoryFlashMiniGame: LoopManager no asignado.");
        }

        // 🔥 Resetear ambos pasillos dentro de Loop-2
        Transform loopRoot = transform;

        while (loopRoot.parent != null && loopRoot.name != "Loop-2")
        {
            loopRoot = loopRoot.parent;
        }

        if (loopRoot.name == "Loop-2")
        {
            MemoryFlashMiniGame[] miniGames =
                loopRoot.GetComponentsInChildren<MemoryFlashMiniGame>(true);

            foreach (var game in miniGames)
            {
                game.ResetMiniGame();
            }
        }
        else
        {
            Debug.LogWarning("No se encontró Loop-2 en la jerarquía.");
        }

        // 🔁 Respawn jugador
        if (loopManager != null)
            loopManager.RespawnPlayer();
    }

    public void ResetMiniGame()
    {
        StopAllCoroutines();

        sequence.Clear();
        playerInput.Clear();

        inputEnabled = false;
        inGame = false;

        if (blocker != null)
            blocker.ResetBlocker();
    }
}
