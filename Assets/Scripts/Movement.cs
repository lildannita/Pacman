using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public Rigidbody2D character { get; private set; }
    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;
    public Vector2 initDirection;
    public LayerMask obstacleLayer;
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startPosition { get; private set; }
    private bool stopped = true;

    private void Awake()
    {
        this.character = GetComponent<Rigidbody2D>();
        this.startPosition = this.transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.speedMultiplier = 1.0f;
        this.direction = this.initDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startPosition;
        this.character.isKinematic = false;
        this.enabled = true;
    }

    private void Update() {
        if (this.nextDirection != Vector2.zero) {
            SetDirection(this.nextDirection);
        }
    }

    private void FixedUpdate()
    {
        if (this.stopped) {
            return;
        }
        Vector2 oldPosition = this.character.position;
        Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
        this.character.MovePosition(oldPosition + translation);
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !Occupied(direction)) {
            this.direction = direction;
            this.nextDirection = Vector2.zero;
        }
        else {
            this.nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, this.obstacleLayer);
        return hit.collider != null;
    }

    public void StopMoving() {
        this.stopped = true;
    }

    public void StartMoving() {
        this.stopped = false;
    }
}
