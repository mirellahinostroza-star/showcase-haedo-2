using UnityEngine;

public class MemoryFlashBlocker : MonoBehaviour
{
    private GameObject blockerRoot;

    [Header("Loop Settings")]
    public LoopManager loopManager;
    public int loopToPlaySound = 2;

    [Header("Sound")]
    public AudioClip victoryClip;

    private bool alreadyUnlocked = false;

    private void Awake()
    {
        blockerRoot = gameObject;
    }

    public void UnlockPath()
{
    Debug.Log("UNLOCKPATH LLAMADO");

    if (victoryClip != null)
        AudioSource.PlayClipAtPoint(victoryClip, transform.position);

    blockerRoot.SetActive(false);
}

    public void ResetBlocker()
    {
        alreadyUnlocked = false;
        blockerRoot.SetActive(true);
    }
}
