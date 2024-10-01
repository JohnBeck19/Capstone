Shader "Custom/ColorTransparentShader" {
    Properties {
        _Color ("Color", Color) = (1, 1, 1, 1) // Base color
        _Transparency ("Transparency", Range(0, 1)) = 1 // Transparency value
    }
    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            float4 _Color; // The color input
            float _Transparency; // Transparency input

            struct appdata {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 color = _Color; 
                color.a *= _Transparency; // Apply the transparency
                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
