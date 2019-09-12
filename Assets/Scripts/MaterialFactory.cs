using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialFactory
{
    public static Material GetSpirteOutLineMaterial(Color color)
    {
        Material material = new Material(Shader.Find("Custom/SpriteOutline"));
        // material.SetInt("_DualGrid", 1);                    //Toggle开关来控制是否显示边缘
        // material.SetFloat("_EdgeAlphaThreshold", 1f);	//边界透明度和的阈值
        material.SetColor("_EdgeColor", color);             //边界颜色
        // material.SetFloat("_EdgeDampRate", 2);           //边缘渐变的分母
        // material.SetFloat("_OriginAlphaThreshold", 0.2f);//原始颜色透明度剔除的阈值
        return material;
    }
}
