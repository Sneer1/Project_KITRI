Shader "Custom/UnlitTransparentDiffuse" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Color", Color ) = (1, 1, 1, 1)
	}
	SubShader {
		Tags { "RenderType"="Transparent" }
		LOD 400
		
		CGPROGRAM
		#pragma surface surf Unlit alpha

		half4 LightingUnlit(SurfaceOutput s, half3 lightDir, half atten)
		{
			return half4(s.Albedo, s.Alpha);
		}

		sampler2D _MainTex;
		float4 _Color;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a * _Color.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
