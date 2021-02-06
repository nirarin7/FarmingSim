using UnityEngine;

    [CreateAssetMenu(fileName = "New Animal", menuName = "Scriptable Objects/Animal", order = 0)]
    public class AnimalData : ScriptableObject {
        [SerializeField] private new string name;
        [SerializeField] private string description;
        
        [SerializeField] private int matureAge;
        [SerializeField] private int maxEnergy;
        [SerializeField] private int maxHappiness;

        [SerializeField] private ItemData itemDrop;

        [SerializeField] private Sprite youthSprite;
        [SerializeField] private Sprite matureSprite;


        public string Name => name;
        public string Description => description;
        public Sprite YouthSprite => youthSprite;
        public Sprite MatureSprite => matureSprite;
        public int MatureAge => matureAge;
        public int MaxEnergy => maxEnergy;
        public int MaxHappiness => maxHappiness;
        public ItemData ItemDrop => itemDrop;
    }