using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    private TextMesh playersText;
    [SerializeField] Text highScoreTextGameOver;
    [SerializeField] Text highScoreTextNextLevel;
    private int playerCount;
    private GameManager gameManager;
    [SerializeField] private int scoreStairValue;

    private void Awake()
    {
        playersText = GameObject.Find("ScoreText").GetComponent<TextMesh>();
        gameManager = FindObjectOfType<GameManager>();
        
    }
    private void Start()
    {
        HighScoreUpdate();
    }
    public  void ScoreUpdate(int stairs)
    {
     
        playerCount = FindObjectsOfType<Player>().Length+stairs*scoreStairValue;
        playersText.text = playerCount.ToString();
     
        if (playerCount == 0)
        {
            gameManager.gameOver = true;
            gameManager.GameOver();
        }
   
    }
    public void HighScoreUpdate()
    {
      
        if (PlayerPrefs.HasKey("highscore"+gameManager.SceneIndex))
        {
            if (PlayerPrefs.GetInt("highscore" + gameManager.SceneIndex) <= playerCount)
            {
                PlayerPrefs.SetInt("highscore" + gameManager.SceneIndex, playerCount);
                
            }

        }
        else
        {
            PlayerPrefs.SetInt("highscore" + gameManager.SceneIndex, 0);
        }

        highScoreTextNextLevel.text = "High Score: " + PlayerPrefs.GetInt("highscore" + gameManager.SceneIndex);
        highScoreTextGameOver.text = highScoreTextNextLevel.text;
    }
}
