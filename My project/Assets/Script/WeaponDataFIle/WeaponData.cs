using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "NewWeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public Sprite weaponSprite;
    public int[] requiredMaterialCounts = new int[5]; 
}