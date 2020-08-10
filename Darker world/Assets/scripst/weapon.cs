using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Shoot());

        }
       
       IEnumerator Shoot()
        {
            Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
            yield return new WaitForSeconds(1);

            
        }
    }
}
