using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public Slider scaleSlider;
    public Slider speedSlider;
    public Slider waterLevel;
    public Button applyButton;
    public Button exitButton;

    public RectTransform miniMapArrow;

    private void Start()
    {
        // Init UI components
        scaleSlider.minValue = 20;
        scaleSlider.maxValue = 60;
        scaleSlider.value = 40;

        speedSlider.minValue = 5;
        speedSlider.maxValue = 30;
        speedSlider.value = 8;

        waterLevel.maxValue = 7;
        waterLevel.value = 2;

        applyButton.onClick.AddListener(ApplyChanges);
        exitButton.onClick.AddListener(Exit);
    }

    private void ApplyChanges()
    {
        Dynamic.Player.speed = speedSlider.value;
        Dynamic.MapData.scale = scaleSlider.value;
        Dynamic.MapData.waterLevel = waterLevel.value;

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
