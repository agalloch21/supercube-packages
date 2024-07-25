using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera _cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = _cam.transform.position - transform.position;
    }
}
