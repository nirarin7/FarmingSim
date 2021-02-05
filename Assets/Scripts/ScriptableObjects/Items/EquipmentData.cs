using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Scriptable Objects/Items/Equipment Item")]
public class EquipmentData : ItemData {

    [SerializeField] private int damage;
    [SerializeField] private List<EquipmentRoles> roles = new List<EquipmentRoles>();

    public int Damage => damage;

    public List<EquipmentRoles> Roles => roles;
}

