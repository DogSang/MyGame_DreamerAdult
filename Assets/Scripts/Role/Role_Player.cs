using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Role_Player : RoleBase
{
    public SpriteRenderer pRoleSpriteRenderer;
    public Sprite[] pRoleSrpiteArray;

    public Transform tfRoleSpriteRoot;
    public PlayerTriggerCtrl pTriggerCtrl;

    private bool bCanCtrl = false;

    private bool bCanMove = true;
    private bool bCanRotate = true;
    private bool bLookRight = true;
    public bool IsLookRight
    {
        get { return bLookRight; }
    }

    public bool bDebug = false;
    private void Awake()
    {
        pTriggerCtrl.player = this;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        if (bDebug)
            SetCanCtrl(true);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!bCanCtrl) return;
        OnMove();
        OnRotate();
        OnCtrl();
    }

    public void SetCanCtrl(bool canCtrl)
    {
        bCanCtrl = canCtrl;
    }

    private void OnRotate()
    {
        if (!bCanRotate) return;

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && bLookRight)
        {
            bLookRight = false;
            tfRoleSpriteRoot.DOLocalRotate(Vector3.up * 180, 0.4f, RotateMode.Fast);
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && !bLookRight)
        {
            bLookRight = true;
            tfRoleSpriteRoot.DOLocalRotate(Vector3.zero, 0.4f, RotateMode.Fast);
        }


    }
    private void OnMove()
    {
        if (!bCanMove) return;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            pMoveCtrl.SimpleMove(Vector3.left * pRoleData.fMoveSpeed, Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            pMoveCtrl.SimpleMove(Vector3.right * pRoleData.fMoveSpeed, Time.deltaTime);
        }
    }

    public ActionBase pCurAction;
    private void OnCtrl()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (pCurAction != null && !pCurAction.bOnAction)
                pCurAction.OnActionStart(this);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (pCurAction != null && pCurAction.bOnAction)
                pCurAction.OnActionExit(this);
        }
    }

    public override void SitDown(Transform tfChair, int chairType)
    {
        bCanMove = false;
        bCanRotate = false;

        bLookRight = true;
        tfRoleSpriteRoot.DOLocalRotate(Vector3.up * 90f, 0.2f, RotateMode.Fast).OnComplete(() =>
            {
                tfRoleSpriteRoot.position = tfChair.position;
                pRoleSpriteRenderer.sprite = pRoleSrpiteArray[1];
                tfRoleSpriteRoot.DOLocalRotate(Vector3.up * 0, 0.2f, RotateMode.Fast).OnComplete(() =>
                {
                    bCanRotate = true;
                });
            });
    }
    public override void StandUp()
    {
        bCanRotate = true;
        bCanMove = false;
        bLookRight = true;

        tfRoleSpriteRoot.DOLocalRotate(Vector3.up * 90f, 0.2f, RotateMode.Fast).OnComplete(() =>
            {
                tfRoleSpriteRoot.localPosition = Vector3.zero;
                pRoleSpriteRenderer.sprite = pRoleSrpiteArray[0];
                tfRoleSpriteRoot.DOLocalRotate(Vector3.up * 0, 0.2f, RotateMode.Fast).OnComplete(() =>
                {
                    bCanRotate = true;
                    bCanMove = true;
                });
            });
    }
}
