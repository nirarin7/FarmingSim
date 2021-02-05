using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmManager : MonoBehaviour {
    public static FarmManager Instance;
    public int currentDay;


    private void Awake() {
        if (!Instance) {
            Instance = this;
        } else if (Instance) {
            Debug.Log("Instance already exist, destroying object");
            Destroy(this);
        }
    }


    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void AdvanceDay() {
        currentDay++;
        var maturables = FindObjectsOfType<MonoBehaviour>().OfType<IMaturable>();

        foreach (var maturable in maturables) {
            maturable.Matures();
        }
    }
}