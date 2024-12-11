Shader "Custom/ComplexWaveShader"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0.5, 0.5, 1.0, 1.0) // 기본 색상
        _WaveSpeed ("Wave Speed", Float) = 1.0                 // 물결 속도
        _Frequency ("Wave Frequency", Float) = 5.0            // 물결 주파수
        _Amplitude ("Wave Amplitude", Float) = 0.1            // 물결 크기
        _GradientFactor ("Gradient Factor", Float) = 2.0      // 색상 그라데이션 조정
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            // Shader 속성
            fixed4 _BaseColor;
            float _WaveSpeed;
            float _Frequency;
            float _Amplitude;
            float _GradientFactor;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                // UV 변형: Y축을 기준으로 물결 효과 추가
                o.uv.y += sin(_Frequency * v.uv.x + _Time.y * _WaveSpeed) * _Amplitude;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 색상 그라데이션: Y축 위치를 기준으로 다채로운 색상 추가
                float gradient = abs(sin(i.uv.y * _GradientFactor)); // Gradient 계산
                fixed4 gradientColor = lerp(fixed4(0, 1, 0, 1), fixed4(1, 0, 0, 1), gradient); // 초록 -> 빨강

                // Gradient와 BaseColor를 조합
                return lerp(_BaseColor, gradientColor, gradient);
            }
            ENDCG
        }
    }
}
