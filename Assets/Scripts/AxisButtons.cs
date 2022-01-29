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



    private void Update()
    {
     
            PlayerController.instance.movement = movement;

    }

    public void move(float movement)
    {
       this.movement = movement;
   
    }



    public void Jump()
    {
        PlayerController.instance.JumpButton();
    }

    public void Attack()
    {
        PlayerController.instance.Attack();
    }
   
}
