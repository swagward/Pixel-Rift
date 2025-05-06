Shader "Custom/GhostEdgeOnly"
{
    Properties
    {
        _OutlineColour ("Outline Colour", Color) = (0.4, 0.8, 1.0, 1)
        _OutlineThickness ("Outline Thickness", Float) = 0.05
        _GlowIntensity ("Glow Intensity", Float) = 1.0
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Cull Back
        ZWrite Off
        Blend One One

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float _OutlineThickness;
            fixed4 _OutlineColour;
            float _GlowIntensity;
            //float _Alpha;

            v2f vert (appdata v)
            {
                v2f o;

                float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                worldPos += worldNormal * _OutlineThickness;

                o.pos = UnityWorldToClipPos(worldPos);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(_OutlineColour.rgb * _GlowIntensity, 1);
            }
            ENDCG

            //NO WORK RAHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH
            //dear harvey: love u x
        }
    }

    FallBack "Unlit/Transparent"
}
