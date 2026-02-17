using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinalCinematicManager : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject player;
    public Camera mainCamera;
    public Camera seatCamera;
    public VideoPlayer videoPlayer;

    [Header("Videos")]
    public VideoClip goodEndingVideo;
    public VideoClip badEndingVideo;

    [Header("Escenas")]
    public string goodEndingScene = "FINAL_BUENO";
    public string badEndingScene = "FINAL_MALO";

    [Header("Configuración")]
    public float waitAfterVideo = 3f;
    public int failThreshold = 5;

    private bool started = false;

    public void StartFinalCinematic()
    {
        if (started) return;
        started = true;

        SwitchCameraAndHidePlayer();

        int fails = GameManager.Instance.totalFails;

        if (fails <= failThreshold)
            StartCoroutine(PlayEnding(goodEndingVideo, goodEndingScene));
        else
            StartCoroutine(PlayEnding(badEndingVideo, badEndingScene));
    }

    void SwitchCameraAndHidePlayer()
    {
        mainCamera.gameObject.SetActive(false);
        player.SetActive(false);
        seatCamera.gameObject.SetActive(true);
    }

    IEnumerator PlayEnding(VideoClip clip, string sceneToLoad)
    {
        videoPlayer.clip = clip;
        videoPlayer.Play();

        yield return new WaitUntil(() => !videoPlayer.isPlaying);

        yield return new WaitForSeconds(waitAfterVideo);

        SceneManager.LoadScene(sceneToLoad);
    }
}
