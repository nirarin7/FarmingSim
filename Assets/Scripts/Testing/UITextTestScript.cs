using UnityEngine;
using UnityEngine.UI;

public class UITextTestScript : MonoBehaviour {
    public Player player;
    private Text Text;

    // Start is called before the first frame update
    void Start() {
        Text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        if (Text != null && player != null) {
            Text.text = player.Inventory.ToString();
        }
    }
}