using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    [SerializeField] bool clonePowerUp;
    [SerializeField] bool clonePowerUp2;
    private void Awake()
    {
        IsPowerUp(0);

    }


    /// <summary>
    /// Power up alýnca yanyana olan iki power upýda kapatýr.
    /// </summary>
    /// <param name="index"></param>
    public void Transition(int index)
    {
        if (transform.GetChild(index).GetComponent<PowerUp>() != null && transform.GetChild(index).GetComponent<PowerUp>() != null)


        {
            if (transform.GetChild(index+1).GetComponent<PowerUp>().CreateClonePlayer || transform.GetChild(index).GetComponent<PowerUp>().CreateClonePlayer)
            {
                transform.GetChild(index).GetComponent<PowerUp>().CreateClonePlayer = false;
                transform.GetChild(index+1).GetComponent<PowerUp>().CreateClonePlayer = false;
                StartCoroutine(Destroy());

            }
        }



    }
    /// <summary>
    /// Power objesinin altýndaki ilk iki objede power upscripti var mý kontrol eder.
    /// </summary>
    /// <param name="index"></param>
    private void IsPowerUp(int index)
    {
        if (transform.GetChild(index).GetComponent<PowerUp>() != null)
        {
            clonePowerUp = transform.GetChild(0).GetComponent<PowerUp>().CreateClonePlayer;

        }
        if (transform.GetChild(index).GetComponent<PowerUp>() != null)
        {
            clonePowerUp2 = transform.GetChild(1).GetComponent<PowerUp>().CreateClonePlayer;

        }
    }
    IEnumerator Destroy()
    {
       yield return new WaitForSeconds(1.5f);
       Destroy(gameObject);
    }

}
