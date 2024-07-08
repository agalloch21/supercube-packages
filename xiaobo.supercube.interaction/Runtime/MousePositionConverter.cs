using UnityEngine;

[ExecuteInEditMode]
public class MousePositionConverter : MonoBehaviour
{
    public Camera interactionCamera;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Map position to Render Target
            Vector2 screen_pos = Input.mousePosition / new Vector2(Screen.width, Screen.height) * new Vector2(interactionCamera.pixelWidth, interactionCamera.pixelHeight);

            // Map screen position to world pos
            Vector3 mouse_pos = new Vector3(screen_pos.x, screen_pos.y, -interactionCamera.transform.position.z);
            Vector3 mouse_world_pos = interactionCamera.ScreenToWorldPoint(mouse_pos);

            // Fixed Y to 0 because it's XZ-oriented
            mouse_world_pos.y = 0;
            transform.position = mouse_world_pos;
        }
    }
}
