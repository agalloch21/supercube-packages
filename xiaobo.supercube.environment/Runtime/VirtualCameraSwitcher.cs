using UnityEngine;
using Unity.Cinemachine;
using System;

namespace supercube.environment
{

    [RequireComponent(typeof(CinemachineMixingCamera))]
    public class VirtualCameraSwitcher : MonoBehaviour
    {
        public GameObject firstPersonController;
        CinemachineMixingCamera mixingCamera;

        void Update()
        {
            if(mixingCamera == null)
                mixingCamera = GetComponent<CinemachineMixingCamera>();

            if (mixingCamera != null)
            {
                for (int i = 0; i < mixingCamera.ChildCameras.Count; i++)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                    {
                        EnableCamera(i);
                        break;
                    }
                }
            }
        }

        void EnableCamera(int index)
        {
            for (int i = 0; i < mixingCamera.ChildCameras.Count; i++)
            {
                if (i == index)
                    mixingCamera.SetWeight(i, 1);
                else
                    mixingCamera.SetWeight(i, 0);
            }

            // Enable FirstPersonController
            if (index == 2)
            {
                firstPersonController.SetActive(true);
            }
            else
            {
                firstPersonController.SetActive(false);
            }
        }
    }
}
