using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    public float delayInSeconds = 3f; // The duration the splash screen will be displayed

    private void Start()
    {
        Invoke("LoadNextScene", delayInSeconds);
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}