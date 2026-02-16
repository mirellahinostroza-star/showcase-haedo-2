using UnityEngine;

public class HazardBlock : MonoBehaviour
{
    private Loop4Manager loop4Manager;

    void Start()
    {
        loop4Manager = FindObjectOfType<Loop4Manager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (loop4Manager != null)
                loop4Manager.PlayerFailed();
        }
    }
}
