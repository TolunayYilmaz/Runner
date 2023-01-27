using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    // Start is called before the first frame update
    Transform cameraPos;
    private Transform playerPos;
    [SerializeField] float camSpeed = 5f;
    public Vector3 offSet;
    void Start()
    {
        playerPos = GameObject.Find("Players").transform;
        offSet = CameraDistance(transform.position,playerPos);
    }

    // Update is called once per frame

    private void Update()
    {
        MoveTheCamera();
    }
    private void MoveTheCamera()
    { //lerp metodu sayesinde yumuþak kemra takibi yapar.
      //fixedupdate tüm fiziksel iþlemler bittikten sonra çalýþýr
        if (playerPos != null)
        {
            Vector3 mesafe = playerPos.position + offSet;
            transform.position = Vector3.Lerp(transform.position, mesafe, camSpeed * Time.deltaTime);
            
        }
        else
        {
            offSet = CameraDistance(transform.position,GameObject.FindGameObjectWithTag("Players").transform);
        }

    }
   public Vector3 CameraDistance(Vector3 cam,Transform newTarget)
    {
        return cam - newTarget.position;
    }
}
