using UnityEngine;



[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class OffAxisCamera : MonoBehaviour
{
    
    [Tooltip("Targed plane has to be XY-Oriented.")]
    [SerializeField] private Transform projectionPlane;

    private Camera eyeCamera;

    void Start()
    {
        UpdateCameraProjectionMatrix();
    }

    void Update()
    {
        if (transform.hasChanged || projectionPlane.hasChanged)
            UpdateCameraProjectionMatrix();
    }



    void UpdateCameraProjectionMatrix()
    {
        if (eyeCamera == null)
            eyeCamera = GetComponent<Camera>();

        if (eyeCamera == null || projectionPlane == null)
            return;


        // Align camera with plane to make sure camera is facing XY-plane
        eyeCamera.transform.rotation = projectionPlane.transform.rotation;

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Code based on Robert Kooima - Generalized Perspective Projection
        // http://160592857366.free.fr/joe/ebooks/ShareData/Generalized%20Perspective%20Projection.pdf
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        // Initiate variables
        GetPlaneCorners(projectionPlane, out Vector3 leftBottom, out Vector3 leftTop, out Vector3 rightBottom, out Vector3 rightTop);

        leftBottom = eyeCamera.transform.InverseTransformPoint(leftBottom);
        leftTop = eyeCamera.transform.InverseTransformPoint(leftTop);
        rightBottom = eyeCamera.transform.InverseTransformPoint(rightBottom);
        rightTop = eyeCamera.transform.InverseTransformPoint(rightTop);

        Vector3 eyePos = eyeCamera.transform.position;
        eyePos = Vector3.zero;

        Vector3 va, vb, vc;
        Vector3 vr, vu, vn;

        float l, r, b, t, d;


        // Compute an orthonormal basis for the screen
        vr = rightBottom - leftBottom;
        vu = leftTop - leftBottom;

        vr.Normalize();
        vu.Normalize();
        vn = Vector3.Cross(vr, vu);
        vn.Normalize();


        // Compute an orthonormal basis for the screen
        va = leftBottom - eyePos;
        vb = rightBottom - eyePos;
        vc = leftTop - eyePos;


        // If it's Orthogonal
        if (eyeCamera.orthographic)
        {
            l = Vector3.Dot(vr, va);
            r = Vector3.Dot(vr, vb);
            b = Vector3.Dot(vu, va);
            t = Vector3.Dot(vu, vc);
            eyeCamera.projectionMatrix = Matrix4x4.Ortho(l, r, b, t, eyeCamera.nearClipPlane, eyeCamera.farClipPlane);
            return;
        }


        // Find the distance from the eye to screen plane.
        d = Vector3.Dot(va, vn);     // Don'd need to multiply -1 because Unity use Left-Hand Axis


        // Find the extent of the perpendicular projection.
        l = Vector3.Dot(vr, va) * eyeCamera.nearClipPlane / d;
        r = Vector3.Dot(vr, vb) * eyeCamera.nearClipPlane / d;
        b = Vector3.Dot(vu, va) * eyeCamera.nearClipPlane / d;
        t = Vector3.Dot(vu, vc) * eyeCamera.nearClipPlane / d;


        // Load the perpendicular projection.
        Matrix4x4 P = Matrix4x4.Frustum(l, r, b, t, eyeCamera.nearClipPlane, eyeCamera.farClipPlane);
        if (eyeCamera.orthographic == true) P = Matrix4x4.Ortho(l, r, b, t, eyeCamera.nearClipPlane, eyeCamera.farClipPlane);


        //Rotate the projection to be non-perpendicular.
        Matrix4x4 m = Matrix4x4.zero;
        m[0, 0] = vr.x;
        m[0, 1] = vr.y;
        m[0, 2] = vr.z;

        m[1, 0] = vu.x;
        m[1, 1] = vu.y;
        m[1, 2] = vu.z;

        m[2, 0] = vn.x;
        m[2, 1] = vn.y;
        m[2, 2] = vn.z;

        m[3, 3] = 1.0f;

        // Set projection matrix
        eyeCamera.projectionMatrix = P * m;
    }

    void GetPlaneCorners(Transform plane, out Vector3 leftBottom, out Vector3 leftTop, out Vector3 rightBottom, out Vector3 rightTop)
    {
        // Get bounds of mesh
        Bounds bounds = new Bounds();
        Mesh mesh = plane.GetComponent<MeshFilter>().sharedMesh;
        if (mesh != null) bounds = mesh.bounds;
        else bounds.SetMinMax(new Vector3(-1, -1, 0), new Vector3(1, 1, 0)); 

        // Get local position based on orientation
        leftBottom = new Vector3(bounds.min.x, bounds.min.y, bounds.center.z);
        leftTop = new Vector3(bounds.min.x, bounds.max.y, bounds.center.z);
        rightBottom = new Vector3(bounds.max.x, bounds.min.y, bounds.center.z);
        rightTop = new Vector3(bounds.max.x, bounds.max.y, bounds.center.z);

        // Transform local position to world position
        leftBottom = plane.transform.TransformPoint(leftBottom);
        leftTop = plane.transform.TransformPoint(leftTop);
        rightBottom = plane.transform.TransformPoint(rightBottom);
        rightTop = plane.transform.TransformPoint(rightTop);
    }

    private void OnDrawGizmos()
    {
        if (eyeCamera == null)
            eyeCamera = GetComponent<Camera>();

        if (eyeCamera == null || projectionPlane == null)
        {
            return;
        }


        GetPlaneCorners(projectionPlane, out Vector3 leftBottom, out Vector3 leftTop, out Vector3 rightBottom, out Vector3 rightTop);

        // Draw border.
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(leftBottom, leftTop);
        Gizmos.DrawLine(leftTop, rightTop);
        Gizmos.DrawLine(rightTop, rightBottom);
        Gizmos.DrawLine(rightBottom, leftBottom);


        // Draw lines from camera to corners.
        Gizmos.color = Color.yellow;
        Vector3 pos = eyeCamera.transform.position;
        Gizmos.DrawLine(pos, leftTop);
        Gizmos.DrawLine(pos, rightTop);
        Gizmos.DrawLine(pos, rightBottom);
        Gizmos.DrawLine(pos, leftBottom);
        Gizmos.color = Color.grey;
    }
}