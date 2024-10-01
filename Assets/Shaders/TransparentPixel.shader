Shader "Custom/PixelatedTransparentShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}   // Main texture
        _PixelSize ("Pixel Size", Float) = 0.1  // Pixelation amount
        _Transparency ("Transparency", Range(0, 1)) = 1 // Transparency control
    }
    SubShader {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 200

        Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _PixelSize;
            float _Transparency;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            // Vertex shader
            v2f vert (appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Fragment shader
            fixed4 frag (v2f i) : SV_Target {
                // Pixelation effect: snap the UV coordinates to a grid
                float2 uv = floor(i.uv / _PixelSize) * _PixelSize;
                
                // Sample the texture with pixelated UVs
                fixed4 texColor = tex2D(_MainTex, uv);
                
                // Apply transparency by multiplying alpha with the _Transparency property
                texColor.a *= _Transparency;

                return texColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
