using System;
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
        if(gameObject.name == "Player")
        {
            CheckPlayerSlider();
        }
        if (slider != null)
        {
            slider.value = health;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }

    private void CheckPlayerSlider()
    {

        if (slider == null)
        {
            slider = GameObject.Find("HealthBar").GetComponentInChildren<Slider>();
        }
        if (gradient == null)
        {
            gradient = GameObject.Find("HealthBar").GetComponentInChildren<Gradient>();
        }
        if (fill == null)
        {
            fill = GameObject.Find("HealthBar").GetComponentInChildren<Image>();
        }
    }
}
