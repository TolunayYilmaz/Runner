using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform Center;
    [SerializeField] private float speed;
    [SerializeField] private GameObject enemyFx;
    private Enemys enemys;
    private bool isDamage;
    private Transform player;
    private int enemyHealth;
    public int EnemyHealth { set { enemyHealth = value; } }
    GameManager gameManager;
    private void Awake()
    {
          player = GameObject.Find("Players").GetComponent<Transform>();
          gameManager=FindObjectOfType<GameManager>();    

    }
    private void Start()
    {
        if (GetComponentInParent<Enemys>() != null)
        {
            enemys = GetComponentInParent<Enemys>();
            Center = enemys.transform;
            enemys.EnemysScoreUpdate();
        }
        else
        {
            print("i�inde Enemys Yok");
        }

    }
    private void FixedUpdate()
    {
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Center.position.x, transform.localPosition.y, Center.position.z), Time.fixedDeltaTime * speed);
            transform.LookAt(new Vector3(player.position.x, transform.localPosition.y, player.position.z));
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Battle(other);
    }
    private void OnDestroy()
    {
        if (enemyFx != null && !gameManager.gameOver)
        {
            Instantiate(enemyFx, transform.position, Quaternion.identity);
            enemys.EnemysScoreUpdate();
        }
    }



    //Trigerlananan enemy ile player ilk a�amada  playerda player scripti var m� diye kontrol eder daha sonra enemanin can�n� kontrol eder ve can 1 is i�indeki koda girer.
    //��erideki hesaplama enemy ile player�n can� e�itse 1 enemy 1 player bir birini g�t�rmesi i�in tasarlanm��t�r.
    //��nk� enemy ile player birbirine do�ru ko�arken ayn� anda birden fazla enemy veya player denk gelebilmektedir.
    void Battle(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null && gameObject.tag == "Enemy")
        {
            isDamage = true;
            if (enemyHealth <= 1)
            {
                if (isDamage)
                {
                    enemyHealth -= other.GetComponent<Player>().PlayerHealth;
                    other.GetComponent<Player>().PlayerHealth--;
                    isDamage = false;
                }

                if (enemyHealth == 0 && other.GetComponent<Player>().PlayerHealth == 0)
                {
                    Destroy(gameObject);
                    Destroy(other.gameObject);

                }
                else
                {
                    other.GetComponent<Player>().PlayerHealth = 1;
                }
            }
            else if (enemyHealth > 1)
            {
                if (isDamage)
                {
                    enemyHealth -= other.GetComponent<Player>().PlayerHealth;
                    other.GetComponent<Player>().PlayerHealth--;
                    Destroy(other.gameObject);
                    isDamage = false;
                }
                if (enemyHealth == 0)
                {
                    Destroy(gameObject);

                }
            }


        }
    }
}
