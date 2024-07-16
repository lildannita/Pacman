using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public List<Vector2> directions { get; private set; }

    void Start() {
        this.directions = new List<Vector2>();

        CheckIfDirectionAvailable(Vector2.up);
        CheckIfDirectionAvailable(Vector2.down);
        CheckIfDirectionAvailable(Vector2.left);
        CheckIfDirectionAvailable(Vector2.right);
    }

    private void CheckIfDirectionAvailable(Vector2 direction) {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0.0f, direction, 1.0f, this.obstacleLayer);
        if (hit.collider == null) {
            this.directions.Add(direction);
        }
    }
}
