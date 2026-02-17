using UnityEngine;

public class CeilingReset : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private string playerTag = "Player";

    [Header("Referencias")]
    [SerializeField] private CeilingTrap ceilingTrap;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag) && !other.transform.root.CompareTag(playerTag))
            return;

        // 🔴 Registrar derrota GLOBAL
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddFail();
        }
        else
        {
            Debug.LogError("GameManager no existe en la escena.");
        }

        // 🔁 Respawn jugador
        CharacterController controller = other.GetComponentInParent<CharacterController>();
        Transform playerRoot = controller != null ? controller.transform : other.transform.root;

        if (spawnPoint == null)
        {
            Debug.LogError("CeilingReset: SpawnPoint no asignado.");
            return;
        }

        if (controller != null)
        {
            controller.enabled = false;
            playerRoot.position = spawnPoint.position;
            controller.enabled = true;
        }
        else
        {
            playerRoot.position = spawnPoint.position;
        }

        // 🔁 Resetear techo
        if (ceilingTrap != null)
        {
            ceilingTrap.ResetCeiling();
        }
    }
}
