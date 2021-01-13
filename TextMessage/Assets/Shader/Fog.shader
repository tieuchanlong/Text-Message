Shader "Custom/VerticalFogIntersection"
{
	Properties
	{
	   _ColorTop("Color Top", Color) = (1, 0, 0, 1.0)
	   _ColorBotton("Color Bottom", Color) = (1, 1, 0, 0.9)
	   _IntersectionThresholdMax("Intersection Threshold Max", float) = 1
	}
		SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent"  }

		Pass
		{
		   Blend SrcAlpha OneMinusSrcAlpha
		   ZWrite Off
		   CGPROGRAM
		   #pragma vertex vert
		   #pragma fragment frag
		   #pragma multi_compile_fog
		   #include "UnityCG.cginc"

		   struct appdata
		   {
			   float4 vertex : POSITION;
			   float2 texcoord0 : TEXCOORD0;
		   };

		   struct v2f
		   {
			   float4 vertex : SV_POSITION;
			   float2 uv0 : TEXCOORD0;
		   };

		   sampler2D _CameraDepthTexture;
		   float4 _ColorTop, _ColorBottom;
		   float4 _IntersectionColor;
		   float _IntersectionThresholdMax;

		   v2f vert(appdata v)
		   {
			   v2f o = (v2f) 0;
			   o.uv0 = v.texcoord0;
			   o.vertex = UnityObjectToClipPos(v.vertex);
			   return o;
		   }


			half4 frag(v2f i) : SV_TARGET
			{
			   float3 emissive = lerp(_ColorTop.rgb, _ColorBottom.rgb, (1.0 - i.uv0.g));
			   float3 finalColor = emissive;
			   return fixed4(finalColor, 0.5);
			}

			ENDCG
		}
	}
		FallBack "Diffuse"
}