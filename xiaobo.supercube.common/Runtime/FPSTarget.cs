using UnityEngine;

public class FPSTarget : MonoBehaviour
{

    public int target = 30;

    void OnEnable()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;
    }
}
