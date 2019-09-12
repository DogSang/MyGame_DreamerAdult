using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTool_SwitchSprite_Mono : MonoBehaviour
{
    public MyTool_SwitchSprite pSwitchSprtite;
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public float fIntervalTime;//每次切换的间隔时间


    // Start is called before the first frame update
    void Start()
    {
        pSwitchSprtite = new MyTool_SwitchSprite(spriteRenderer, sprites, fIntervalTime);
    }

    // Update is called once per frame
    void Update()
    {
        pSwitchSprtite.OnUpdate(Time.deltaTime);
    }
}

public class MyTool_SwitchSprite
{
    public MyTool_SwitchSprite(SpriteRenderer spriteRenderer, Sprite[] sprites, float fIntervalTime)
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("New MyTool_SwitchSprite Error:" + " spriteRenderer is null");
        }
        else if (sprites == null || sprites.Length == 0)
        {
            Debug.LogError("New MyTool_SwitchSprite Error:" + " sprites is null , or sprites length is 0");
        }
        else if (fIntervalTime <= 0)
        {
            Debug.LogError("New MyTool_SwitchSprite Error:" + " fIntervalTime <= 0");
        }


        this.spriteRenderer = spriteRenderer;
        this.sprites = sprites;
        this.fIntervalTime = fIntervalTime;

        nCurIndex = 0;
        fCurTime = 0;
    }

    private SpriteRenderer spriteRenderer;
    private Sprite[] sprites;
    private float fIntervalTime;//每次切换的间隔时间

    private int nCurIndex;
    private float fCurTime;


    public void OnUpdate(float time)
    {
        fCurTime += time;
        int num = (int)(fCurTime / fIntervalTime);//TODO FUTURE 不用除法，改成循环的减法
        fCurTime -= num * fIntervalTime;

        int spriteNum = sprites.Length;
        nCurIndex += num;
        while (nCurIndex < 0)
        {
            nCurIndex += spriteNum;
        }
        nCurIndex %= spriteNum;

        SetSprite(nCurIndex);
    }

    private void SetSprite(int index)
    {
        spriteRenderer.sprite = sprites[index];
    }
}