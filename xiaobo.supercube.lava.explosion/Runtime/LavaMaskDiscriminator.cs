using Unity.Mathematics;
using UnityEngine;

public class LavaMaskDiscriminator : MonoBehaviour
{
    public RenderTexture lavaMask;

    Vector3 bounds = new Vector3(11.5f, 0, 18.09f);

    Texture2D tempTex;

    public float checkRadius = 1;
    Vector2 [] offsetList =  new[] { new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, -1), new Vector2(0, 1) };

    void Start()
    {
        if (lavaMask == null)
            return;

        tempTex = new Texture2D(lavaMask.width, lavaMask.height);
    }

    void Update()
    {
        if (lavaMask == null || tempTex == null)
            return;

        RenderTexture temp = RenderTexture.active;
        RenderTexture.active = lavaMask;
        tempTex.ReadPixels(new Rect(0, 0, tempTex.width, tempTex.height), 0, 0);
        tempTex.Apply();
        RenderTexture.active = temp;
    }


    public float IsAboveLava(Vector3 pos)
    {
        Vector2 uv = WorldPosToUV(pos);

        float max_value = tempTex.GetPixel((int)((uv.x) * tempTex.width), (int)((uv.y) * tempTex.height)).r;
        for (int i=0; i<offsetList.Length; i++)
        {
            uv = WorldPosToUV(pos + new Vector3(offsetList[i].x, 0, offsetList[i].y) * checkRadius);
            float v = tempTex.GetPixel((int)((uv.x) * lavaMask.width), (int)((uv.y) * lavaMask.height)).r;
            if (v > max_value)
                max_value = v;
            if (max_value == 1)
                break;
        }

        return max_value;
    }

    Vector2 WorldPosToUV(Vector3 pos)
    {
        Vector2 uv = Vector2.zero;

        uv.x = Remap_Dirty(pos.x, bounds.x * -0.5f, bounds.x * 0.5f, 0, 1f);
        uv.y = Remap_Dirty(pos.z, bounds.z * -0.5f, bounds.z * 0.5f, 0, 1f);

        return uv;
    }

    float Remap_Dirty(float v, float src_min, float src_max, float dst_min, float dst_max)
    {
        v = Mathf.Clamp(v, src_min, src_max);

        return (v - src_min) / (src_max - src_min) * (dst_max - dst_min) + dst_min;
    }
}
