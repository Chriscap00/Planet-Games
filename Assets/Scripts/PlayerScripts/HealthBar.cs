using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void setMaxBarValue(int value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void setValue(int value)
    {
        slider.value = value;
    }
}
