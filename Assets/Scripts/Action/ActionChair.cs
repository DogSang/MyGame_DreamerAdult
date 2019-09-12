using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChair : ActionBase_WithTrigger
{
    public Transform tfChairRoot;
    public int nChairType = 0;

    protected override void Awake()
    {
        base.Awake();
    }

    //玩家碰到触发器激活
    public override void OnActionActive()
    {
        base.OnActionActive();

    }
    //玩家离开了触发器
    public override void OnActionUnactive()
    {
        base.OnActionUnactive();

    }

    //事件开始了
    public override void OnActionStart(RoleBase role)
    {
        base.OnActionStart(role);
        role.SitDown(this.transform, nChairType);
    }

    //玩家手动结束事件
    public override void OnActionExit(RoleBase role)
    {
        base.OnActionExit(role);
        role.StandUp();
    }


}
