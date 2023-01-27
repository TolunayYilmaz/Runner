using UnityEngine;

public class Enemys : MonoBehaviour
{
    private int enemyScore;
    private TextMesh enemyText;
    private PlayerController playerController;
    [SerializeField] int enemyCount; 
    [SerializeField]GameObject enemyPrefab;
    [SerializeField] private int enemyPower;
    
    
    private void Awake()
    {
        enemyText = transform.GetChild(0).GetComponent<TextMesh>();
        EnemysScoreUpdate();
    }
    private void Start()
    {
    SpawnEnemy();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponentInParent<PlayerController>();

            //tetiklendiði zaman düþmanada doðru rotasyon ata
            other.gameObject.GetComponent<Player>().Target = transform;
            playerController.battle = true;
            playerController.run = false;
            playerController.target=gameObject.transform;
            gameObject.GetComponent<PlayerController>().battle = true;
            gameObject.GetComponent<PlayerController>().target = playerController.transform;
            print("player deðdi");
          
        }
    
    }
    public void EnemysScoreUpdate()
    {
       enemyScore= transform.childCount -1;
       enemyText.text = (enemyScore*enemyPower).ToString();
      
        if(enemyScore <= 1&& playerController!=null)
        {
            playerController.battle = false;
            playerController.run = true;
            Destroy(gameObject);    
        }
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject clone=Instantiate(enemyPrefab, PlayerPosition(), Quaternion.identity);
           // üretilen enemylerin gücü ve büyüklüðü belirtilir.
            clone.GetComponent<Enemy>().EnemyHealth = enemyPower;
            if (enemyPower == 1)
            {
                clone.transform.localScale = Vector3.one * enemyPower;
            }
            else if (enemyPower> 1)
            {
                clone.transform.localScale = Vector3.one * (enemyPower*0.2f)+Vector3.one;
            }
           
            clone.transform.SetParent(transform, true);
        }
    }
    public Vector3 PlayerPosition()
    {
        Vector3 pos = Random.insideUnitSphere * 0.4f;
        Vector3 newPos = transform.position + pos;
        newPos.y = 1f;
        return newPos;
    }
}
