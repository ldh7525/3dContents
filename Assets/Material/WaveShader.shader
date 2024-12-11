Shader "Custom/ComplexWaveShader"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0.5, 0.5, 1.0, 1.0) // �⺻ ����
        _WaveSpeed ("Wave Speed", Float) = 1.0                 // ���� �ӵ�
        _Frequency ("Wave Frequency", Float) = 5.0            // ���� ���ļ�
        _Amplitude ("Wave Amplitude", Float) = 0.1            // ���� ũ��
        _GradientFactor ("Gradient Factor", Float) = 2.0      // ���� �׶��̼� ����
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

            // Shader �Ӽ�
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

                // UV ����: Y���� �������� ���� ȿ�� �߰�
                o.uv.y += sin(_Frequency * v.uv.x + _Time.y * _WaveSpeed) * _Amplitude;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // ���� �׶��̼�: Y�� ��ġ�� �������� ��ä�ο� ���� �߰�
                float gradient = abs(sin(i.uv.y * _GradientFactor)); // Gradient ���
                fixed4 gradientColor = lerp(fixed4(0, 1, 0, 1), fixed4(1, 0, 0, 1), gradient); // �ʷ� -> ����

                // Gradient�� BaseColor�� ����
                return lerp(_BaseColor, gradientColor, gradient);
            }
            ENDCG
        }
    }
}
