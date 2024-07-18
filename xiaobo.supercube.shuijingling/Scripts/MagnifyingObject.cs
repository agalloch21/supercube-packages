using UnityEngine;

public class MagnifyingObject : MonoBehaviour
{
    [SerializeField] Camera _cam;

    Renderer _renderer;
    
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Vector3 screen_point = _cam.WorldToScreenPoint(transform.position);
        screen_point.x = screen_point.x / Screen.width;
        screen_point.y = screen_point.y / Screen.height;
        _renderer.material.SetVector("_ObjScreenPos", screen_point);
    }
}
