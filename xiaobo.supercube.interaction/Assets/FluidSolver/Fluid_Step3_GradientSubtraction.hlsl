


void GradientSubtraction_float(UnityTexture2D VelocityTex, UnityTexture2D PressureTex, UnitySamplerState samp, float2 uv, float vFlowDims, float GradMul, out float4 color)
{
   	//float4 c = texture2d.Sample(linearSampler,input.uv);
    float pL = SAMPLE_TEXTURE2D(PressureTex, samp, float2(uv.x - 1. / vFlowDims, uv.y)).x;
    float pR = SAMPLE_TEXTURE2D(PressureTex, samp, float2(uv.x + 1. / vFlowDims, uv.y)).x;
    float pB = SAMPLE_TEXTURE2D(PressureTex, samp, float2(uv.x, uv.y - 1. / vFlowDims)).x;
    float pT = SAMPLE_TEXTURE2D(PressureTex, samp, float2(uv.x, uv.y + 1. / vFlowDims)).x;

    float2 grad = float2(pR - pL, pT - pB) * GradMul;

    float4 v = SAMPLE_TEXTURE2D(VelocityTex, samp, uv.xy);
    v.xy -= grad;
	
    color = v;
}

