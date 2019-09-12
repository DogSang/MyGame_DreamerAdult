using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerCtrl : MonoBehaviour
{
    public Role_Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Action_Trigger") return;
        ActionTrigger trigger = other.GetComponent<ActionTrigger>();
        trigger.OnActionActive();

        player.pCurAction = trigger.pAction;
    }
    // private void OnTriggerStay2D(Collider2D other)
    // {

    // }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Action_Trigger") return;

        ActionTrigger trigger = other.GetComponent<ActionTrigger>();
        trigger.OnActionUnactive();

        player.pCurAction = null;
    }
}
