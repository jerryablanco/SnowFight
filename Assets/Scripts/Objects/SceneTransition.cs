using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("New Scene Variables")]
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerLocationStorage;
    public Vector2 cameraNewMin;
    public Vector2 cameraNewMax;
    public VectorValue cameraMin;
    public VectorValue cameraMax;

    [Header("Transition Variables")]
    public GameObject fadeOutPanel;
    public GameObject fadeInPanel;
    public float fadeWait;

    public void Awake()
    {
        if (fadeInPanel != null) {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel,1);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger) {
            playerLocationStorage.initialValue = playerPosition;
            StartCoroutine(FadeCoRoutine());
            //SceneManager.LoadScene(sceneToLoad);
        }
    }

    public IEnumerator FadeCoRoutine()
    {
        if (fadeOutPanel != null) {
            GameObject panel = Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity) as GameObject;
        }
        yield return new WaitForSeconds(fadeWait);
        ResetCameraBounds();

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone) {
            yield return null;
        }
    }

    public void ResetCameraBounds()
    {
        cameraMin.initialValue = cameraNewMin;
        cameraMax.initialValue = cameraNewMax;
    }

}
