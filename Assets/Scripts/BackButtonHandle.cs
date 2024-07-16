using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButtonHandle : MonoBehaviour
{
    public int sceneNumber;
    public Button backButton;
    public Sprite backButtonHoveredImage;
    private Sprite backButtonOriginalImage;

    private void Start() {
        this.backButtonOriginalImage = this.backButton.image.sprite;
    }

    public void BackToMenu() {
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.UnloadSceneAsync(currentSceneIdx);
        SceneManager.LoadScene(0);
    }

    public void ChangeWhenHovered() {
        this.backButton.image.sprite = this.backButtonHoveredImage;
    }

    public void ChangeWhenExit() {
        this.backButton.image.sprite = this.backButtonOriginalImage;
    }
}
