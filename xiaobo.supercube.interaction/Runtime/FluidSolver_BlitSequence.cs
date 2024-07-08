using UnityEngine;

public class FluidSolver_BlitSequence : MonoBehaviour
{
    public RenderTexture texTotallyBlack;

    public RenderTexture texStep1;
    public Material matStep1;

    public RenderTexture texStep2;
    public RenderTexture texStep2_Swap;
    public Material matStep2;

    public RenderTexture texStep3;
    public Material matStep3;

    public RenderTexture texStep4;
    public RenderTexture texStep4_Swap;
    public Material matStep4;


    bool initialized = false;
    bool needSwap = false;
    bool needSwap4 = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        matStep1.SetTexture("_VelocityTexture", texStep3);
        Graphics.Blit(texTotallyBlack, texStep1, matStep1);

        for(int i=0; i<50; i++)
        {
            if(needSwap)
            {
                matStep2.SetTexture("_VelocityTexture", texStep1);
                matStep2.SetTexture("_PressureTexture", texStep2);
                Graphics.Blit(texTotallyBlack, texStep2_Swap, matStep2);
            }
            else
            {
                matStep2.SetTexture("_VelocityTexture", texStep1);
                matStep2.SetTexture("_PressureTexture", texStep2_Swap);
                Graphics.Blit(texTotallyBlack, texStep2, matStep2);
            }
            needSwap = !needSwap;
        }
            

        matStep3.SetTexture("_VelocityTexture", texStep1);
        matStep3.SetTexture("_PressureTexture", texStep2_Swap);
        Graphics.Blit(texTotallyBlack, texStep3, matStep3);

        if(needSwap4)
        {
            matStep4.SetTexture("_VelocityTexture", texStep3);
            matStep4.SetTexture("_TargetTexture", texStep4_Swap);
            Graphics.Blit(texTotallyBlack, texStep4, matStep4);
        }
        else
        {
            matStep4.SetTexture("_VelocityTexture", texStep3);
            matStep4.SetTexture("_TargetTexture", texStep4);
            Graphics.Blit(texTotallyBlack, texStep4_Swap, matStep4);
        }
        needSwap4 = !needSwap4;

    }
}
