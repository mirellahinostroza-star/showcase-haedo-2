using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Cerrando juego...");
        Application.Quit();
    }
}
