using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisButtons : MonoBehaviour
{
    public float movement=0f;
    public bool pressed=false;
    public float jumpTimer=0f;
    public float jumpTime;
    private bool isJumping;

    private void Start()
    {
     
    }

    private void Update()
    {
        //if (settings.control == true)
      //  PlayerController.instance.movement =  Mathf.Lerp( PlayerController.instance.movement, joystick.Horizontal, 10* Time.deltaTime );
       // else
      //  {
            PlayerController.instance.movement = movement;
      //  }
        
        /*if (jumpTimer>0)
        {
            jumpTimer -= 8*Time.deltaTime;
        }
        
        if (pressed && jumpTimer<=0)
        {
            if (jumpTimer <= 0)
            {
                jumpTimer = jumpTime;
                if(isJumping)
            
            }
        }*/
    }

    public void move(float movement)
    {
       this.movement = movement;
     //  StartCoroutine(Slowly(movement))
    }


    /*private IEnumerator Slowly(float movement)
    {
        movement =  Mathf.Lerp( this.movement, movement,  Time.deltaTime );
    }*/
    
    /*public void Up()
    {
        pressed = true;
        isJumping = true;
       // StartCoroutine(Wait());
    }*/

    public void Jump()
    {
        PlayerController.instance.JumpButton();
    }

    public void Attack()
    {
        PlayerController.instance.Attack();
    }
    /*public void StopUp()
    {
        pressed = false;
        jumpTimer = 0f;
    }*/

    // private IEnumerator Wait()
    // {
    //     yield return new WaitForSeconds(0.1f);
    //     isJumping = false;
    //     yield return new WaitForSeconds(0.1f);
    //     isJumping = true;
    // }

}
