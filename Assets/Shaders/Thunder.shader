Shader "Unlit/Thunder"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RadnomForce("Speed Wawe", Range(0.0,1)) = 0.5
    }
    SubShader
    {
        Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
        
        Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
        
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color: COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _RadnomForce;
            
            float random (float2 uv)
            {
                return frac(sin(dot(uv,float2(12.9898,78.233)))*43758.5453123);
            }
            v2f vert (appdata v)
            {
                v2f o;
                //v.vertex.y += sin((v.vertex.x + _Time.y*_SpeedWawe+random(v.uv)*0.1)*7)/5;
                v.vertex.y += random(v.uv*_Time.y*0.005)*_RadnomForce;
                v.vertex.x += random(v.uv*_Time.y*-0.005*_RadnomForce);
                v.vertex.x -= random(v.uv*_Time.y*-0.007*_RadnomForce);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return i.color;
            }


            
            ENDCG
        }
    }
}
