using System.Collections;
using UnityEngine;

public class GhostBase : GhostBehavior
{
    public Transform baseTransform;
    public Transform outsideTransform;

    private void OnCollisionEnter2D(Collision2D other) {
        if (this.enabled && other.gameObject.layer == LayerMask.NameToLayer("Obstacle")) {
            this.ghost.movement.SetDirection(-this.ghost.movement.direction);
        }
    }

    private IEnumerator ExitTransition() {
        this.ghost.movement.SetDirection(Vector2.up, true);
        this.ghost.movement.character.isKinematic = true;
        this.ghost.movement.enabled = false;

        Vector3 curPosition = this.transform.position;
        float duration = 0.5f;
        float elapsed = 0.0f;

        while (elapsed < duration) {
            Vector3 newPosition = Vector3.Lerp(curPosition, this.baseTransform.position, elapsed / duration);
            newPosition.z = curPosition.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f;

        while (elapsed < duration) {
            Vector3 newPosition = Vector3.Lerp(this.baseTransform.position, this.outsideTransform.position, elapsed / duration);
            newPosition.z = curPosition.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        this.ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        this.ghost.movement.character.isKinematic = false;
        this.ghost.movement.enabled = true;
    }

    private void OnDisable() {
        if (this.gameObject.activeSelf) {
            StartCoroutine(ExitTransition());
        }
    }

    private void OnEnable() {
        StopAllCoroutines();
    }
}
