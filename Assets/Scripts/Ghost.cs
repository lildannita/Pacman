using UnityEngine;

public class Ghost : MonoBehaviour
{
    public enum GhostType {
        Blinky,
        Pinky,
        Inky,
        Clyde,
    }
    public Movement movement { get; private set; }
    public GhostBase home { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFrightened frightened { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostBehavior initBehavior;
    public Transform target;
    public GhostType type;
    public int points = 200;

    private int overallWins = 0;
    private int overallLoss = 0;

    private void Awake() {
        this.movement = GetComponent<Movement>();
        this.home = GetComponent<GhostBase>();
        this.chase = GetComponent<GhostChase>();
        this.frightened = GetComponent<GhostFrightened>();
        this.scatter = GetComponent<GhostScatter>();

        switch (this.type) {
            case GhostType.Blinky:
                this.overallWins = PlayerPrefs.GetInt("OverallBlinkyWin");
                this.overallLoss = PlayerPrefs.GetInt("OverallBlinkyLoss");
                break;
            case GhostType.Pinky:
                this.overallWins = PlayerPrefs.GetInt("OverallPinkyWin");
                this.overallLoss = PlayerPrefs.GetInt("OverallPinkyLoss");
                break;
            case GhostType.Inky:
                this.overallWins = PlayerPrefs.GetInt("OverallInkyWin");
                this.overallLoss = PlayerPrefs.GetInt("OverallInkyLoss");
                break;
            case GhostType.Clyde:
                this.overallWins = PlayerPrefs.GetInt("OverallClydeWin");
                this.overallLoss = PlayerPrefs.GetInt("OverallClydeLoss");
                break;
            default:
                break;
        }
    }

    public void EnableMoving() {
        this.movement.StartMoving();
        ResetState();
    }

    public void ResetState() {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable();

        if (this.home != this.initBehavior) {
            this.home.Disable();
        }
        
        this.initBehavior?.Enable();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman")) {
            if (this.frightened.enabled) {
                this.overallWins += 1;
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else {
                this.overallLoss += 1;
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }

    private void OnDestroy() {
        switch (this.type) {
            case GhostType.Blinky:
                PlayerPrefs.SetInt("OverallBlinkyWin", this.overallWins);
                PlayerPrefs.SetInt("OverallBlinkyLoss", this.overallLoss);
                break;
            case GhostType.Pinky:
                PlayerPrefs.SetInt("OverallPinkyWin", this.overallWins);
                PlayerPrefs.SetInt("OverallPinkyLoss", this.overallLoss);
                break;
            case GhostType.Inky:
                PlayerPrefs.SetInt("OverallInkyWin", this.overallWins);
                PlayerPrefs.SetInt("OverallInkyLoss", this.overallLoss);
                break;
            case GhostType.Clyde:
                PlayerPrefs.SetInt("OverallClydeWin", this.overallWins);
                PlayerPrefs.SetInt("OverallClydeLoss", this.overallLoss);
                break;
            default:
                break;
        }
    }

    public void StopMoving() {
        this.home.Disable();
        this.chase.Disable();
        this.frightened.Disable();
        this.scatter.Disable();
        this.movement.StopMoving();
    }
}
