using UnityEngine;

public class GhostScatter : GhostBehavior
{
    private void OnTriggerEnter2D(Collider2D other) {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !this.ghost.frightened.enabled) {
            int dirIndex = Random.Range(0, node.directions.Count);
            if (node.directions[dirIndex] == -this.ghost.movement.direction && node.directions.Count > 1) {
                if (dirIndex == node.directions.Count - 1) {
                    dirIndex--;
                }
                else {
                    dirIndex++;
                }
            }
            this.ghost.movement.SetDirection(node.directions[dirIndex]);
        }
    }

    private void OnDisable() {
        this.ghost.chase.Enable();
    }
}
