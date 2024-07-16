using UnityEngine;

public class Fruit : Pellet
{
    public int fruitOrder = 0;
    public int pelletsNeedToBeEaten { get; private set; } = 0;

    private void Awake() {
        this.pelletsNeedToBeEaten = (fruitOrder + 1) * 55;
        this.gameObject.SetActive(false);
    }
    protected override void Eat() {
        FindObjectOfType<GameManager>().PelletEaten(this, false, this.fruitOrder);
    }
}
