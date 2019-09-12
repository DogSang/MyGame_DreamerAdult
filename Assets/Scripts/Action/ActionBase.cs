using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家互动的事件
public class ActionBase : MonoBehaviour
{
    public bool bOnActive = false;//是否是激活状态
    public bool bOnAction = false;//是否是活动状态

    //玩家碰到触发器激活
    public virtual void OnActionActive()
    {
        bOnActive = true;
    }

    //TODO 后续可能做事件选择不是一开始就激活
    // public virtual void OnActionBeChoose(bool bChoose) { }

    //玩家离开了触发器
    public virtual void OnActionUnactive()
    {
        bOnActive = false;
    }

    //事件开始了
    public virtual void OnActionStart(RoleBase role)
    {
        bOnAction = true;
    }

    //玩家手动结束事件
    public virtual void OnActionExit(RoleBase role)
    {
        bOnAction = false;

    }
}
