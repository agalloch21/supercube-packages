using System.Collections.Generic;
using UnityEngine;

public class LavaEffectController : MonoBehaviour
{
    public Vector2 upperOffsetSpeed;
    public float upperFlowSpeed = 0.1f;
    public float upperFlowStrength = 1f;

    public Vector2 lowerOffsetSpeed;
    
    public List<Material> matList;

    public Material matHeightmap;
    public RenderTexture textureHeightmap;
    public RenderTexture textureBlank;

    Vector2 upperOffset = Vector2.zero;
    Vector2 lowerOffset = Vector2.zero;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        upperOffset += Time.deltaTime * upperOffsetSpeed;
        lowerOffset += Time.deltaTime * lowerOffsetSpeed;

        foreach (Material mat in matList)
        {
            mat.SetVector("_UVOffset_UpperLayer", upperOffset);
            mat.SetFloat("_FlowSpeed_UpperLayer", upperFlowSpeed);
            mat.SetFloat("_FlowStrength_UpperLayer", upperFlowStrength);
            
            //mat.SetVector("_UVOffset_LowerLayer", lowerOffset);
        }


        // 
        //Graphics.Blit(textureBlank, textureHeightmap, matHeightmap);
    }
}
