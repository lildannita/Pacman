using UnityEngine;

public class GhostChase : GhostBehavior
{
    private void OnTriggerEnter2D(Collider2D other) {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !this.ghost.frightened.enabled) {
            Vector2 minDirection = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach (Vector2 direction in node.directions) {
                Vector3 newPosition = this.transform.position + new Vector3(direction.x, direction.y, 0.0f);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance) {
                    minDistance = distance;
                    minDirection = direction;
                }
            }

            this.ghost.movement.SetDirection(minDirection);
        }
    }

    private void OnDisable() {
        this.ghost.scatter.Enable();
    }
}
