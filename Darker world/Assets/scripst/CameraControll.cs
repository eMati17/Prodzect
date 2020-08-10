using System.Collections;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float smoothSpeed = .125f;
    [SerializeField] Vector3 offset;
    
  
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, smoothSpeed);
    }
}
