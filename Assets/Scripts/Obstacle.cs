using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool saw=false;
    [SerializeField] private float rotateSpeed;
    private PlayerController playerController;
    private GameManager gameManager;
    private ScoreManager scoreManager;
    private List<GameObject> players= new List<GameObject>();
    [SerializeField]bool calis = true;
    private int cameraPositionX;
    private bool isDown = true;
   
    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Players").GetComponent<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }


    // Update is called once per frame
    void Update()
    {
        ObstacleRotation();
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerDestroy(other);
        FinishLine(other);
        DownPlayer(other);
        PassedLevel(other);
    }

    private void OnTriggerExit(Collider other)
    {
        BuildTriangle(other);
    
    }
    private void OnCollisionEnter(Collision collision)
    {
        playerPosition(collision,false);
        Stairs(collision);

    }
    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.GetComponent<Player>().ramp = true;

    }
      void ObstacleRotation()
    {
        if (saw)
        {
            transform.Rotate(Vector3.up * rotateSpeed*Time.deltaTime);
        }
    }
    void playerPosition(Collision collision,bool isDirection)
    {    

        if (collision.gameObject.tag == "Player")
        {
           // playerController = collision.gameObject.GetComponentInParent<PlayerController>();
            if(gameObject.tag == "WallRight")
            {
                playerController.IsRight = isDirection;
                playerController.IsLeft = !isDirection;
            }
            else if (gameObject.tag == "WallLeft")
            {
                playerController.IsLeft = isDirection;
                playerController.IsRight = !isDirection;
            }
        }
        else
        {
            return;
            
        }
        
    }
  
    void PlayerDestroy(Collider other)
    {
        if (other.CompareTag("Player")&&gameObject.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
            StartCoroutine(Destroy());
        }
       
    }
    // kullanýlan nesneleri 3 sn sonra yok eder performans için.
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(transform.parent.gameObject);
    }
    void FinishLine(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag("Finish"))
        {
           GameObject.Find("BuildTriangle").GetComponent<Triangle>().players.Add(other.gameObject);
           PlayerControllerOff(other,true,false);
           
        }
    }

    /// <summary>
    /// Playerý hareket kýsýtlamasýný saðlar.
    /// </summary>
    /// <param name="other">Player scriptindeki merkez noktasýndaki haraketi kontrol eder</param>
    /// <param name="stop"> PlayerController ileri yönü kontrol</param>
    /// <param name="move"> PlayerController sað ve sol hareket kontrol</param>
    void PlayerControllerOff(Collider other,bool stop, bool move)
    {
        other.gameObject.GetComponent<Player>().FinishLine = stop;
        playerController.run = stop;
        playerController.IsRight = move;
        playerController.IsLeft = move;
        
    }
    //Score güncellenirken gecikmeli güncellenmeli yoksa 1 kalýyor.
   
    void BuildTriangle(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag("BuildTriangle") && calis)
        {
            Triangle Triangle = GetComponent<Triangle>();
            Triangle.ücgen();
            cameraPositionX = Triangle.Height;
            CameraPos(Quaternion.Euler(0, -45, 0), new Vector3(2 * cameraPositionX, 15 + cameraPositionX, 450));
            calis = false;
            print("calisti");
        }

       
    }

    //Merdivenlere takýlan karakterler Players çýkar otomatik olarak durur.
    void Stairs(Collision other)
    {
        if (gameObject.CompareTag("Stairs") && other.gameObject.GetComponent<Player>() != null)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            other.gameObject.transform.SetParent(transform, true);
            other.gameObject.GetComponent<Player>().enabled=false;
            other.gameObject.tag = gameObject.tag;
            scoreManager.ScoreUpdate((int)transform.position.y);
            if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
            {
                gameManager.NextLevelPanel();
            }
        }
    }
    void PassedLevel(Collider other)
    {
        if (gameObject.CompareTag("NextLevel") && other.gameObject.GetComponent<Player>() != null)
        {
            PlayerControllerOff(other,true,false);
            scoreManager.HighScoreUpdate();
            gameManager.NextLevelPanel();
        }
    }

    // üst üste olan karakterlerin konumunu sýfýrlar.
    void DownPlayer(Collider other)
    {
        if (gameObject.CompareTag("DownPlayer") && other.gameObject.GetComponent<Player>() != null&&isDown)
        {
            playerController.transform.position = new Vector3(playerController.transform.position.x,16f, playerController.transform.position.z);

            for (int i = 1; i < playerController.transform.childCount; i++)
            {
                playerController.transform.GetChild(i).transform.position = new Vector3(gameManager.PlayerPosition(playerController.transform.position, 0.2f).x, gameManager.PlayerPosition(playerController.transform.position, 0.2f).y+16f, gameManager.PlayerPosition(playerController.transform.position, 0.2f).z);
               
            }
            CameraPos(Quaternion.Euler(15, 0, 0), new Vector3(playerController.transform.position.x, playerController.transform.position.y + 15, playerController.transform.position.z - 20f));
            isDown = false;
            scoreManager.ScoreUpdate((int)transform.position.y);
        }

    }

    // üst üste olan karakterler farklý açýya geçmesini saðlar.
    void CameraPos(Quaternion perspective,Vector3 position)
    {

        Camera.main.transform.rotation =perspective;
        CameraFollow cam= Camera.main.GetComponent<CameraFollow>();
        cam.offSet = cam.CameraDistance(position, GameObject.Find("Players").transform);
    }
}
