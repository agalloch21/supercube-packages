using UnityEngine;

public class FaceAlongVelocity : MonoBehaviour
{
    public float weight = 1.0f;
    Vector3 velocity;
    Vector3 lastPos;
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
    }
}
