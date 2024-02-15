using Assets.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LightScipt : MonoBehaviour
{
    [SerializeField] Light2D globalLight;
    [SerializeField] Slider slider;
    void Start()
    {
        globalLight.intensity = PlayerData.lightIntensivity;
        slider.value = globalLight.intensity;
    }

    public void SetLightIntensivity()
    {
        globalLight.intensity = slider.value;
        PlayerData.lightIntensivity = slider.value;
    }
}
