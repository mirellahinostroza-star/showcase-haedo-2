using UnityEngine;

public class FinalSeatInteraction : MonoBehaviour
{
    public FinalCinematicManager cinematicManager;
    private bool used = false;

    private void OnMouseDown()
    {
        if (used) return;

        used = true;
        cinematicManager.StartFinalCinematic();
    }
}
