using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "New Plant", menuName = "Scriptable Objects/Plant", order = 52)]
    public class PlantData : ScriptableObject {
        [SerializeField] private new string name;

        [SerializeField] private int growthTimeDays;
        [SerializeField] private int minDrop;
        [SerializeField] private int maxDrop;

        [SerializeField] private ItemData drop;
        
        [SerializeField] private HarvestType harvestType;
        
        [SerializeField] private Sprite seedSprite;
        [SerializeField] private Sprite harvestSprite;
        [SerializeField] private List<Sprite> growingSprites = new List<Sprite>();

        public string Name => name;
        
        public int GrowthTimeDays => growthTimeDays;
        public int MinDrop => minDrop;
        public int MaxDrop => maxDrop;
        
        public ItemData Drop => drop;
        
        public HarvestType HarvestType => harvestType;
        
        public Sprite SeedSprite => seedSprite;
        public Sprite HarvestSprite => harvestSprite;
        public List<Sprite> GrowingSprites => growingSprites;
    }
}