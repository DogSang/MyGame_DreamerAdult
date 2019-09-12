using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ActionDoor : ActionBase_WithTrigger
{
    public SpriteRenderer spriteRendererDoor;
    public Sprite[] spritesDoor;
    public bool bOpen;
    public Collider2D objCol;

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
        bOpen = !bOpen;

        spriteRendererDoor.transform.DOLocalRotate(Vector3.up * 90, 0.2f).OnComplete(() =>
        {
            spriteRendererDoor.sprite = spritesDoor[bOpen ? 0 : 1];
            spriteRendererDoor.transform.DOLocalRotate(Vector3.up * 0, 0.2f).OnComplete(() =>
            {
                bOnAction = false;
                objCol.gameObject.SetActive(!bOpen);
            });
        });
    }

    //玩家手动结束事件
    public override void OnActionExit(RoleBase role)
    {
        base.OnActionExit(role);
    }
}
