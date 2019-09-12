using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCtrl : MonoBehaviour
{
    private bool bCanMoveRight = true;
    private bool bCanMoveLeft = true;

    public void Move(Vector3 moveDis)
    {
        transform.position += moveDis;
    }
    public void SimpleMove(Vector3 moveDir, float dt)
    {
        if ((moveDir.x > 0 && !bCanMoveRight) || (moveDir.x < 0 && !bCanMoveLeft))
        {
            moveDir.x = 0;
        }

        transform.position += moveDir * dt;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Move_Obs") return;

        if (other.transform.position.x > this.transform.position.x)
        {
            bCanMoveRight = false;
        }
        else if (other.transform.position.x < this.transform.position.x)
        {
            bCanMoveLeft = false;
        }
    }
    // private void OnTriggerStay2D(Collider2D other)
    // {

    // }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Move_Obs") return;
        
        if (other.transform.position.x > this.transform.position.x)
        {
            bCanMoveRight = true;
        }
        else if (other.transform.position.x < this.transform.position.x)
        {
            bCanMoveLeft = true;
        }
    }
}
