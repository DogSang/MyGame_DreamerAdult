using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTrigger : MonoBehaviour
{
    public ActionBase pAction;

    private void Start()
    {
        if (pAction == null)
            Destroy(this.gameObject);
    }
    public virtual void OnActionActive()
    {
        pAction.OnActionActive();
    }

    public virtual void OnActionUnactive()
    {
        pAction.OnActionUnactive();
    }
}
