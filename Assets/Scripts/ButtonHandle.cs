using UnityEngine;
using UnityEngine.UI;

public class ButtonHandle : MonoBehaviour
{
    public Text buttonText;
    public Text buttonDuplicateText;
    public Color originalColor { get; private set; }
    public Color hoverColor;

    private void Start() {
        this.originalColor = this.buttonText.color;
        this.buttonDuplicateText.enabled = false;
    }

    public void ChangeWhenHovered()  {
        this.buttonText.color = this.hoverColor;
        this.buttonDuplicateText.enabled = true;
    }

    public void ChangeWhenExit() {
        this.buttonText.color = this.originalColor;
        this.buttonDuplicateText.enabled = false;
    }
}
