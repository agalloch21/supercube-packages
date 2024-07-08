


void Divergence_float(UnityTexture2D tex, UnitySamplerState samp, float2 uv, float vFlowDims, out float4 color)
{
    float4 vL = SAMPLE_TEXTURE2D(tex, samp, float2(uv.x - 1. / vFlowDims, uv.y));
    float4 vR = SAMPLE_TEXTURE2D(tex, samp, float2(uv.x + 1. / vFlowDims, uv.y));
    float4 vB = SAMPLE_TEXTURE2D(tex, samp, float2(uv.x, uv.y - 1. / vFlowDims));
    float4 vT = SAMPLE_TEXTURE2D(tex, samp, float2(uv.x, uv.y + 1. / vFlowDims));
    color = 0.5 * ((vR.x - vL.x) + (vT.y - vB.y));
}


void Pressure_float(UnityTexture2D PTex, UnitySamplerState samp, float4 DivTexColor, float2 uv, float vFlowDims, out float4 color)
{
    //float4 c = texture2d.Sample(linearSampler,input.uv);
    float bCenter = DivTexColor.x;

    float xL = SAMPLE_TEXTURE2D(PTex, samp, float2(uv.x - 1. / vFlowDims, uv.y)).x;
    float xR = SAMPLE_TEXTURE2D(PTex, samp, float2(uv.x + 1. / vFlowDims, uv.y)).x;
    float xB = SAMPLE_TEXTURE2D(PTex, samp, float2(uv.x, uv.y - 1. / vFlowDims)).x;
    float xT = SAMPLE_TEXTURE2D(PTex, samp, float2(uv.x, uv.y + 1. / vFlowDims)).x;
  
    float result = (xL + xR + xB + xT - bCenter) * 0.25;
    color = result;
}