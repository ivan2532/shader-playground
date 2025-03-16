Shader "Playground/SimpleColorWithEmission"
{
    Properties
    {
        _Albedo ("Albedo", Color) = (1,1,1,1)
        _Emission ("Emission", Color) = (1,1,1,1)
    }
    SubShader
    {
        CGPROGRAM
        
        #pragma surface surf Lambert

        struct Input
        {
            float2 uv_main_tex;
        };

        fixed4 _Albedo;
        fixed4 _Emission;

        void surf(Input input, inout SurfaceOutput output)
        {
            output.Albedo = _Albedo.rgb;
            output.Emission = _Emission.rgb;
        }
        
        ENDCG
    }
    FallBack "Diffuse"
}