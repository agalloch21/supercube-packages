using UnityEngine;
using UnityEngine.VFX;

public class BindOrientation : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] VisualEffect vfx;
    [SerializeField] Transform matPlane;
    public float deg = 90;
    public Vector3 offset = Vector3.zero;

    public float weight = 1.0f;

    Vector3 velocity;
    Vector3 lastPos;

    Vector3 screenPos;
    Vector3 lastScreenPos;

    Vector3 screenVel;
    Vector3 orientation;
    float screenOrientation;
    void Start()
    {
        lastPos = transform.position;
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        velocity = (transform.position - lastPos) / Time.deltaTime;
        transform.up = Vector3.Lerp(Vector3.up, velocity.normalized, weight);
        lastPos = transform.position;

        screenPos = cam.WorldToScreenPoint(transform.position);
        screenVel = (screenPos - lastScreenPos) / Time.deltaTime;
        lastScreenPos = screenPos;

        orientation = transform.eulerAngles;
        screenOrientation = Mathf.Atan2(screenVel.y, screenVel.x);

        vfx.SetFloat("AngleZ", screenOrientation);

        matPlane.eulerAngles = new Vector3(0, 0, -90);
        matPlane.position = transform.position;
        matPlane.Rotate(Vector3.forward, screenOrientation * Mathf.Rad2Deg + deg);
        matPlane.position += matPlane.TransformVector(offset);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, cam.ScreenToWorldPoint(screenPos + screenVel));
    }
}
