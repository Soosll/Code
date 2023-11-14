// Toony Colors Pro+Mobile 2
// (c) 2014-2021 Jean Moreno

Shader "Toony Colors Pro 2/User/My TCP2 Shader"
{
	Properties
	{
		[Enum(Front, 2, Back, 1, Both, 0)] _Cull ("Render Face", Float) = 2.0
		[TCP2ToggleNoKeyword] _ZWrite ("Depth Write", Float) = 1.0
		[HideInInspector] _RenderingMode ("rendering mode", Float) = 0.0
		[HideInInspector] _SrcBlend ("blending source", Float) = 1.0
		[HideInInspector] _DstBlend ("blending destination", Float) = 0.0
		[TCP2Separator]

		[TCP2HeaderHelp(Base)]
		_Color ("Color", Color) = (1,1,1,1)
		[TCP2ColorNoAlpha] _HColor ("Highlight Color", Color) = (0.75,0.75,0.75,1)
		[TCP2ColorNoAlpha] _SColor ("Shadow Color", Color) = (0.2,0.2,0.2,1)
		_MainTex ("Albedo", 2D) = "white" {}
		[TCP2Separator]

		[TCP2Header(Ramp Shading)]
		_RampThreshold ("Threshold", Range(0.01,1)) = 0.5
		_RampSmoothing ("Smoothing", Range(0.001,1)) = 0.5
		_LightWrapFactor ("Light Wrap Factor", Range(0,2)) = 0.5
		[TCP2Separator]
		
		[TCP2HeaderHelp(Specular)]
		[TCP2ColorNoAlpha] _SpecularColor ("Specular Color", Color) = (0.5,0.5,0.5,1)
		_SpecularShadowAttenuation ("Specular Shadow Attenuation", Float) = 0.25
		_SpecularSmoothness ("Smoothness", Float) = 0.2
		_AnisotropicSpread ("Anisotropic Spread", Range(0,2)) = 1
		[TCP2Separator]
		
		[TCP2HeaderHelp(Rim Lighting)]
		[TCP2ColorNoAlpha] _RimColor ("Rim Color", Color) = (0.8,0.8,0.8,0.5)
		_RimMinVert ("Rim Min", Range(0,2)) = 0.5
		_RimMaxVert ("Rim Max", Range(0,2)) = 1
		//Rim Direction
		_RimDirVert ("Rim Direction", Vector) = (0,0,1,1)
		[TCP2Separator]

		[TCP2HeaderHelp(Reflections)]
		[TCP2ColorNoAlpha] _ReflectionColor ("Color", Color) = (1,1,1,1)
		_ReflectionSmoothness ("Smoothness", Range(0,1)) = 0.5
		_FresnelMin ("Fresnel Min", Range(0,2)) = 0
		_FresnelMax ("Fresnel Max", Range(0,2)) = 1.5
		[TCP2Separator]
		[TCP2HeaderHelp(Ambient Lighting)]
		_TCP2_AMBIENT_RIGHT ("+X (Right)", Color) = (0,0,0,1)
		_TCP2_AMBIENT_LEFT ("-X (Left)", Color) = (0,0,0,1)
		_TCP2_AMBIENT_TOP ("+Y (Top)", Color) = (0,0,0,1)
		_TCP2_AMBIENT_BOTTOM ("-Y (Bottom)", Color) = (0,0,0,1)
		_TCP2_AMBIENT_FRONT ("+Z (Front)", Color) = (0,0,0,1)
		_TCP2_AMBIENT_BACK ("-Z (Back)", Color) = (0,0,0,1)
		[TCP2Separator]
		
		[TCP2ColorNoAlpha] _DiffuseTint ("Diffuse Tint", Color) = (1,0.5,0,1)
		[TCP2Separator]
		
		[TCP2HeaderHelp(Vertex Waves Animation)]
		_WavesSpeed ("Speed", Float) = 2
		_WavesHeight ("Height", Float) = 0.1
		_WavesFrequency ("Frequency", Range(0,10)) = 1
		
		[TCP2HeaderHelp(Depth Based Effects)]
		[PowerSlider(5.0)] _DepthAlphaDistance ("Depth Alpha Distance", Range(0.01,10)) = 0.5
		_DepthAlphaMin ("Depth Alpha Min", Range(0,1)) = 0.5
		
		//Avoid compile error if the properties are ending with a drawer
		[HideInInspector] __dummy__ ("unused", Float) = 0
	}

	SubShader
	{
		Tags
		{
			"RenderType"="Opaque"
		}
		
		CGINCLUDE

		#include "UnityCG.cginc"
		#include "UnityLightingCommon.cginc"	// needed for LightColor

		// Shader Properties
		sampler2D _MainTex;
		
		// Shader Properties
		float _WavesFrequency;
		float _WavesHeight;
		float _WavesSpeed;
		float4 _RimDirVert;
		float _RimMinVert;
		float _RimMaxVert;
		float4 _MainTex_ST;
		fixed4 _Color;
		float _DepthAlphaDistance;
		float _DepthAlphaMin;
		float _LightWrapFactor;
		float _RampThreshold;
		float _RampSmoothing;
		fixed4 _SColor;
		fixed4 _HColor;
		fixed4 _DiffuseTint;
		float _AnisotropicSpread;
		float _SpecularSmoothness;
		float _SpecularShadowAttenuation;
		fixed4 _SpecularColor;
		fixed4 _RimColor;
		float _FresnelMin;
		float _FresnelMax;
		float _ReflectionSmoothness;
		
		fixed4 _TCP2_AMBIENT_RIGHT;
		fixed4 _TCP2_AMBIENT_LEFT;
		fixed4 _TCP2_AMBIENT_TOP;
		fixed4 _TCP2_AMBIENT_BOTTOM;
		fixed4 _TCP2_AMBIENT_FRONT;
		fixed4 _TCP2_AMBIENT_BACK;
		UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);

		half3 DirAmbient (half3 normal)
		{
			fixed3 retColor =
				saturate( normal.x * _TCP2_AMBIENT_RIGHT) +
				saturate(-normal.x * _TCP2_AMBIENT_LEFT) +
				saturate( normal.y * _TCP2_AMBIENT_TOP) +
				saturate(-normal.y * _TCP2_AMBIENT_BOTTOM) +
				saturate( normal.z * _TCP2_AMBIENT_FRONT) +
				saturate(-normal.z * _TCP2_AMBIENT_BACK);
			return retColor * 2.0;
		}

		ENDCG

		//Depth pre-pass
		Pass
		{
			Name "Depth Prepass"
			Tags
			{
				"LightMode"="ForwardBase"
			}
			ColorMask 0
			ZWrite On

			CGPROGRAM
			#pragma vertex vertex_depthprepass
			#pragma fragment fragment_depthprepass
			#pragma target 3.0

			struct appdata_sil
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f_depthprepass
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_OUTPUT_STEREO
				float4 screenPosition : TEXCOORD0;
				float4 pack1 : TEXCOORD1; /* pack1.xyz = worldNormal  pack1.w = rim */
			};

			v2f_depthprepass vertex_depthprepass (appdata_sil v)
			{
				v2f_depthprepass output;
				UNITY_INITIALIZE_OUTPUT(v2f_depthprepass, output);
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

				float3 worldNormalUv = mul(unity_ObjectToWorld, float4(v.normal, 1.0)).xyz;
				// Shader Properties Sampling
				float __wavesFrequency = ( _WavesFrequency );
				float __wavesHeight = ( _WavesHeight );
				float __wavesSpeed = ( _WavesSpeed );

				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				
				// Vertex water waves
				float _waveFrequency = __wavesFrequency;
				float _waveHeight = __wavesHeight;
				float3 _vertexWavePos = worldPos.xyz * _waveFrequency;
				float _phase = _Time.y * __wavesSpeed;
				float waveFactorX = sin(_vertexWavePos.x + _phase) * _waveHeight;
				float waveFactorZ = sin(_vertexWavePos.z + _phase) * _waveHeight;
				v.vertex.xyz += v.normal.xyz * (waveFactorX + waveFactorZ);
				float xn = -_waveHeight * cos(_vertexWavePos.x + _phase);
				float zn = -_waveHeight * cos(_vertexWavePos.z + _phase);
				v.normal = normalize(float3(xn, 1, zn));
				output.pack1.xyz = worldNormalUv;
				output.vertex = UnityObjectToClipPos(v.vertex);
				float4 clipPos = output.vertex;

				//Screen Position
				float4 screenPos = ComputeScreenPos(clipPos);
				output.screenPosition = screenPos;
				COMPUTE_EYEDEPTH(output.screenPosition.z);

				return output;
			}

			half4 fragment_depthprepass (v2f_depthprepass input) : SV_Target
			{

				return 0;
			}
			ENDCG
		}
		// Main Surface Shader
		Blend [_SrcBlend] [_DstBlend]
		Cull [_Cull]
		ZWrite [_ZWrite]

		CGPROGRAM

		#pragma surface surf ToonyColorsCustom vertex:vertex_surface exclude_path:deferred exclude_path:prepass keepalpha addshadow fullforwardshadows nolightmap nofog nolppv keepalpha
		#pragma target 3.0

		//================================================================
		// SHADER KEYWORDS

		#pragma shader_feature _ _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON

		//================================================================
		// STRUCTS

		//Vertex input
		struct appdata_tcp2
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float4 texcoord0 : TEXCOORD0;
			float4 texcoord1 : TEXCOORD1;
			float4 texcoord2 : TEXCOORD2;
			half4 tangent : TANGENT;
			UNITY_VERTEX_INPUT_INSTANCE_ID
		};

		struct Input
		{
			half3 viewDir;
			half3 tangent;
			half3 worldNormal; INTERNAL_DATA
			float4 screenPosition;
			half rim;
			float2 texcoord0;
		};

		//================================================================
		// VERTEX FUNCTION

		void vertex_surface(inout appdata_tcp2 v, out Input output)
		{
			UNITY_INITIALIZE_OUTPUT(Input, output);

			// Texture Coordinates
			output.texcoord0.xy = v.texcoord0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
			// Shader Properties Sampling
			float __wavesFrequency = ( _WavesFrequency );
			float __wavesHeight = ( _WavesHeight );
			float __wavesSpeed = ( _WavesSpeed );
			float3 __rimDirVert = ( _RimDirVert.xyz );
			float __rimMinVert = ( _RimMinVert );
			float __rimMaxVert = ( _RimMaxVert );

			float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
			
			// Vertex water waves
			float _waveFrequency = __wavesFrequency;
			float _waveHeight = __wavesHeight;
			float3 _vertexWavePos = worldPos.xyz * _waveFrequency;
			float _phase = _Time.y * __wavesSpeed;
			float waveFactorX = sin(_vertexWavePos.x + _phase) * _waveHeight;
			float waveFactorZ = sin(_vertexWavePos.z + _phase) * _waveHeight;
			v.vertex.xyz += v.normal.xyz * (waveFactorX + waveFactorZ);
			float xn = -_waveHeight * cos(_vertexWavePos.x + _phase);
			float zn = -_waveHeight * cos(_vertexWavePos.z + _phase);
			v.normal = normalize(float3(xn, 1, zn));
			float4 clipPos = UnityObjectToClipPos(v.vertex);

			//Screen Position
			float4 screenPos = ComputeScreenPos(clipPos);
			output.screenPosition = screenPos;
			COMPUTE_EYEDEPTH(output.screenPosition.z);
			half3 viewDir = normalize(UnityWorldSpaceViewDir(worldPos));
			half3 worldNormal = UnityObjectToWorldNormal(v.normal);

			output.tangent = mul(unity_ObjectToWorld, float4(v.tangent.xyz, 0)).xyz;
			half3 rViewDir = viewDir;
			half3 rimDir = __rimDirVert;
			rViewDir = normalize(UNITY_MATRIX_V[0].xyz * rimDir.x + UNITY_MATRIX_V[1].xyz * rimDir.y + UNITY_MATRIX_V[2].xyz * rimDir.z);
			half rim = 1.0f - saturate(dot(rViewDir, v.normal.xyz));
			rim = smoothstep(__rimMinVert, __rimMaxVert, rim);
			output.rim = rim;

		}

		//================================================================

		//Custom SurfaceOutput
		struct SurfaceOutputCustom
		{
			half atten;
			half3 Albedo;
			half3 Normal;
			half3 worldNormal;
			half3 Emission;
			half Specular;
			half Gloss;
			half Alpha;
			half ndv;
			half ndvRaw;

			Input input;
			
			// Shader Properties
			float __lightWrapFactor;
			float __rampThreshold;
			float __rampSmoothing;
			float3 __shadowColor;
			float3 __highlightColor;
			float3 __diffuseTint;
			float __occlusion;
			float __ambientIntensity;
			float __anisotropicSpread;
			float __specularSmoothness;
			float __specularShadowAttenuation;
			float3 __specularColor;
			float3 __rimColor;
			float __rimStrength;
			float __fresnelMin;
			float __fresnelMax;
			float __reflectionSmoothness;
		};

		//================================================================
		// SURFACE FUNCTION

		void surf(Input input, inout SurfaceOutputCustom output)
		{
			
			// Shader Properties Sampling
			float4 __albedo = ( tex2D(_MainTex, input.texcoord0.xy).rgba );
			float4 __mainColor = ( _Color.rgba );
			float __alpha = ( __albedo.a * __mainColor.a );
			float __depthAlphaDistance = ( _DepthAlphaDistance );
			float __depthAlphaMin = ( _DepthAlphaMin );
			output.__lightWrapFactor = ( _LightWrapFactor );
			output.__rampThreshold = ( _RampThreshold );
			output.__rampSmoothing = ( _RampSmoothing );
			output.__shadowColor = ( _SColor.rgb );
			output.__highlightColor = ( _HColor.rgb );
			output.__diffuseTint = ( _DiffuseTint.rgb );
			output.__occlusion = ( __albedo.a );
			output.__ambientIntensity = ( 1.0 );
			output.__anisotropicSpread = ( _AnisotropicSpread );
			output.__specularSmoothness = ( _SpecularSmoothness );
			output.__specularShadowAttenuation = ( _SpecularShadowAttenuation );
			output.__specularColor = ( _SpecularColor.rgb );
			output.__rimColor = ( _RimColor.rgb );
			output.__rimStrength = ( 1.0 );
			output.__fresnelMin = ( _FresnelMin );
			output.__fresnelMax = ( _FresnelMax );
			output.__reflectionSmoothness = ( _ReflectionSmoothness );

			output.input = input;

			half3 worldNormal = WorldNormalVector(input, output.Normal);
			output.worldNormal = worldNormal;

			half ndv = abs(dot(input.viewDir, normalize(output.Normal.xyz)));
			half ndvRaw = ndv;
			output.ndv = ndv;
			output.ndvRaw = ndvRaw;

			// Sample depth texture and calculate difference with local depth
			float sceneDepth = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(input.screenPosition));
			if (unity_OrthoParams.w > 0.0)
			{
				// Orthographic camera
				#if defined(UNITY_REVERSED_Z)
					sceneDepth = 1.0 - sceneDepth;
				#endif
				sceneDepth = (sceneDepth * _ProjectionParams.z) + _ProjectionParams.y;
			}
			else
			{
				// Perspective camera
				sceneDepth = LinearEyeDepth(sceneDepth);
			}
			
			float localDepth = input.screenPosition.z;
			float depthDiff = abs(sceneDepth - localDepth);

			output.Albedo = __albedo.rgb;
			output.Alpha = __alpha;
			
			output.Albedo *= __mainColor.rgb;
			
			// Depth-based output.Alpha
			output.Alpha *= saturate((__depthAlphaDistance * depthDiff) + __depthAlphaMin);

		}

		//================================================================
		// LIGHTING FUNCTION

		inline half4 LightingToonyColorsCustom(inout SurfaceOutputCustom surface, half3 viewDir, UnityGI gi)
		{
			half ndv = surface.ndv;
			half3 lightDir = gi.light.dir;
			#if defined(UNITY_PASS_FORWARDBASE)
				half3 lightColor = _LightColor0.rgb;
				half atten = surface.atten;
			#else
				//extract attenuation from point/spot lights
				half3 lightColor = _LightColor0.rgb;
				half atten = max(gi.light.color.r, max(gi.light.color.g, gi.light.color.b)) / max(_LightColor0.r, max(_LightColor0.g, _LightColor0.b));
			#endif

			half3 normal = normalize(surface.Normal);
			half ndl = dot(normal, lightDir);
			half3 ramp;
			#if defined(UNITY_PASS_FORWARDBASE)
			
			// Wrapped Lighting
			half lightWrap = surface.__lightWrapFactor;
			ndl = (ndl + lightWrap) / (1 + lightWrap);
			#endif
			
			#define		RAMP_THRESHOLD	surface.__rampThreshold
			#define		RAMP_SMOOTH		surface.__rampSmoothing
			ndl = saturate(ndl);
			ramp = smoothstep(RAMP_THRESHOLD - RAMP_SMOOTH*0.5, RAMP_THRESHOLD + RAMP_SMOOTH*0.5, ndl);
			half3 rampGrayscale = ramp;

			//Apply attenuation (shadowmaps & point/spot lights attenuation)
			ramp *= atten;

			//Highlight/Shadow Colors
			surface.Albedo = lerp(surface.__shadowColor, surface.Albedo, ramp);
			ramp = lerp(half3(1,1,1), surface.__highlightColor, ramp);

			// Diffuse Tint
			half3 diffuseTint = saturate(surface.__diffuseTint + ndl);
			ramp *= diffuseTint;
			
			//Output color
			half4 color;
			color.rgb = surface.Albedo * lightColor.rgb * ramp;
			color.a = surface.Alpha;

			// Apply indirect lighting (ambient)
			half occlusion = surface.__occlusion;
			#ifdef UNITY_LIGHT_FUNCTION_APPLY_INDIRECT
				half3 ambient = gi.indirect.diffuse;
				
				//Directional Ambient
				half3 viewNormal = mul(UNITY_MATRIX_V, half4(normal, 0)).xyz;
				ambient.rgb += DirAmbient(viewNormal);
				ambient *= surface.Albedo * occlusion * surface.__ambientIntensity;

				color.rgb += ambient;
			#endif

			// Premultiply blending
			#if defined(_ALPHAPREMULTIPLY_ON)
				color.rgb *= color.a;
			#endif

			//Anisotropic Specular
			half3 h = normalize(lightDir + viewDir);
			float ndh = max(0, dot (normal, h));
			half3 binorm = cross(normal, surface.input.tangent);
			float aX = dot(h, surface.input.tangent) / surface.__anisotropicSpread;
			float aY = dot(h, binorm) / surface.__specularSmoothness;
			float specAniso = sqrt(max(0.0, ndl / surface.ndvRaw)) * exp(-2.0 * (aX * aX + aY * aY) / (1.0 + ndh));
			float spec = specAniso;
			spec = saturate(spec);
			spec *= saturate(atten * ndl + surface.__specularShadowAttenuation);
			
			//Apply specular
			color.rgb += spec * lightColor.rgb * surface.__specularColor;
			// Rim Lighting
			#if !defined(UNITY_PASS_FORWARDADD)
			half rim = surface.input.rim;
			rim = ( rim );
			half3 rimColor = surface.__rimColor;
			half rimStrength = surface.__rimStrength;
			color.rgb += rim * rimColor * rimStrength;
			#endif
			// ForwardBase pass only
			#if !defined(UNITY_PASS_FORWARDADD)

					// Reflection probes/skybox
					half3 reflections = gi.indirect.specular * occlusion;
					half fresnelMin = surface.__fresnelMin;
					half fresnelMax = surface.__fresnelMax;
					half fresnelTerm = smoothstep(fresnelMin, fresnelMax, 1 - surface.ndvRaw);
					reflections *= fresnelTerm;
					color.rgb += reflections;

			#endif

			// Apply alpha to Forward Add passes
			#if defined(_ALPHABLEND_ON) && defined(UNITY_PASS_FORWARDADD)
				color.rgb *= color.a;
			#endif

			return color;
		}

		void LightingToonyColorsCustom_GI(inout SurfaceOutputCustom surface, UnityGIInput data, inout UnityGI gi)
		{
			half3 normal = surface.Normal;

			//GI with reflection probes support
			half smoothness = surface.__reflectionSmoothness;
			Unity_GlossyEnvironmentData g = UnityGlossyEnvironmentSetup(smoothness, data.worldViewDir, normal, half3(0,0,0));	// last parameter is actually unused
			gi = UnityGlobalIllumination(data, 1.0, normal, g); // occlusion is applied in the lighting function, if necessary

			surface.atten = data.atten; // transfer attenuation to lighting function
			gi.light.color = _LightColor0.rgb; // remove attenuation

		}

		ENDCG

	}

	CustomEditor "ToonyColorsPro.ShaderGenerator.MaterialInspector_SG2"
}

