Shader "Custom/Gradation" {

	Properties{
		_Color("Color", Color) = (1, 1, 1, 1)
		_UpColor("UpColor", Color) = (1,0.35,0.6,1)
		_DownColor("DownColor", Color) = (0.3,0.3,1,1)
		_ExplosionTime("ExplosionTime",float) = 0
		_Rate("Rate",float) = 0.2
	}

		SubShader{
		Tags{ "RenderType" = "Transparent" }
		Cull Off ZWrite On Blend SrcAlpha OneMinusSrcAlpha

		Pass{
		CGPROGRAM

#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		struct v2f {
		float4 pos : SV_POSITION;
		float3 worldPos : TEXCOORD0;
	};

	float4 _Color;
	float4 _UpColor;
	float4 _DownColor;
	float _ExplosionTime;
	float _Rate;

	// vertexシェーダー
	v2f vert(appdata_base v) {
		v2f o;
		float3 n = UnityObjectToWorldNormal(v.normal);
		o.pos = UnityObjectToClipPos(v.vertex) + float4(n * _ExplosionTime, 0);
		o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

		return o;
	}

	// pixelシェーダー
	half4 frag(v2f input) : COLOR
	{
		return lerp(_UpColor, _DownColor, input.worldPos.y * _Rate);
	}
		ENDCG
	}
	}
		FallBack Off
}