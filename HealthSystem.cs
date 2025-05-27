using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public Slider HealthBarSlider;
    public void GiveFullHealth( float health)
    {
        HealthBarSlider.maxValue = health;
        HealthBarSlider.value = health;

    }
    public void SetHealth(float health)
    {
        HealthBarSlider.value=health;
    }
}
