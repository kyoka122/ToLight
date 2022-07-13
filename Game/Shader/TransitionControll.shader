Shader "Unlit/transition"
{
	Properties
	{
		
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}

		_Color("Tint", Color) = (1,1,1,1)
		_Alpha ("Time", Range(0, 1)) = 0

	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
			//"RenderPipeline"="UniversalPipeline"//<-
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Fog{ Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha


		Pass
		{
            //Name "ForwardLit"                         // <-

           //Tags { "LightMode"="UniversalForward" }   // <-

			CGPROGRAM
#pragma vertex vert
#pragma fragment frag
//#include "UnityCG.cginc"
            //#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"  // <-
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
                float fogFactor: TEXCOORD1;
				half2 texcoord  : TEXCOORD0;
			};

			//CBUFFER_START(UnityPerMaterial) // <-
            //TEXTURE2D(_MainTex);                // <-
            //SAMPLER(sampler_MainTex);           // <-
			float4 _Color;
			fixed _Alpha;
			sampler2D _MainTex;
			//CBUFFER_END                     // <-

			// 頂点シェーダーの基本
			v2f vert(appdata_t IN)
			{
				v2f OUT;
                //OUT.vertex = TransformObjectToHClip(v.vertex);    // <-
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
                //OUT.fogFactor = ComputeFogFactor(o.vertex.z);     // <-
				OUT.texcoord = IN.texcoord;
				#ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw - 1.0) * float2(-1,1);
				#endif
				return OUT;
			}

			// 通常のフラグメントシェーダー
			fixed4 frag(v2f IN) : SV_Target
			{
				half alpha = tex2D(_MainTex, IN.texcoord).a;
				alpha = saturate(alpha + (_Alpha * 2 - 1));
                //color.rgb = MixFog(col.rgb, i.fogFactor); // <-
				return fixed4(_Color.r, _Color.g, _Color.b, alpha);
			}
			ENDCG
		}
	}

	FallBack "UI/Default"
}