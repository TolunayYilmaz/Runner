using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    Transform players;
    [SerializeField] private int powerUpVariable;

    private bool createClonePlayer = true;
    public bool CreateClonePlayer { get { return createClonePlayer; } set { createClonePlayer = value; } }  
    private string powerUp;
    private int powerUpNumberInput;
    private char signInput;
    ScoreManager scoreManager;
    GameManager gameManager;
    private void Awake()
    {
        players = GameObject.FindGameObjectWithTag("Players").GetComponent<Transform>();
        //PowerUp �st�ndeki yaz�y� bulur ve metodlara atama yapar.
        powerUp = transform.GetChild(0).GetComponent<TextMesh>().text;
        powerUpNumberInput = System.Int32.Parse(powerUp.Substring(1));
        signInput= powerUp.ToLower()[0];
        scoreManager = FindObjectOfType<ScoreManager>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
       
        StartCoroutine(Basla(other));
    }
    private void OnTriggerExit(Collider other)
    {
        scoreManager.ScoreUpdate(0);
    }
    /// <summary>
    /// Matematilsel i�lem yap�ld�ktan sonra aktar�lan say� tekrar� yap�l�r ve �retilir.Players objesinin i�ine g�nderilir.
    /// </summary>
    /// <param name="other"></param>
    void ClonePlayer(Collider other)
    {
        PowerUpController();
        if (other.CompareTag("Player")&& createClonePlayer)
        {
            GetComponentInParent<PowerManager>().Transition(0);
            createClonePlayer = false;
            for (int i = 1; i < powerUpVariable; i++)
            {
                GameObject clone = Instantiate(other.gameObject, gameManager.PlayerPosition(players.position, 0.2f), Quaternion.identity);
                //if (clone.GetComponent<PlayerController>() == null)
                //{
                //   // clone.AddComponent<PlayerController>();
                //    clone.transform.SetParent(players.transform, true);
                //}
                clone.transform.SetParent(players.transform, true);
            }
        }
    }

    /// <summary>
    /// �retimi gecitrimek i�in oyuncu deneyimini artt�mrak i�in yap�ld�.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    IEnumerator Basla(Collider other)
    {
        yield return new WaitForSeconds(0.05f);
        ClonePlayer(other);
    }


    /// <summary>
    /// �arpma ve toplama i�lemi yapar.
    /// </summary>
    void PowerUpController()
    {
        if (signInput == '+')
        {
            powerUpVariable = powerUpNumberInput+1;
        }
        else if(signInput == 'x')
        {
            int multiply = GameObject.FindGameObjectsWithTag("Player").Length;
            Debug.Log(multiply);
            if (multiply == 1)
            {
                powerUpVariable = powerUpNumberInput * multiply;
            }
            else
            {
                powerUpVariable = (powerUpNumberInput * multiply)-multiply+1;
            }
          
        }
      
    }

}
