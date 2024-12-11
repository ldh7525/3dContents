using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public static SliderController Instance { get; private set; }    // Singleton
    private Slider slider;
    private Image fillImage;
    public Gradient gradient;

    private void Awake()
    {
        if (Instance == null) //Singleton
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        slider = GetComponent<Slider>();
        // Fill Image ���� �Ҵ�
        fillImage = slider.transform.Find("Fill Area/Fill").GetComponent<Image>();

        // Gradient ��ü ���� �� ����
        gradient = new Gradient();

        // Gradient ���� Ű�� ���� Ű ����
        GradientColorKey[] colorKeys = new GradientColorKey[6];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];

        // ���� Ű ���� (����� ��ġ)
        colorKeys[0].color = new Color(144f / 255f, 238f / 255f, 144f / 255f); // Light Green
        colorKeys[0].time = 0.0f;

        colorKeys[1].color = new Color(173f / 255f, 255f / 255f, 47f / 255f); // Lime Green
        colorKeys[1].time = 0.2f;

        colorKeys[2].color = new Color(255f / 255f, 255f / 255f, 102f / 255f); // Light Yellow
        colorKeys[2].time = 0.4f;

        colorKeys[3].color = new Color(255f / 255f, 165f / 255f, 0f / 255f); // Bright Orange
        colorKeys[3].time = 0.6f;

        colorKeys[4].color = new Color(255f / 255f, 69f / 255f, 0f / 255f); // Orange Red
        colorKeys[4].time = 0.8f;

        colorKeys[5].color = new Color(1.0f, 69f / 255f, 69f / 255f); // Bright Red
        colorKeys[5].time = 1.0f;

        // ���� Ű ���� (�������� ��ġ)
        alphaKeys[0].alpha = 1.0f; // ������ ������
        alphaKeys[0].time = 0.0f;

        alphaKeys[1].alpha = 1.0f; // ������ ������
        alphaKeys[1].time = 1.0f;

        // Gradient�� Ű ����
        gradient.SetKeys(colorKeys, alphaKeys);
    }

    private void Update()
    {
        float v = (float)GameManager.Instance.skillGauge / GameManager.Instance.skillGaugeMax;
        slider.value = Mathf.Clamp(v, slider.minValue, slider.maxValue);

        fillImage.color = gradient.Evaluate(slider.value);
    }
}