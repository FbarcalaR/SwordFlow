using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public float health = 50f;
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private void Start()
    {
        if (slider != null)
        {
            slider.value = health;
            slider.maxValue = health;
            fill.color = gradient.Evaluate(1f);
        }
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(slider != null)
        {
            slider.value = health;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}
