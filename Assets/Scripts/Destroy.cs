using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update


   void OnEnable()
    {
        StartCoroutine(DestroyFx());
    }

    IEnumerator DestroyFx()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
