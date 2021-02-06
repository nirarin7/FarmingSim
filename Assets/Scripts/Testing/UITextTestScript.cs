using UnityEngine;
using UnityEngine.UI;

public class UITextTestScript : MonoBehaviour {
    public Inventory Inventory;
    private Text Text;

    // Start is called before the first frame update
    void Start() {
        Text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        if (Text != null && Inventory != null) {
            Text.text = Inventory.ToString();
        }

        var animals = FindObjectsOfType<Animal>();
        if (Text != null) {

            foreach (var animal in animals) {
                Text.text += animal.ToString();
            }
        }
    }
}