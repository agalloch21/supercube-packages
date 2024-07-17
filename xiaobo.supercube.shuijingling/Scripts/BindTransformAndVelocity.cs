using UnityEngine;

public class BindTransformAndVelocity : MonoBehaviour
{
    public Transform targetGameObject;
    Vector3 lastPos;
    Vector3 lastVel;
    MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        lastPos = transform.position;
        lastVel = Vector3.zero;
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
        vel = Vector3.SmoothDamp(lastVel, vel, ref speed, 0.4f);
        lastVel = vel;

        meshRenderer.material.SetVector("_Parent", targetGameObject.position);
        meshRenderer.material.SetVector("_ParentVel", vel);
    }
}
