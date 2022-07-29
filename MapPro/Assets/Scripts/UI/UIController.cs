using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public Slider scaleSlider;
    public Slider waterLevel;
    public Slider sandLevel;
    public Slider grassLevel;
    public Button applyButton;
    public Button exitButton;

    public RectTransform miniMapArrow;

    private void Start()
    {
        // Init UI components
        scaleSlider.minValue = 20;
        scaleSlider.maxValue = 60;
        scaleSlider.value = 40;

        waterLevel.maxValue = 5;
        waterLevel.value = 2;
        
        sandLevel.maxValue = 7;
        sandLevel.value = 3;
        
        grassLevel.maxValue = 10;
        grassLevel.value = 8;
        
        applyButton.onClick.AddListener(ApplyChanges);
        exitButton.onClick.AddListener(Exit);
    }

    private void ApplyChanges()
    {
        Dynamic.MapData.scale = scaleSlider.value;
        Dynamic.MapData.layers = new List<float>
            { waterLevel.value / 2, waterLevel.value, sandLevel.value, grassLevel.value };
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
