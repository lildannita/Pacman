using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    public SpriteRenderer eyesRenderer { get; private set; }
    public Movement ghostMovement { get; private set; }
    public Sprite topEyes;
    public Sprite downEyes;
    public Sprite leftEyes;
    public Sprite rightEyes;

    private void Awake() {
        this.eyesRenderer = GetComponent<SpriteRenderer>();
        this.ghostMovement = GetComponentInParent<Movement>();
    }

    private void Update() {
        if (this.ghostMovement.direction == Vector2.up) {
            this.eyesRenderer.sprite = this.topEyes;
        }
        else if (this.ghostMovement.direction == Vector2.down) {
            this.eyesRenderer.sprite = this.downEyes;
        }
        else if (this.ghostMovement.direction == Vector2.left) {
            this.eyesRenderer.sprite = this.leftEyes;
        }
        else if (this.ghostMovement.direction == Vector2.right) {
            this.eyesRenderer.sprite = this.rightEyes;
        }
    }
}
