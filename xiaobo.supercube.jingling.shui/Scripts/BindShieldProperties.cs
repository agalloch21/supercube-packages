using UnityEngine;

public class BindShieldProperties : MonoBehaviour
{
    public Transform followedGameObject;
    public Vector3 facingPosition;
    public float smoothDamp = 0.1f;

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
        // Facing camera
        Quaternion cur_rotation = transform.rotation;
        transform.LookAt(facingPosition, Vector3.up);
        Quaternion dst_rotation = Quaternion.Slerp(cur_rotation, transform.rotation, smoothDamp);
        transform.rotation = dst_rotation;



        // Set properties
        if (meshRenderer == null)
            meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer == null || followedGameObject == null)
            return;


        Vector3 vel = (followedGameObject.position - lastPos) / Time.deltaTime;
        lastPos = followedGameObject.position;

        vel = Vector3.Lerp(lastVel, vel, smoothDamp);
        lastVel = vel;

        meshRenderer.material.SetVector("_Parent", followedGameObject.position);
        meshRenderer.material.SetVector("_ParentVel", vel);
    }
}
