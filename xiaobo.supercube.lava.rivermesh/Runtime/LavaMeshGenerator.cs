using UnityEngine;
using SplineMesh;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;

public class LavaMeshGenerator : MonoBehaviour
{
    public class RiverWrapper
    {
        public Transform go;
        public Spline spline;
        public SplineMeshTiling meshTiling;
        public MeshBender meshBender;
        public Material mat;

        // basic
        public float scaleZ;
        public float baseTiling;

        // growth
        public float lifetime;
        public float age;
        public float lastTriggerTime;
        public Vector2 displayRange;

        // flow
        public float flowmapSpeed;
        public float flowmapStrength;
        public float flowmapOffset;
        public float flowmapOffsetSpeed;
    }
    public class RiverGroup
    {
        public Transform root;
        public List<RiverWrapper> riverList;
        public int currentIndex;
    }

    List<RiverGroup> riverGroupList = new List<RiverGroup>();

    public bool simulateReset;
    void Start()
    {
        InitializeRiverList();

        ResetRiver();
    }

    public void ResetRiver()
    {
        for (int i = 0; i < riverGroupList.Count; i++)
        {
            RiverGroup river_group = riverGroupList[i];
            river_group.riverList.Shuffle();
            river_group.currentIndex = 0;
            for (int k=0; k<river_group.riverList.Count; k++)
            {
                RiverWrapper river = river_group.riverList[k];

                river.go.gameObject.SetActive(k == river_group.currentIndex);

                // basic
                river.baseTiling = Random.Range(2f, 4f);
                river.mat.SetVector("_BaseTiling", new Vector2(1, river.baseTiling));

                river.scaleZ = Random.Range(3f, 5f);
                river.meshTiling.scale = new Vector3(1, 1, river.scaleZ);
                for (int n = 0; n < river.spline.nodes.Count; n++)
                {
                    river.spline.nodes[n].Scale = new Vector2(Random.Range(0.6f, 1.2f), 0);
                }

                // growth
                river.lifetime = Random.Range(60, 120);
                river.age = 0;// Random.Range(-20, 0);

                // flow
                river.flowmapSpeed = Random.Range(0.2f, 0.5f);
                river.flowmapStrength = Random.Range(0.1f, 0.4f);
                river.mat.SetFloat("_FlowSpeed_UpperLayer", river.flowmapSpeed);
                river.mat.SetFloat("_FlowStrength_UpperLayer", river.flowmapStrength);

                river.flowmapOffset = Random.Range(0f, 1000f);
                river.flowmapOffsetSpeed = Random.Range(0.001f, 0.002f);

                UpdateRiver(river);
            }
            
        }
    }

    void Update()
    {
        if(simulateReset)
        {
            ResetRiver();
            simulateReset = false;
        }

        for (int i = 0; i < riverGroupList.Count; i++)
        {
            RiverGroup river_group = riverGroupList[i];


            RiverWrapper river = river_group.riverList[river_group.currentIndex];

            river.age += Time.deltaTime;

            if (river.age > river.lifetime)
            {
                river.age = 0;
                river.go.gameObject.SetActive(false);

                river_group.currentIndex = (river_group.currentIndex+1) % river_group.riverList.Count;
                river_group.riverList[river_group.currentIndex].go.gameObject.SetActive(true);
                continue;
            }

            UpdateRiver(river);
        }
    }

    void UpdateRiver(RiverWrapper river)
    {
        // Control the growth of lava river 
        float percentage = river.age / river.lifetime;
        percentage = percentage < 0 ? 0 : percentage;
        float edge0 = 0.4f;
        float edge1 = 0.9f;
        if (percentage < edge0)
        {
            river.mat.SetVector("_DisplayRangeY", new Vector2(1f - percentage / edge0, 1f));
            river.mat.SetFloat("_Alpha", 1);
        }
        else if (percentage > edge1)
        {
            //river.mat.SetVector("_DisplayRangeY", new Vector2(0f, 1f - (percentage - edge1) / (1 - edge1)));
            river.mat.SetVector("_DisplayRangeY", new Vector2(0f, 1f));
            river.mat.SetFloat("_Alpha", 1f - (percentage - edge1) / (1 - edge1));
        }
        else
        {
            river.mat.SetVector("_DisplayRangeY", new Vector2(0f, 1f));
            river.mat.SetFloat("_Alpha", 1);
        }

        // Flow
        river.flowmapOffset += river.flowmapOffsetSpeed;
        river.mat.SetVector("_FlowMapOffset", new Vector2(0, river.flowmapOffset));


        // shrink tip of river
        /*
        float start_scale, end_scale;
        float cur_length, start_length;
        float shrink_length = 2;
        float shrink_dst = 0.1f;
        if (percentage < edge0)
        {
            start_scale = 1;
            end_scale = shrink_dst;
            cur_length = (percentage / edge0)* river.spline.Length;
            start_length = Mathf.Max(cur_length - shrink_length, 0);
        }
        else if(percentage >= edge0 && percentage < edge1) 
        {
            start_scale = 1;
            end_scale = shrink_dst + (percentage-edge0) / (edge1 - edge0) * (1- shrink_dst);
            cur_length = river.spline.Length;
            start_length = Mathf.Max(cur_length - shrink_length, 0);
        }
        else
        {
            start_scale = 1;
            end_scale = 1;
            cur_length = river.spline.Length;
            start_length = 0;
        }

        start_scale = 1;
        end_scale = 0;
        cur_length = river.spline.Length;
        start_length = 0;
        shrink_length = river.spline.Length;

        // apply scale and roll at each node
        float currentLength = 0;
        foreach (CubicBezierCurve curve in river.spline.GetCurves())
        {
            float startRate = (currentLength - start_length) / shrink_length;
            startRate = Mathf.Clamp(startRate, 0, 1);
            currentLength += curve.Length;
            float endRate = (currentLength - start_length) / shrink_length;
            endRate = Mathf.Clamp(endRate, 0, 1);

            curve.n1.Scale = Vector2.one * (start_scale + (end_scale - start_scale) * startRate);
            curve.n2.Scale = Vector2.one * (start_scale + (end_scale - start_scale) * endRate);

        }
        */
    }


    void InitializeRiverList()
    {
        for (int i = 0; i<transform.childCount;i++)
        {
            RiverGroup river_group = new RiverGroup();
            river_group.root = transform.GetChild(i);
            river_group.riverList = new List<RiverWrapper>();
            riverGroupList.Add(river_group);
            
            for (int k=0; k< river_group.root.childCount; k++)
            {
                RiverWrapper river = new RiverWrapper();
                river.go = river_group.root.GetChild(k);
                river.spline = river.go.GetComponent<Spline>();
                river.meshTiling = river.go.GetComponent<SplineMeshTiling>();
                river.meshBender = river.go.GetComponentInChildren<MeshBender>();
                river.mat = river.go.GetComponentInChildren<MeshRenderer>().material;

                river_group.riverList.Add(river);
            }
            
        }
    }
    
}

static class MyListExtensions
{
    private static System.Random rng = new System.Random();
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
