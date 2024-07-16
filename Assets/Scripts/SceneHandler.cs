using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void SetScene(int sceneNumber) {
        SceneManager.LoadScene(sceneNumber);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
