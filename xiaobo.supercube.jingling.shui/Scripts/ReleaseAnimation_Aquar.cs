using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class ReleaseAnimation_Aquar : MonoBehaviour
{
    [SerializeField]
    VisualEffect vfx;
    [SerializeField]
    SkinnedMeshRenderer meshRenderer;
    [SerializeField]
    MeshRenderer shiledRenderer;

    public bool simulateReleaseEvent;
    public bool simulateResetEvent;


    float totalTime = 6;
    float shieldAppearTime = 3.1f;
    float shieldAppearDuration = 2;
    float meshAppearTime = 4.1f;

    float elapsedTime = 0;
    float animationTime = 0;

    void Update()
    {
        if (simulateReleaseEvent)
        {
            simulateReleaseEvent = false;
            Release();
        }
        if (simulateResetEvent)
        {
            simulateResetEvent = false;
            Reset();
        }
    }
    public void Release()
    {
        animationTime = Time.time;
        elapsedTime = 0;

        StartCoroutine(ReleaseAnimation());
    }

    IEnumerator ReleaseAnimation()
    {
        vfx.SendEvent("DoRelease");

        while (Time.time - animationTime < totalTime)
        {
            elapsedTime = Time.time - animationTime;
            if (elapsedTime >= shieldAppearTime && elapsedTime <= shieldAppearTime+ shieldAppearDuration)
            {
                shiledRenderer.material.SetFloat("_Animation", (elapsedTime - shieldAppearTime) / shieldAppearDuration);
            }

            if(elapsedTime >= meshAppearTime)
            {
                meshRenderer.enabled = true;
            }

            yield return new WaitForSeconds(0.1f);
        }        
        animationTime = 0;
    }

    public void Reset()
    {
        shiledRenderer.material.SetFloat("_Animation", 0);
        meshRenderer.enabled = false;
    }

}
