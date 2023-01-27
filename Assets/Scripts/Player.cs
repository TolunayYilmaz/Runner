using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
   
   private Transform Center;
   private float speed=1f;
   public float Speed { set { speed = value; } }    
   public bool ramp=false;
   [SerializeField] GameObject playerEffect;
   ScoreManager scoreManager;
   private int playerHealth = 1;
   public int PlayerHealth { get { return playerHealth; } set { playerHealth = value; } }   
   private Transform target;
   public Transform Target { set { target = value; } }
   private bool finishLine=false;
   public bool FinishLine {  set { finishLine = value; } }   
    GameManager gameManager;
    private void Awake()
    {
        Center = GameObject.FindGameObjectWithTag("Players").transform;
        scoreManager = FindObjectOfType<ScoreManager>();
        gameManager=FindObjectOfType<GameManager>();
        
    }

    void FixedUpdate()
    {
        PlayersCollection();
  
    }
    void PlayersCollection()
    {
        if (finishLine == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Center.position.x, Center.position.y - 1f, Center.position.z), Time.fixedDeltaTime * speed);
            if (target != null)
            {
                print("target bos deðil");
                transform.LookAt(new Vector3(target.position.x, transform.localPosition.y, target.position.z));
            }
            if (transform.position.y > 2.7 && ramp)
            {
                StartCoroutine(GroundDistanceZero());
                if (ramp)
                {
                    print("aþaaðý in");
                    StartCoroutine(Ramp());
                }
            }
        }
    }
    IEnumerator GroundDistanceZero()
    {
        yield return new WaitForSeconds(0.2f);
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,1f, transform.position.z), 15f * Time.deltaTime);
    }
    IEnumerator Ramp()
    {
        yield return new WaitForSeconds(3f);
        ramp = false;
    }

    private void OnDestroy()
    {
        if(playerEffect != null&&!gameManager.gameOver)
        {
            Instantiate(playerEffect, transform.position, Quaternion.identity);
            scoreManager.ScoreUpdate(0);
        }
    }

}
