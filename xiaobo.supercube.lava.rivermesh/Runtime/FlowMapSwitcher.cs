using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FlowMapSwitcher : MonoBehaviour
{
    [SerializeField]
    VisualEffect vfx;

    [SerializeField]
    List<Texture2D> flowmapList;

    public Vector2 lifetimeRange = new Vector2(3f, 6f);
    float lifetime;
    float age;
    int flowmapIndex;
    void Start()
    {
        lifetime = RandomLifetime();
        age = 0;
        flowmapIndex = 0;
        SwitchFlowmap(flowmapIndex);
    }

    void Update()
    {
        age += Time.deltaTime;
        if(age > lifetime)
        {
            lifetime = RandomLifetime();
            age = 0;
            flowmapIndex = flowmapIndex + 1;
            flowmapIndex = flowmapIndex % flowmapList.Count;
            SwitchFlowmap(flowmapIndex);
        }
    }

    float RandomLifetime()
    {
        return Random.Range(lifetimeRange.x, lifetimeRange.y);
    }
    void SwitchFlowmap(int index)
    {
        vfx.SetTexture("FlowMap", flowmapList[index]);
    }
}
