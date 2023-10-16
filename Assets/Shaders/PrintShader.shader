Shader "Custom/PrintShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,0)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float4 color : COLOR;
            float2 uv_MainTex;
            float3 worldPos;
        };

        float3 center;
        float height;
        float width;

        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c;
            fixed4 alpha0 = fixed4(1, 1, 1, 0);
            fixed4 alpha1 = fixed4(1, 1, 1, 1);
            
            c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

            if (abs(IN.worldPos.x - center.x) < width / 2 && abs(IN.worldPos.z - center.z) < height / 2) {
                o.Albedo = c.rgb;
            }
            else o.Albedo = IN.color.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
