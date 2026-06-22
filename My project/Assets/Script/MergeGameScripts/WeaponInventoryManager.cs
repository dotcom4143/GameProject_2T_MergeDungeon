using UnityEngine;
using UnityEngine.UI;

public class WeaponInventoryManager : MonoBehaviour
{
    [Header("장착된 무기 데이터 (상단 4칸)")]
    public WeaponData[] equippedWeapons = new WeaponData[4];

    [Header("상단 장착 슬롯 UI 이미지 4개")]
    public Image[] slotImages;

    void Start()
    {
        UpdateInventoryUI();
    }

    public bool TryAddWeapon(WeaponData newWeapon)
    {
        for (int i = 0; i < equippedWeapons.Length; i++)
        {
            if (equippedWeapons[i] == null)
            {
                equippedWeapons[i] = newWeapon;
                UpdateInventoryUI();
                return true;
            }
        }
        return false;
    }

    public bool CanAddWeapon()
    {
        for (int i = 0; i < equippedWeapons.Length; i++)
        {
            if (equippedWeapons[i] == null) return true;
        }
        return false;
    }

    public bool CanAddWeapon(WeaponData data)
    {
        return CanAddWeapon();
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (i >= equippedWeapons.Length) break;

            if (equippedWeapons[i] != null)
            {
                slotImages[i].sprite = equippedWeapons[i].weaponSprite;
                slotImages[i].enabled = true;
            }
            else
            {
                slotImages[i].sprite = null;
                slotImages[i].enabled = false; 
            }
        }
    }

    public void ResetInventory()
    {
        for (int i = 0; i < equippedWeapons.Length; i++)
        {
            equippedWeapons[i] = null;
        }
        UpdateInventoryUI();
    }
}