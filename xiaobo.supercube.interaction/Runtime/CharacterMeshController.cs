using UnityEngine;

public class CharacterMeshController : MonoBehaviour
{
    public Transform targetCamera;
    private Vector3 lastFramePosition;
    private Animator anim;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        lastFramePosition = targetCamera.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(targetCamera.position.x, transform.position.y, targetCamera.position.y);
        transform.eulerAngles = new Vector3(0, targetCamera.eulerAngles.y,0);

        
        Vector3 direction = transform.position - lastFramePosition;
        float speed = direction.magnitude / Time.deltaTime;
        lastFramePosition = transform.position;

        Vector3 foward = targetCamera.forward;
        if (Vector3.Dot(foward, direction) < 0)
            speed = -speed;

        anim.SetFloat("Speed", speed);

    }
}
