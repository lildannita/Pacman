using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public Text scoreText;
    public Text highScoreText;
    public Text roundText;
    public Text readyText;
    public Text gameOverText;
    public Image[] liveImages;
    public Image[] fruitImages;

    public Fruit[] fruits;

    public AudioSource siren;
    public AudioSource startGameSound;
    public AudioSource gameOverSound;

    public AudioSource pelletEatenSound1;
    public AudioSource pelletEatenSound2;
    private bool frightenedState = false;
    public AudioSource powerPelletEatenSound;

    public AudioSource ghostEatenSound;
    public AudioSource pacmanEatenSound;

    public int score { get; private set; }
    private int highScore;
    public int lives { get; private set; }
    public int round { get; private set; } = 0;
    private bool pacmanDead = false;
    public int ghostMultiplier { get; private set; } = 1;

    private int overallScore = 0;
    private int overallRounds = 0;
    private int overallWins = 0;
    private int overallGhostsEaten = 0;
    private int overallEatenByGhosts = 0;

    private int pelletsEatenCount = 0;

    private void Awake() {
        this.highScore = PlayerPrefs.GetInt("HighScore");
        this.highScoreText.text = this.highScore.ToString();

        this.overallScore = PlayerPrefs.GetInt("OverallScore");
        this.overallRounds = PlayerPrefs.GetInt("OverallRounds");
        this.overallWins = PlayerPrefs.GetInt("OverallWins");
        this.overallGhostsEaten = PlayerPrefs.GetInt("OverallGhostsEaten");
        this.overallEatenByGhosts = PlayerPrefs.GetInt("OverallEatenByGhosts");

        this.readyText.enabled = true;
        this.gameOverText.enabled = false;
        this.startGameSound.Play();

        Invoke(nameof(NewGame), 4.0f);
    }

    private void Update()
    {
        if (this.pacmanDead && Input.anyKeyDown && !Input.GetMouseButtonDown(0)) {
            this.pacmanDead = false;
            NewGame();
        }    
    }

    private void NewGame()
    {
        this.readyText.enabled = false;
        this.gameOverText.enabled = false;

        this.pacman.movement.StartMoving();
        for (int i = 0; i < this.ghosts.Length; i++) {
            this.ghosts[i].EnableMoving();
        }

        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        this.siren.Play();
        AddRound();

        foreach (Transform pellet in this.pellets) {
            pellet.gameObject.SetActive(true);
        }

        // Важно отключать фрукты после включения всех съедобных объектов,
        // т.к. они находятся на том же Layer, что и все остальные
        this.pelletsEatenCount = 0;
        foreach (Fruit fruit in this.fruits) {
            fruit.gameObject.SetActive(false);
            fruitImages[fruit.fruitOrder].enabled = false;
        }
        ResetCharactersState();
    }

    private void GameOver()
    {
        this.round = 1;
        this.roundText.text = this.round.ToString();

        this.gameOverText.enabled = true;
        for (int i = 0; i < this.ghosts.Length; i++) {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
    }

    private void ResetCharactersState()
    {
        this.siren.Play();

        for (int i = 0; i < this.ghosts.Length; i++) {
            this.ghosts[i].movement.StartMoving();
            this.ghosts[i].ResetState();
        }

        ResetGhostMultiplier();

        this.pacman.ResetState();
    }

    private void SetScore(int score)
    {
        this.score = score;
        this.scoreText.text = this.score.ToString();
    }

    private void AddScore(int score)
    {
        this.score += score;
        this.scoreText.text = this.score.ToString();
        this.overallScore += score;

        if (this.score > this.highScore) {
            this.highScore = this.score;
            this.highScoreText.text = this.highScore.ToString();
        }
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        for (int i = 1; i <= this.liveImages.Length; i++) {
            if (i <= this.lives) {
                this.liveImages[i - 1].enabled = true;
            }
            else {
                this.liveImages[i - 1].enabled = false;
            }
        }
    }

    private void AddRound() {
        this.round += 1;
        this.roundText.text = round.ToString();
        this.overallRounds += 1;
    }

    public void GhostEaten(Ghost ghost)
    {
        this.overallGhostsEaten += 1;
        this.ghostEatenSound.Play();

        int points = ghost.points * this.ghostMultiplier;
        AddScore(points);
        this.ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        this.siren.Stop();
        this.overallEatenByGhosts += 1;

        this.pacman.gameObject.SetActive(false);
        SetLives(this.lives - 1);

        if (this.lives == 0) {
            this.gameOverSound.Play();
            this.pacmanDead = true;
            GameOver();
        }
        else {
            this.pacmanEatenSound.Play();
            for (int i = 0; i < this.ghosts.Length; i++) {
                this.ghosts[i].StopMoving();
            }

            Invoke(nameof(ResetCharactersState), 3.0f);
        }
    }

    public void PelletEaten(Pellet pellet, bool isPowerPellet = false, int fruitNumber = -1)
    {
        if (!isPowerPellet) {
            if (this.frightenedState)
                this.pelletEatenSound2.Play();
            else
                this.pelletEatenSound1.Play();
            this.pelletsEatenCount += 1;

            foreach (Fruit fruit in this.fruits) {
                if (fruit.pelletsNeedToBeEaten == this.pelletsEatenCount) {
                    fruit.gameObject.SetActive(true);
                }
            }
            
        }

        if (fruitNumber != -1) {
            this.fruitImages[fruitNumber].enabled = true;
        }

        pellet.gameObject.SetActive(false);
        AddScore(pellet.points);

        if (!HasPelletsLeft()) {
            this.overallWins += 1;
            this.pacman.gameObject.SetActive(false);

            this.siren.Stop();
            if (frightenedState)
                this.powerPelletEatenSound.Stop();
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        this.frightenedState = true;
        this.siren.Stop();
        this.powerPelletEatenSound.Play();

        for (int i = 0; i < this.ghosts.Length; i++) {
            this.ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet, true);
        CancelInvoke(nameof(ResetGhostMultiplier));
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasPelletsLeft()
    {
        foreach (Transform pellet in this.pellets) {
            if (pellet.gameObject.activeSelf) {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiplier() {
        this.frightenedState = false;
        this.powerPelletEatenSound.Stop();
        this.siren.Play();
        this.ghostMultiplier = 1;
    }

    private void OnDestroy() {
        PlayerPrefs.SetInt("HighScore", this.highScore);
        PlayerPrefs.SetInt("OverallScore", this.overallScore);
        PlayerPrefs.SetInt("OverallRounds", this.overallRounds);
        PlayerPrefs.SetInt("OverallWins", this.overallWins);
        PlayerPrefs.SetInt("OverallGhostsEaten", this.overallGhostsEaten);
        PlayerPrefs.SetInt("OverallEatenByGhosts", this.overallEatenByGhosts);
    }
}
