using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameOver;
    private PlayerController[] playerController;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject nextLevelPanel;
    private int sceneIndex;
    public int SceneIndex { get { return sceneIndex; } } 
    ScoreManager scoreManager;
    private void Awake()
    {
        playerController = FindObjectsOfType<PlayerController>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        
    }
    public void GameOver()
    {
        if (gameOver)
        {
            
            Debug.Log("Oyun Bitti");
            Pause();
            gameOverPanel.SetActive(true);


        }

        
    }
    // oyun durdurmak için 
    private void Pause()
    {
        foreach (PlayerController player in playerController)
        {
            player.run = false;
            player.battle = false;
        }
    }
    public void Restart()
    {
        
        SceneManager.LoadScene(sceneIndex);
      
    }
    public void NextLevel()
    {
        if (sceneIndex + 1 <= SceneManager.sceneCountInBuildSettings-1)
        {
            
            SceneManager.LoadScene(sceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(sceneIndex);

        }
      

    }
    public void NextLevelPanel()
    {
      
        nextLevelPanel.SetActive(true);
        gameOver = true;
        Pause();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Random Üretilen playerlar players position etrafýnda küre þeklinde random daðýlýr.
    /// </summary>
    /// <returns></returns>
    public Vector3 PlayerPosition(Vector3 targetPosition, float radius)
    {
        Vector3 pos = Random.insideUnitSphere * radius;
        Vector3 newPos = targetPosition + pos;
        newPos.y = 1f;
        return newPos;
    }

}