/* TCP_DATA u config(unity:"2020.3.18f1";ver:"2.6.4";tmplt:"SG2_Template_Default";features:list["UNITY_5_4","UNITY_5_5","UNITY_5_6","UNITY_2017_1","UNITY_2018_1","UNITY_2018_2","UNITY_2018_3","UNITY_2019_1","UNITY_2019_2","UNITY_2019_3","VERTEX_SIN_WAVES","VERTEX_SIN_NORMALS","VSW_WORLDPOS","AUTO_TRANSPARENT_BLENDING","SPECULAR_NO_ATTEN","SPECULAR","SPECULAR_ANISOTROPIC","RIM","RIM_DIR","RIM_DIR_PERSP_CORRECTION","DIFFUSE_TINT","SUBSURFACE_AMB_COLOR","SS_SCREEN_INFLUENCE","RIM_VERTEX","SS_MULTIPLICATIVE","SS_NO_LIGHTCOLOR","DEPTH_BUFFER_ALPHA","DEPTH_PREPASS","SHADOW_COLOR_LERP","WRAPPED_LIGHTING_CUSTOM","WRAPPED_LIGHTING_MAIN_LIGHT","VSW_FOLLOWNORM","REFLECTION_FRESNEL","GLOSSY_REFLECTIONS","DIRAMBIENT","DIRAMBIENT_VIEW","OCCLUSION"];flags:list["addshadow","fullforwardshadows"];flags_extra:dict[];keywords:dict[RENDER_TYPE="Opaque",RampTextureDrawer="[TCP2Gradient]",RampTextureLabel="Ramp Texture",SHADER_TARGET="3.0",RIM_LABEL="Rim Lighting"];shaderProperties:list[];customTextures:list[];codeInjection:codeInjection(injectedFiles:list[];mark:False)) */
/* TCP_HASH 8a7a2a0561dda49ec7201484748c6e91 */
