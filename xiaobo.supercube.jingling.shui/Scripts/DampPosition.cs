using UnityEngine;

public class DampPosition : MonoBehaviour
{
    public Transform targetGameObject;
    public float damp = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetGameObject.position, damp);
    }
}
