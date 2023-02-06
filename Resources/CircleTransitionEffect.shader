Shader "Hidden/BloopCircleTransitionEffect"
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
            
            float diag;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _Color;
                diag = length(_ScreenParams.xy);
                //Wipe
                //Normalize to width and height to account for aspect ratio. Move UV's by .5 to center it.
                //Use diagonal/2 for the radius to work on regardless of aspect ratio, 0->1. Because math.
                if(length(float2((_ScreenParams.x*(i.uv.x-0.5)),(_ScreenParams.y*(i.uv.y-0.5)))) < ((1-_Lerp)*diag/2))
                {
                    col = tex2D(_MainTex, i.uv);
                }
                
                return col;
            }
            ENDCG
        }
    }
}
