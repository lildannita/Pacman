using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer frightened;
    public SpriteRenderer halfFrightened;

    public bool eaten { get; private set; }

    public override void Enable(float duration)
    {
        base.Enable(duration);

        this.body.enabled = false;
        this.eyes.enabled = false;
        this.frightened.enabled = true;
        this.halfFrightened.enabled = false;

        Invoke(nameof(Flash), duration / 2.0f);
    }

    private void Flash() {
        if (!this.eaten) {
            this.frightened.enabled = false;
            this.halfFrightened.enabled = true;
            this.halfFrightened.GetComponent<AnimateSprite>().Restore();
        }
    }

    public override void Disable()
    {
        base.Disable();

        this.body.enabled = true;
        this.eyes.enabled = true;
        this.frightened.enabled = false;
        this.halfFrightened.enabled = false;
    }

    private void OnEnable() {
        this.ghost.movement.speedMultiplier = 0.5f;
        this.eaten = false;
    }

    private void OnDisable() {
        this.ghost.movement.speedMultiplier = 1.0f;
        this.eaten = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman")) {
            if (this.enabled) {
                Eaten();
            }
        }
    }

    private void Eaten() {
        this.eaten = true;
        
        Vector3 basePosition = this.ghost.home.baseTransform.position;
        basePosition.z = this.ghost.transform.position.z;
        this.ghost.transform.position = basePosition;
        this.ghost.home.Enable(this.duration);

        this.body.enabled = false;
        this.eyes.enabled = true;
        this.frightened.enabled = false;
        this.halfFrightened.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled) {
            Vector2 maxDirection = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach (Vector2 direction in node.directions) {
                Vector3 newPosition = this.transform.position + new Vector3(direction.x, direction.y, 0.0f);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance) {
                    maxDistance = distance;
                    maxDirection = direction;
                }
            }

            this.ghost.movement.SetDirection(maxDirection);
        }
    }
}
