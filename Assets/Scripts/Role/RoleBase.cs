using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleBase : MonoBehaviour
{
    public MoveCtrl pMoveCtrl;

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    //类型暂时没用，只取坐标
    public virtual void SitDown(Transform tfChair,int chairType)
    {

    }
    public virtual void StandUp(){

    }

}
