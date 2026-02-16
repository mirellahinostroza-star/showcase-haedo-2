using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLightGreenLightManager : MonoBehaviour
{
    [Header("Referencias generales")]
    public List<DollController> dolls;
    public float greenDuration = 3f;
    public float redDuration = 2f;

    [Header("Sonido")]
    public AudioSource redLightSound;

    [Header("Loop Manager")]
    public LoopManager loopManager; // 🔴 IMPORTANTE

    [HideInInspector] public bool isRedLight = false;

    private Coroutine cycleCoroutine;
    private bool isActive = false;

    // 🔵 ARRANCAR MINIJUEGO
    public void StartMinigame()
    {
        if (isActive) return;

        isActive = true;
        isRedLight = false;

        foreach (var doll in dolls)
            doll.SetLightState(true);

        if (redLightSound != null)
            redLightSound.Stop();

        cycleCoroutine = StartCoroutine(Cycle());
    }

    // 🔴 DERROTA OFICIAL DEL LOOP 3
    public void PlayerFailed()
    {
        if (!isActive) return;

        Debug.Log("🔴 Loop 3 - Derrota registrada");

        // 1️⃣ Registrar derrota global
        if (loopManager != null)
            loopManager.RegisterFail();

        // 2️⃣ Detener minijuego
        StopMinigame();

        // 3️⃣ Respawn centralizado
        if (loopManager != null)
            loopManager.RespawnPlayer();
    }

    // 🛑 DETENER MINIJUEGO
    public void StopMinigame()
    {
        if (!isActive) return;

        isActive = false;
        isRedLight = false;

        if (cycleCoroutine != null)
            StopCoroutine(cycleCoroutine);

        foreach (var doll in dolls)
            doll.SetLightState(true);

        if (redLightSound != null)
            redLightSound.Stop();
    }

    private IEnumerator Cycle()
    {
        while (isActive)
        {
            // 🟢 GREEN LIGHT
            isRedLight = false;

            foreach (var doll in dolls)
                doll.SetLightState(true);

            if (redLightSound != null && redLightSound.isPlaying)
                redLightSound.Stop();

            yield return new WaitForSeconds(greenDuration);

            // 🔴 RED LIGHT
            isRedLight = true;

            foreach (var doll in dolls)
                doll.SetLightState(false);

            if (redLightSound != null && !redLightSound.isPlaying)
                redLightSound.Play();

            yield return new WaitForSeconds(redDuration);
        }
    }
}
