using UnityEngine;

public class FluidMouseInput : MonoBehaviour
{
    public bool useScreenMouse = false;
    public Transform bindedGameObject;
    public Material matStep1;
    public Material matStep4;
    Vector2 mousePos = Vector2.zero;
    Vector2 lastMousePos = Vector2.zero;

    // Update is called once per frame
    

    void Update()
    {
        Vector2 screen_pos = Vector2.zero;
        bool mouseClick = true;
        if(useScreenMouse)
        {
            if (Input.GetMouseButton(0))
            {
                screen_pos = GetMousePos();
                mouseClick = true ;
            }
            else
            {
                mouseClick = false;
            }
        }
        else
        {
            screen_pos = GetCharacterPos();
            mouseClick = true;
        }

        lastMousePos = mousePos;
        mousePos = screen_pos;

        matStep1.SetVector("_MousePos", mousePos);
        matStep1.SetVector("_LastMousePos", lastMousePos);
        matStep1.SetFloat("_Init", mouseClick?1:0);

        matStep4.SetVector("_MousePos", mousePos);
        matStep4.SetVector("_LastMousePos", lastMousePos);
        matStep4.SetFloat("_Init", mouseClick ? 1 : 0);

    }

    Vector2 GetMousePos()
    {
        Vector2 screen_pos = Input.mousePosition / new Vector2(Screen.width, Screen.height);
        return screen_pos;
        
    }
    Vector2 GetCharacterPos()
    {
        Vector2 floor_size = new Vector2(11.5f, 18.09f);
        Vector2 screen_pos = new Vector2(bindedGameObject.transform.position.x, bindedGameObject.transform.position.z);
        screen_pos = (screen_pos + floor_size * 0.5f) / floor_size;
        screen_pos = Vector2.Min(screen_pos, floor_size);
        screen_pos = Vector2.Max(screen_pos, Vector2.zero);
        return screen_pos;
    }
}
