using UnityEngine;

public class NormalMap_BlitSequence : MonoBehaviour
{
    public RenderTexture sourceRT;
//    public RenderTexture blurredRT;
//    public Material matBlur;
    public RenderTexture destinationRT;
    public Material matNormalMap;

    // Update is called once per frame
    void Update()
    {
        //Graphics.Blit(sourceRT, blurredRT, matBlur, 0);

        Graphics.Blit(sourceRT, destinationRT, matNormalMap, 0);
    }
}
