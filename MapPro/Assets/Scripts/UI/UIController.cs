using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public Slider scaleSlider;
    public Slider biomeScaleSlider;
    public Slider speedSlider;
    public Slider waterLevel;
    public Slider sharpnessSlider;
    public Button applyButton;
    public Button exitButton;

    public RectTransform miniMapArrow;

    private void Start()
    {
        // Init UI values
        scaleSlider.minValue = 20;
        scaleSlider.maxValue = 150;
        scaleSlider.value = 100;
        
        biomeScaleSlider.minValue = 10;
        biomeScaleSlider.maxValue = 100;
        biomeScaleSlider.value = 50;

        speedSlider.minValue = 10;
        speedSlider.maxValue = 50;
        speedSlider.value = 20;

        waterLevel.maxValue = 6;
        waterLevel.value = 2;
        
        sharpnessSlider.minValue = 5;
        sharpnessSlider.maxValue = 15;
        sharpnessSlider.value = 10;

        applyButton.onClick.AddListener(ApplyChanges);
        exitButton.onClick.AddListener(Exit);
        ApplyChanges();
    }

    private void ApplyChanges()
    {
        // Sets functional values to equal ui values
        Dynamic.Player.speed = speedSlider.value;
        Dynamic.MapData.scale = scaleSlider.value;
        Dynamic.MapData.biomeScale = biomeScaleSlider.value;
        Dynamic.MapData.waterLevel = waterLevel.value;
        Dynamic.MapData.sharpness = sharpnessSlider.value;

        EventManager.instance.UpdateMap();
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void Update()
    {
        miniMapArrow.eulerAngles = new Vector3(0, 0, Dynamic.Player.playerTransform.eulerAngles.z);
    }
}
