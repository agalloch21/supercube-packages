using UnityEngine;
using Xiaobo.UnityToolkit.Damper;

public class BindTransformAndVelocity : MonoBehaviour
{
    public Transform targetGameObject;
    Vector3 lastPos;
    Vector3 lastVel;
    MeshRenderer meshRenderer;

    SecondOrderDynamics_V3 sod;
    public float f = 0.1f;
    public float z = 0.1f;
    public float r = 0.1f;
    public float smoothTime = 0.2f;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        lastPos = transform.position;
        lastVel = Vector3.zero;

        sod = new SecondOrderDynamics_V3(f, z, r, lastVel);
    }


    void Update()
    {
        if (meshRenderer == null)
            meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer == null || targetGameObject == null)
            return;

        
        Vector3 vel = (targetGameObject.position - lastPos) / Time.deltaTime;
        lastPos = targetGameObject.position;

        
        Vector3 speed = Vector3.zero;
        vel = Vector3.SmoothDamp(lastVel, vel, ref speed, smoothTime);
        lastVel = vel;


        //Vector3 xd = (vel - lastVel) / Time.deltaTime;
        //lastVel = vel;

        //sod.SetParameters(f, z, r);
        //vel = sod.Update(Time.deltaTime, vel, xd);


        meshRenderer.material.SetVector("_Parent", targetGameObject.position);
        meshRenderer.material.SetVector("_ParentVel", vel);
    }
}
