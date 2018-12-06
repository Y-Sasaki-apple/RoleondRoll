﻿Shader "GameObjectShader"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		TimeStop("TimeStop",Range(0,1)) = 0
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
			}

			Cull Off
			Lighting Off
			ZWrite Off
			ZTest[unity_GUIZTestMode]
			Fog{ Mode Off }
			Blend SrcAlpha OneMinusSrcAlpha

			Pass
			{
				CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag
	#include "UnityCG.cginc"

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					half2 texcoord  : TEXCOORD0;
				};

				fixed TimeStop;
				sampler2D _MainTex;

				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);
					OUT.texcoord = IN.texcoord;
	#ifdef UNITY_HALF_TEXEL_OFFSET
					OUT.vertex.xy += (_ScreenParams.zw - 1.0) * float2(-1,1);
	#endif
					return OUT;
				}

				fixed4 frag(v2f IN) : SV_Target
				{
					fixed4 color = tex2D(_MainTex, IN.texcoord);
					fixed bright = color.r*0.3 + color.g*0.6 + color.b*0.1;
					fixed add;
					if (bright > 0.5)add = -1.0;
					else add = 1.0;
					fixed4 output = fixed4(color.r + TimeStop * add,color.g + TimeStop * add,color.b + TimeStop * add,color.a);
					return output;
				}
				ENDCG
			}
		}

			FallBack "UI/Default"
}