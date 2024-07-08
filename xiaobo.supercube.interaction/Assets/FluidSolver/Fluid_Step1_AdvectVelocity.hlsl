#define MAX_MOUSE_COUNT 1


void AdvectVelocity_float(UnityTexture2D tex, UnitySamplerState samp, float2 uv, float vFlowDims, float dissipation, float timestep, out float4 color)
{
    float2 pos = uv;
    float4 v = SAMPLE_TEXTURE2D(tex, samp, pos);
    
    pos += timestep * v.xy / vFlowDims;
    float4 newValue = SAMPLE_TEXTURE2D(tex, samp, pos);
    newValue *= dissipation;
    
    color = newValue;
}



float curMousePosList[MAX_MOUSE_COUNT * 2];
float lastMousePosList[MAX_MOUSE_COUNT * 2];
float curMouseStateList[MAX_MOUSE_COUNT];

float gaussian(float d2, float radius)
{
    return exp(-d2 / radius);
}
void AddVelocity_float(float2 uv, float vFlowDims, float fRadius, float2 MousePos, float2 LastMousePos, float init, out float4 color)
{
    color = 0;
    float4 iterator_color = 0;
    for (int i = 0; i < MAX_MOUSE_COUNT;i++)
    {
        //float2 MousePos = float2(curMousePosList[i * 2 + 0], curMousePosList[i * 2 + 1]);
        //float2 LastMousePos = float2(lastMousePosList[i * 2 + 0], lastMousePosList[i * 2 + 1]);
        //float init = curMouseStateList[i];
        
        
        // MousePos.y = 1-MousePos.y;
	    //LastMousePos.y = 1-LastMousePos.y;
        float3 mouseVector = vFlowDims * float3((MousePos.x - LastMousePos.x), (MousePos.y) - (LastMousePos.y), 1);
        mouseVector = -max(-1., min(1., mouseVector));
        float2 pos = float2(MousePos.x, MousePos.y) - uv;
        float3 result = min(.8f, init * mouseVector.xyz * gaussian(dot(pos, pos), fRadius));
	
        iterator_color = float4(result.xyz * 1, gaussian(dot(pos, pos), fRadius));
    }
    
	
    color += iterator_color;
}

void BoundaryConditions_float(UnityTexture2D tex, UnitySamplerState samp, float2 uv, float vFlowDims, float scale, out float4 color)
{
    float2 tc = uv;
    if (tc.x < 1.0f / vFlowDims)
        tc.x += 1.0 / vFlowDims;
    else if (tc.y < 1.0f / vFlowDims)
        tc.y += 1.0f / vFlowDims;
    else if (tc.x > (vFlowDims - 1.0f) / vFlowDims)
        tc.x -= 1.f / vFlowDims.x;
    else if (tc.y > (vFlowDims - 1.0f) / vFlowDims)
        tc.y -= 1.f / vFlowDims;
    else
        clip(-1.);
	
    color = scale * SAMPLE_TEXTURE2D(tex, samp, tc);    
}


