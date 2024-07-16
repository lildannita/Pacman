using UnityEngine;
using UnityEngine.UI;

public class StatsHandler : MonoBehaviour
{
    public Text highScore;
    public Text overallScore;
    public Text overallRounds;
    public Text overallWins;
    public Text overallGhostsEaten;
    public Text overallEatenByGhosts;
    public Text overallBlinkyWin;
    public Text overallBlinkyLoss;
    public Text overallPinkyWin;
    public Text overallPinkyLoss;
    public Text overallInkyWin;
    public Text overallInkyLoss;
    public Text overallClydeWin;
    public Text overallClydeLoss;

    private void Awake() {
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        overallScore.text = PlayerPrefs.GetInt("OverallScore").ToString();
        overallRounds.text = PlayerPrefs.GetInt("OverallRounds").ToString();
        overallWins.text = PlayerPrefs.GetInt("OverallWins").ToString();

        overallGhostsEaten.text = PlayerPrefs.GetInt("OverallGhostsEaten").ToString();
        overallEatenByGhosts.text = PlayerPrefs.GetInt("OverallEatenByGhosts").ToString();

        overallBlinkyWin.text = PlayerPrefs.GetInt("OverallBlinkyWin").ToString();
        overallBlinkyLoss.text = PlayerPrefs.GetInt("OverallBlinkyLoss").ToString();

        overallPinkyWin.text = PlayerPrefs.GetInt("OverallPinkyWin").ToString();
        overallPinkyLoss.text = PlayerPrefs.GetInt("OverallPinkyLoss").ToString();

        overallInkyWin.text = PlayerPrefs.GetInt("OverallInkyWin").ToString();
        overallInkyLoss.text = PlayerPrefs.GetInt("OverallInkyLoss").ToString();
        
        overallClydeWin.text = PlayerPrefs.GetInt("OverallClydeWin").ToString();
        overallClydeLoss.text = PlayerPrefs.GetInt("OverallClydeLoss").ToString();
    }
}
