using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarSlider : MonoBehaviour
{
    public Gradient gradient;
    public Image fill;

    private Slider slider;
        
    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public virtual void SetMaxValue(float value)
    {
        slider.maxValue= value;
        slider.value = value;

        fill.color = gradient.Evaluate(1f);
    }

    public virtual void SetValue(float value)
    {
        slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
