﻿Shader "Hidden/BloopFadeTransitionEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Lerp ("Lerp",float) = 0
        _Color ("Color",Color) = (0,0,0,0)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            Name "Transition Effect Pass"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _Lerp;
            fixed4 _Color;

            fixed4 frag (v2f i) : SV_Target
            {
                return lerp(tex2D(_MainTex, i.uv),_Color,_Lerp);
            }
            ENDCG
        }
    }
}
