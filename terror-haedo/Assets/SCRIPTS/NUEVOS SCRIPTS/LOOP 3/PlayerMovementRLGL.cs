using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementRLGL : MonoBehaviour
{
    [Header("Red Light, Green Light")]
    public RedLightGreenLightManager rlglManager;
    public float moveThreshold = 0.01f;
    public float cameraThreshold = 0.5f;
    public float redLightDelay = 0.2f;

    [Header("Referencia a cámara")]
    public Transform playerCamera;

    private CharacterController controller;
    private Vector3 lastPosition;
    private Vector2 lastCamEuler;
    private bool canDie = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        lastPosition = controller.transform.position;

        if (playerCamera != null)
            lastCamEuler = new Vector2(playerCamera.eulerAngles.x, playerCamera.eulerAngles.y);

        if (rlglManager == null)
            Debug.LogWarning("⚠️ RedLightGreenLightManager no asignado.");
    }

    void Update()
    {
        if (rlglManager == null || !rlglManager.enabled) return;

        bool isRedLight = rlglManager.isRedLight;

        if (isRedLight && !canDie)
            StartCoroutine(EnableDeathAfterDelay());
        else if (!isRedLight)
            canDie = false;

        Vector3 moveDelta = controller.transform.position - lastPosition;
        float inputDelta = Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"));

        bool camMoved = false;

        if (playerCamera != null)
        {
            Vector2 camEuler = new Vector2(playerCamera.eulerAngles.x, playerCamera.eulerAngles.y);
            Vector2 camDelta = camEuler - lastCamEuler;

            if (Mathf.Abs(camDelta.x) > cameraThreshold || Mathf.Abs(camDelta.y) > cameraThreshold)
                camMoved = true;

            lastCamEuler = camEuler;
        }

        if (isRedLight && canDie && (moveDelta.magnitude > moveThreshold || inputDelta > 0f || camMoved))
        {
            Debug.Log("💀 Movimiento detectado en RED LIGHT");
            TriggerFail();
        }

        lastPosition = controller.transform.position;
    }

    private IEnumerator EnableDeathAfterDelay()
    {
        yield return new WaitForSeconds(redLightDelay);
        canDie = true;
    }

    private void TriggerFail()
    {
        if (rlglManager != null)
        {
            rlglManager.PlayerFailed(); // 🔴 ahora pasa por el sistema global
        }

        canDie = false;
    }
}
