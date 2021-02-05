using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappinessBar : MonoBehaviour {
    public Slider slider;
    public Cow cow;


    // Update is called once per frame
    void Update() {
        slider.value = cow.currentHappiness;
    }
}

