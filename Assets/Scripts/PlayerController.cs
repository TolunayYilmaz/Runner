using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private float speed=15f;
    public bool run=true;
    public bool battle=false;
    [SerializeField]private bool isPlayer=false; 
   [SerializeField] public Transform target;
    private bool isLeft = true;
    private bool isRight = true;
    public bool IsLeft{set { isLeft = value; } } 
    public bool IsRight{ set { isRight = value; }}

    private void LateUpdate()
    {
        Move();
       
    }
    private void Move()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal")*Time.deltaTime;
        if (run)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
            if (isRight && horizontalInput > 0)
            {
                transform.position += Vector3.right * horizontalInput * speed;
                isLeft = true;
            }
            //Left
            else if (isLeft && horizontalInput < 0)
            {
                transform.position += Vector3.right * horizontalInput * speed;
                isRight = true;
            }
        } 
        else if (battle)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x,transform.localPosition.y, target.position.z), (speed/2)*Time.deltaTime);
           
            if (isPlayer)
            {
                isRight = true;
                isLeft = true;
            }
        }
       
       

    }
}
