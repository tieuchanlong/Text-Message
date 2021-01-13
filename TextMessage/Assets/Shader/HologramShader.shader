Shader "Unlit/HologramShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1, 0, 0, 1)
		_Speed("Speed", Range(0.0, 1.0)) = 0.5
		_WaveLength("WaveLength", Range(0, 1000)) = 100
		_Bias("Bias", Range(-1, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "RenderType" = "Transparent" }
        LOD 100
		//ZWrite Off
		Blend SrcAlpha One
		Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float4 objVertex: TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			fixed4 _Color;
			float _Speed, _WaveLength, _Bias;

            v2f vert (appdata v)
            {
                v2f o;
				o.objVertex = v.vertex;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
				col *= _Color * max(0, sin((i.objVertex.y + _Speed * _Time.y) * _WaveLength) + _Bias);
				col *= max(0, sin((i.objVertex.x + _Speed * _Time.y) * _WaveLength) + _Bias);
                return col;
            }
            ENDCG
        }
    }
}
