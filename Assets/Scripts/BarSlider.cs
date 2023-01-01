using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BarSlider : MonoBehaviour
{
    public Gradient gradient;
    public Image fill;

    protected Player player;
    protected Slider slider;
        
    void Start()
    {
        slider = GetComponent<Slider>();
        player = GameObject.Find("Player").GetComponent<Player>();
        HUDInitialize();
    }

    
    // Read & set max value for BarSlide
    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
        slider.value = value;

        fill.color = gradient.Evaluate(1f);
    }

    // Set value to BarSlide
    public void SetValue(float value)
    {
        slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // Update MainHUD
    public abstract void HUDUpdate();

    // Initialize MainHUD at start
    public abstract void HUDInitialize();  
        
}
