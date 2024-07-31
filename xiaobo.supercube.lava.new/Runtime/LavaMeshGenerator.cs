using UnityEngine;
using SplineMesh;
public class LavaMeshGenerator : MonoBehaviour
{
    public class RiverWrapper
    {
        public Transform go;
        public SplineMeshTiling meshTiling;
        public Material mat;

        public float lifetime;
        public float age;
        public float lastTriggerTime;
        public Vector2 displayRange;

        public float flowmapSpeed;
        public float flowmapStrength;

        public float scaleZ;
    }

    void Start()
    {
        //InitializeRiverList();
    }

    
    void Update()
    {
        
    }



    void InitializeRiverList()
    {
        for (int i = 0; i<transform.childCount;i++)
        {
            RiverWrapper river = new RiverWrapper();
            river.go = transform.GetChild(i);
            river.meshTiling = river.go.GetComponent<SplineMeshTiling>();
            river.mat = river.go.GetComponentInChildren<MeshRenderer>().material;

            river.flowmapSpeed = Random.Range(0.2f, 0.5f);
            river.flowmapStrength = Random.Range(0.1f, 0.4f);
            river.mat.SetFloat("_FlowSpeed_UpperLayer", river.flowmapSpeed);
            river.mat.SetFloat("FlowStrength_UpperLayer", river.flowmapStrength);
        }
    }
}
