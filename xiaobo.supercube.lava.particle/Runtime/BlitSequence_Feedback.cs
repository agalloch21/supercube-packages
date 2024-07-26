using UnityEngine;
using static Unity.VisualScripting.Member;

public class BlitSequence_Feedback : MonoBehaviour
{
    public RenderTexture sourceRT;
    public RenderTexture destinationRT;
    public RenderTexture backupRT;
    public Material matFeedback;
    public bool resetBuffer;

    void Start()
    {
        ClearOutRenderTexture(destinationRT);
        ClearOutRenderTexture(backupRT);
    }

    void Update()
    {
        // Set Backup Render Texture as LastFrameTexture
        matFeedback.SetTexture("_LastFrameTexture", backupRT);

        // Feedback effect.
        // sourceRT: RenderTexture 
        // destinationRT: A temporary destination Render Texture
        Graphics.Blit(sourceRT, destinationRT, matFeedback, 0);

        // Copy temporary destination Render Texture to Backup Render Texture
        Graphics.Blit(destinationRT, backupRT);

        if(resetBuffer )
        {
            ClearOutRenderTexture(destinationRT);
            ClearOutRenderTexture(backupRT);
        }    
    }

    public void ClearOutRenderTexture(RenderTexture renderTexture)
    {
        RenderTexture rt = RenderTexture.active;
        RenderTexture.active = renderTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = rt;
    }
}
