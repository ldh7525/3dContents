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
        // Fill Image 동적 할당
        fillImage = slider.transform.Find("Fill Area/Fill").GetComponent<Image>();

        // Gradient 객체 생성 및 설정
        gradient = new Gradient();

        // Gradient 색상 키와 알파 키 설정
        GradientColorKey[] colorKeys = new GradientColorKey[6];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];

        // 색상 키 설정 (색상과 위치)
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

        // 알파 키 설정 (불투명도와 위치)
        alphaKeys[0].alpha = 1.0f; // 완전히 불투명
        alphaKeys[0].time = 0.0f;

        alphaKeys[1].alpha = 1.0f; // 완전히 불투명
        alphaKeys[1].time = 1.0f;

        // Gradient에 키 설정
        gradient.SetKeys(colorKeys, alphaKeys);
    }

    private void Update()
    {
        float v = (float)GameManager.Instance.skillGauge / GameManager.Instance.skillGaugeMax;
        slider.value = Mathf.Clamp(v, slider.minValue, slider.maxValue);

        fillImage.color = gradient.Evaluate(slider.value);
    }
}