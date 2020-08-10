using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiming : MonoBehaviour
{
    public Joystick joystick;
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weapon;
   

    
    void Update()
    {

        
        if (transform.eulerAngles.y == 0) 
        {
            if (joystick.Direction.x >= 0)
            {
                weaponHolder.transform.eulerAngles = new Vector3(0, 0 + transform.eulerAngles.y, joystick.Direction.y * 90);
            }
            else
            {
                weaponHolder.transform.eulerAngles = new Vector3(0, 180 + transform.eulerAngles.y, joystick.Direction.y * 90);
            }
            

        }
        else
        {
            if (joystick.Direction.x <= 0)
            {
                weaponHolder.transform.eulerAngles = new Vector3(0, 0 + transform.eulerAngles.y, joystick.Direction.y * 90);
            }
            else
            {
                weaponHolder.transform.eulerAngles = new Vector3(0, 180 + transform.eulerAngles.y, joystick.Direction.y * 90);
            }
            
        }
        weaponHolder.transform.position = new Vector3(weaponHolder.transform.position.x, weaponHolder.transform.position.y, -1);
    }
}
