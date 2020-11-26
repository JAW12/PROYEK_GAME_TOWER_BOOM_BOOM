using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    //healthbar : https://youtu.be/v1UGTTeQzbo

    public Slider Slider;
    public Color HighColor;
    public Color LowColor;
    public Vector3 Offset;

    public void SetHealth(float health, float maxHealth){
        //munculin hp kalo hp sudah berkurang dari max health
        Slider.gameObject.SetActive(health < maxHealth);

        //atur value slider saat ini
        Slider.value = health;

        //atur max value slider
        Slider.maxValue = maxHealth;

        //atur warna yang ditampilkan
        //Slider.normalizedValue -> digunakan untuk mengambil warna di antara warna high dan low
        Slider.fillRect.GetComponentInChildren<Image>().color =
            Color.Lerp(LowColor, HighColor, Slider.normalizedValue);
    }

    private void Start()
    {
        //biar healthbar agak naik dikit
        Offset = new Vector3(0f, 0.4f, 0f);
    }

    void Update()
    {        
        //posisi slider mengikuti posisi musuh
        Slider.transform.position = Camera.main.WorldToScreenPoint(
            transform.parent.position + Offset);
    }
}
