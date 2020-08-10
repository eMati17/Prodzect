using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float deadzone = .2f;
    public Joystick joystick;
    float horizontal;
    float vertical;
    public float speed = 10;
    Vector2 move;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    
    void Update()
    {
        move = new Vector2(horizontal, vertical)*speed;
        rb.velocity = move;

#if UNITY_EDITOR
        horizontal =Input.GetAxis("Horizontal");
        vertical =Input.GetAxis("Vertical");
#endif 
        
        if (joystick.Horizontal >= deadzone)
        {
            horizontal = 1;
            transform.eulerAngles = new Vector3(0,0);
        }
        else if (joystick.Horizontal <= -deadzone)
        {
            horizontal = -1;
            transform.eulerAngles = new Vector3(0,180);
        }
        if (joystick.Vertical >= deadzone)
        {
            vertical = 1;
        }
        else if (joystick.Vertical <= -deadzone)
        {
            vertical = -1;
        }
    }
}
