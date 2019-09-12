using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBase_WithTrigger : ActionBase
{
    public SpriteRenderer[] pOutLineSpriteArray;
    public ActionTrigger[] pTriggerArray;
    protected Material mat;
    protected virtual void Awake()
    {
        mat = MaterialFactory.GetSpirteOutLineMaterial(Color.green);
        for (int i = 0; i < pOutLineSpriteArray.Length; i++)
        {
            pOutLineSpriteArray[i].material = mat;
        }

        for (int i = 0; i < pTriggerArray.Length; i++)
        {
            pTriggerArray[i].pAction = this;
        }
    }

    public override void OnActionActive()
    {
        base.OnActionActive();
        mat.EnableKeyword("_ShowOutline");
    }

    //TODO 后续可能做事件选择不是一开始就激活
    // public virtual void OnActionBeChoose(bool bChoose) { }

    //玩家离开了触发器
    public override void OnActionUnactive()
    {
        base.OnActionUnactive();
        mat.DisableKeyword("_ShowOutline");
    }

    //事件开始了
    public override void OnActionStart(RoleBase role)
    {
        base.OnActionStart(role);
    }

    //玩家手动结束事件
    public override void OnActionExit(RoleBase role)
    {
        base.OnActionExit(role);
    }
}
