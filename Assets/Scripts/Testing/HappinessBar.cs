using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappinessBar : MonoBehaviour {
    public Slider slider;
    public Animal animal;


    // Update is called once per frame
    void Update() {
        slider.value = animal.currentHappiness;
    }
}

