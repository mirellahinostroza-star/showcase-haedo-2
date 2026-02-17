using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    [Header("Player Settings")]
    public GameObject player;
    public Transform spawnPoint;

    [Header("Loops")]
    public GameObject[] loops;

    [HideInInspector] public int currentLoop = 0;

    // 🔔 Evento para notificar a la UI
    public static Action<int> OnFailUpdated;

    private Vector3 initialSpawnPosition;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        if (spawnPoint != null)
            initialSpawnPosition = spawnPoint.position;
        else if (player != null)
            initialSpawnPosition = player.transform.position;

        ActivateLoop(currentLoop);

        // 🔄 Notificar valor actual desde GameManager
        if (GameManager.Instance != null)
            OnFailUpdated?.Invoke(GameManager.Instance.totalFails);
    }

    // 🔴 Registrar derrota
    public void RegisterFail()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterFail();

            Debug.Log("❌ Derrotas globales: " + GameManager.Instance.totalFails);

            // Notificar UI
            OnFailUpdated?.Invoke(GameManager.Instance.totalFails);
        }
        else
        {
            Debug.LogWarning("GameManager no encontrado.");
        }
    }

    public void ResetToFirstLoop()
    {
        currentLoop = 0;
        RespawnPlayer();
        ActivateLoop(currentLoop);
    }

    public void AdvanceLoop()
    {
        currentLoop++;

        if (currentLoop >= loops.Length)
        {
            Debug.Log("🎉 ¡Ganaste todos los loops!");
            currentLoop = loops.Length - 1;
            return;
        }

        RespawnPlayer();
        ActivateLoop(currentLoop);
    }

    public void RespawnPlayer()
    {
        if (player == null) return;

        CharacterController controller = player.GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.enabled = false;
            player.transform.position = initialSpawnPosition;
            controller.enabled = true;
        }
        else
        {
            player.transform.position = initialSpawnPosition;
        }
    }

    public void SetSpawnPoint(Vector3 newSpawn)
    {
        initialSpawnPosition = newSpawn;
    }

    private void ActivateLoop(int index)
    {
        for (int i = 0; i < loops.Length; i++)
        {
            if (loops[i] != null)
                loops[i].SetActive(i == index);
        }

        Debug.Log("Loop activo: " + index);
    }

    public void ResetCurrentLoop()
    {
        RespawnPlayer();
    }
}
