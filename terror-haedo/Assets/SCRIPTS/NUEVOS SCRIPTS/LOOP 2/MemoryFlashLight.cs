using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class MemoryFlashLight : MonoBehaviour
{
    public MemoryFlashMiniGame miniGame;
    public float flashIntensity = 6f;

    [Header("Audio")]
    public AudioClip flashSound;

    private Light _light;
    private AudioSource audioSource;
    private float baseIntensity;

    private void Awake()
    {
        _light = GetComponent<Light>();
        audioSource = GetComponent<AudioSource>();
        baseIntensity = _light.intensity;

        audioSource.playOnAwake = false;
    }

    public void Flash(float duration)
    {
        if (!gameObject.activeInHierarchy)
            return;

        PlayFlashSound();
        StartCoroutine(FlashRoutine(duration));
    }

    private IEnumerator FlashRoutine(float duration)
    {
        _light.intensity = flashIntensity;
        yield return new WaitForSeconds(duration);
        _light.intensity = baseIntensity;
    }

    private void OnMouseDown()
    {
        if (miniGame == null) return;
        if (!miniGame.IsInputEnabled()) return;

        miniGame.RegisterPlayerClick(this);
        Flash(0.25f);
    }

    private void PlayFlashSound()
    {
        if (flashSound != null && audioSource != null)
            audioSource.PlayOneShot(flashSound);
    }
}
