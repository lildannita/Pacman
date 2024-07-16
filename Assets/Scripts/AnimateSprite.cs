using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimateSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;
    public float animationTime = 0.125f;
    public int animationFrame { get; private set; }
    public bool needToLoop = true;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime);
    }

    public void Advance()
    {
        if (!this.spriteRenderer.enabled) {
            return;
        }

        this.animationFrame++;
        if (this.animationFrame >= this.sprites.Length && this.needToLoop) {
            this.animationFrame = 0;
        }
        // Debug.Assert(this.animationFrame >= 0 && this.animationFrame < this.sprites.Length);
        this.spriteRenderer.sprite = this.sprites[this.animationFrame];
    }

    public void Restore()
    {
        this.animationFrame = -1;
        Advance();
    }
}
