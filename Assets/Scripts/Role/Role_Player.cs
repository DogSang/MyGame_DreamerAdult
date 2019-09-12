using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Role_Player : RoleBase
{
    public float fMoveSpeed = 2f;

    public SpriteRenderer pRoleSpriteRenderer;
    public Sprite[] pRoleSrpiteArray;

    public Transform tfRoleSpriteRoot;
    public PlayerTriggerCtrl pTriggerCtrl;


    private bool bLookRight = true;
    public bool IsLookRight
    {
        get { return bLookRight; }
    }

    public bool bDebug = false;

    private FSMBase curFSM;
    [SerializeField]
    private EM_PlayerStage eM_PlayerStage;

    private void Awake()
    {
        pTriggerCtrl.player = this;
        curFSM = FSMBase.GetNullFSM();
        SwitchPlayerStage(EM_PlayerStage.EM_Wait, GetFSM_WaitStage());
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        if (bDebug)
            SwitchPlayerStage(EM_PlayerStage.EM_Ctrl, GetFSM_CtrlStage());
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (curFSM != null)
            curFSM.OnFSMUpdate(Time.deltaTime);
    }

    private void SwitchPlayerStage(EM_PlayerStage stage, FSMBase nextFSM)
    {
        eM_PlayerStage = stage;
        curFSM.NextFSM(nextFSM);
    }

    private void OnRotate()
    {
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
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            pMoveCtrl.SimpleMove(Vector3.left * fMoveSpeed, Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            pMoveCtrl.SimpleMove(Vector3.right * fMoveSpeed, Time.deltaTime);
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
        SwitchPlayerStage(EM_PlayerStage.EM_SitDown, GetFSM_SitDownStage(tfChair, chairType));
    }
    public override void StandUp()
    {
        SwitchPlayerStage(EM_PlayerStage.EM_StandUp, GetFSM_StandUpStage());
    }

    #region FSM  factory
    private FSMBase GetFSM_WaitStage()
    {
        return FSMBase.GetNullFSM();
    }
    private FSMBase GetFSM_CtrlStage()
    {
        return new FSMBase(null, (float dt) =>
        {
            OnCtrl();
            OnRotate();
            OnMove();
        }, null);
    }
    private FSMBase GetFSM_SitDownStage(Transform tfChair, int chairType)
    {
        bool bCanRotate = false;

        System.Action onStart = () =>
        {
            bCanRotate = false;
            bLookRight = true;
            tfRoleSpriteRoot.DOLocalRotate(Vector3.up * 90f, 0.2f, RotateMode.Fast).OnComplete(() =>
                {
                    transform.position = new Vector3(tfChair.position.x, transform.position.y, transform.position.z);
                    tfRoleSpriteRoot.position = tfChair.position;
                    pRoleSpriteRenderer.sprite = pRoleSrpiteArray[1];
                    tfRoleSpriteRoot.DOLocalRotate(Vector3.up * 0, 0.2f, RotateMode.Fast).OnComplete(() =>
                        {
                            bCanRotate = true;
                        });
                });
        };
        System.Action<float> onUpdate = (float dt) =>
        {
            if (bCanRotate)
                OnRotate();

            OnCtrl();
        };

        return new FSMBase(onStart, onUpdate, null);
    }
    private FSMBase GetFSM_StandUpStage()
    {
        bool bStandOver = false;

        System.Action onStart = () =>
        {
            bStandOver = false;
            bLookRight = true;

            tfRoleSpriteRoot.DOLocalRotate(Vector3.up * 90f, 0.2f, RotateMode.Fast).OnComplete(() =>
                {
                    tfRoleSpriteRoot.localPosition = Vector3.zero;
                    pRoleSpriteRenderer.sprite = pRoleSrpiteArray[0];
                    tfRoleSpriteRoot.DOLocalRotate(Vector3.up * 0, 0.2f, RotateMode.Fast).OnComplete(() =>
                    {
                        bStandOver = true;
                    });
                });
        };
        System.Action<float> onUpdate = (float dt) =>
        {
            //TODO 暂时写死站起之后挑战的玩家操作状态
            if (bStandOver)
                SwitchPlayerStage(EM_PlayerStage.EM_Ctrl, GetFSM_CtrlStage());
        };

        return new FSMBase(onStart, onUpdate, null);
    }

    #endregion
}

public enum EM_PlayerStage
{
    EM_Wait,
    EM_Ctrl,
    EM_SitDown,
    EM_StandUp,
    EM_OpenBag,
}