Shader "ImageEffect/SobelEdge"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	_Width("Width", Float) = 640 // Screen Size X
		_Height("Height", Float) = 480 // Screen Size Y
		_Threshold("Threshold", Range(0, 1)) = 0.5 // Threshold for edge
	}
		SubShader
	{

		Pass
	{
		CGPROGRAM
#pragma vertex vert_img
#pragma fragment frag

#include "UnityCG.cginc"

		sampler2D _MainTex;
	float _Width;
	float _Height;
	float _Threshold;

	float gray(fixed4 c) {
		return 0.2126 * c.r + 0.7152 * c.g + 0.0722 * c.b;
	}

	fixed4 frag(v2f_img i) : COLOR
	{
		float dx = 1.0 / _Width;
	float dy = 1.0 / _Height;
	float c00 = gray(tex2D(_MainTex, i.uv + float2(-dx, -dy)));
	float c01 = gray(tex2D(_MainTex, i.uv + float2(-dx, 0.0)));
	float c02 = gray(tex2D(_MainTex, i.uv + float2(-dx, dy)));
	float c10 = gray(tex2D(_MainTex, i.uv + float2(0, -dy)));
	float c12 = gray(tex2D(_MainTex, i.uv + float2(0, dy)));
	float c20 = gray(tex2D(_MainTex, i.uv + float2(dx, -dy)));
	float c21 = gray(tex2D(_MainTex, i.uv + float2(dx, 0.0)));
	float c22 = gray(tex2D(_MainTex, i.uv + float2(dx, dy)));

	float sx = -1.0 * c00 + -2.0 * c10 + -1.0 * c20 + 1.0 * c02 + 2.0 * c12 + 1.0 * c22;
	float sy = -1.0 * c00 + -2.0 * c01 + -1.0 * c02 + 1.0 * c20 + 2.0 * c21 + 1.0 * c22;

	float g = sqrt(sx * sx + sy * sy);

	//return g > _Threshold ? fixed4(1, 1, 1, 1) : fixed4(0, 0, 0, 1);

	return g > _Threshold ? fixed4(0, 0, 0, 1) : tex2D(_MainTex, i.uv + float2(-dx, -dy)) ;
	}
		ENDCG
	}
	}
}
