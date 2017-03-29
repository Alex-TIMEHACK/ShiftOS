Texture2D ShaderTexture : register(t0);
SamplerState Sampler : register(s0);

cbuffer PerObject: register(b0)
{
	float4x4 WorldViewProj;
};

struct VertexShaderInput
{
	float4 Position : SV_Position;
	float4 Color : COLOR;
	float2 TextureUV : TEXCOORD0;
};

struct VertexShaderOutput
{
	float4 Position : SV_Position;
	float4 Color : COLOR;
	float2 TextureUV : TEXCOORD0;
};

VertexShaderOutput vertex_main(VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;

	output.Position = mul(input.Position, WorldViewProj);
	output.TextureUV = input.TextureUV;

	return output;
}

float4 pixel_main(VertexShaderOutput input) : SV_Target
{
	return ShaderTexture.Sample(Sampler, input.TextureUV);
}