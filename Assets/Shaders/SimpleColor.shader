Shader "Playground/SimpleColor"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        CGPROGRAM
        
        #pragma surface surf Lambert

        struct Input
        {
            float2 uv_main_tex;
        };

        fixed4 _Color;

        void surf(Input input, inout SurfaceOutput output)
        {
            output.Albedo = _Color.rgb;
        }
        
        ENDCG
    }
    FallBack "Diffuse"
}