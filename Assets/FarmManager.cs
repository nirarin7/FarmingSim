using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmManager : MonoBehaviour {
    
    public int currentDay;
    
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AdvanceDay() {
        currentDay++;
    }

    public void GrowPlants() {
        var plantGameObjects = GameObject.FindGameObjectsWithTag("Plant");
        var plantList = new List<Plant>();
        foreach (var plantGameObject in plantGameObjects) {
            plantList.Add(plantGameObject.GetComponent<Plant>());
        }
       
        foreach (var plant in plantList) 
            plant.Grow();
    }
    
}

