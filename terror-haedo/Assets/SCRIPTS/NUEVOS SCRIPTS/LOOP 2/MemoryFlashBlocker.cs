using UnityEngine;

public class MemoryFlashBlocker : MonoBehaviour
{
    private GameObject blockerRoot;

    private void Awake()
    {
        blockerRoot = gameObject;
    }

    public void UnlockPath()
    {
        Debug.Log($"[{gameObject.name}] Camino desbloqueado.");
        blockerRoot.SetActive(false);
    }

    public void ResetBlocker()
    {
        Debug.Log($"[{gameObject.name}] Camino BLOQUEADO nuevamente.");
        blockerRoot.SetActive(true);
    }
}
