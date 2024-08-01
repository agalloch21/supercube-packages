using UnityEngine;
using SplineMesh;
using System.Collections.Generic;
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
    List<RiverWrapper> riverList = new List<RiverWrapper>();
    void Start()
    {
        InitializeRiverList();
    }


    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            RiverWrapper river = riverList[i];

            river.age += Time.deltaTime;

            if(river.age > river.lifetime)river.age = 0;

            float percentage = river.age / river.lifetime;
            if(percentage < 0.2f)
            {
                river.mat.SetVector("_DisplayRangeY", new Vector2(1f - percentage / 0.2f, 1f));
            }
            else if (percentage > 0.8f)
            {
                river.mat.SetVector("_DisplayRangeY", new Vector2(0f, 1f - (percentage-0.8f) / 0.2f));
            }
            else
            {
                river.mat.SetVector("_DisplayRangeY", new Vector2(0f, 1f));
            }

        }
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

            river.lifetime = Random.Range(30, 50);
            river.age = 0;

            riverList.Add(river);
        }
    }
}
