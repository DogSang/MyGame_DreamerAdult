using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMBase
{
    public static FSMBase GetNullFSM()
    {
        return new FSMBase(null, null, null);
    }

    private System.Action onStartDel;
    private System.Action<float> onUpdateDel;
    private System.Action onEndDel;

    public FSMBase(System.Action onStartDel, System.Action<float> onUpdateDel, System.Action onEndDel)
    {
        this.onStartDel = onStartDel;
        this.onUpdateDel = onUpdateDel;
        this.onEndDel = onEndDel;
    }

    public virtual void OnFSMStart()
    {
        if (onStartDel != null)
            onStartDel();
    }

    public virtual void OnFSMUpdate(float dt)
    {
        if (onUpdateDel != null)
            onUpdateDel(dt);
    }

    public virtual void OnFSMEnd()
    {
        if (onEndDel != null)
            onEndDel();
    }

    public virtual void NextFSM(FSMBase curFSM)
    {
        OnFSMEnd();

        onStartDel = curFSM.onStartDel;
        onUpdateDel = curFSM.onUpdateDel;
        onEndDel = curFSM.onEndDel;

        OnFSMStart();
    }
}
