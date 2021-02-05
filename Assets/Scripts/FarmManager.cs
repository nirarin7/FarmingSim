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
    void Update() {
    }

    public void AdvanceDay() {
        currentDay++;


        

        var maturables = FindObjectsOfType<MonoBehaviour>().OfType<IMaturable>();

        foreach (var maturable in maturables) {
            
            maturable.Matures();
            
        }


    }
    
    

 

    
}

