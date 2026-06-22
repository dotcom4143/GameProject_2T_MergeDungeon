using UnityEngine;
using UnityEngine.UI;

public class WeaponCraftButton : MonoBehaviour
{
    public WeaponData weaponData;
    public Image iconImage;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickCraft);
    }

    public void SetupButton(WeaponData data)
    {
        weaponData = data;
        if (weaponData != null && iconImage != null)
        {
            iconImage.sprite = weaponData.weaponSprite;
            iconImage.enabled = true;
        }
    }

    void OnClickCraft()
    {
        if (weaponData == null) return;

        MergeGameManager gameManager = Object.FindAnyObjectByType<MergeGameManager>();
        WeaponInventoryManager invManager = Object.FindAnyObjectByType<WeaponInventoryManager>();

        if (gameManager != null && invManager != null)
        {
            bool hasEmptySlot = false;
            for (int i = 0; i < invManager.equippedWeapons.Length; i++)
            {
                if (invManager.equippedWeapons[i] == null) { hasEmptySlot = true; break; }
            }
            if (!hasEmptySlot) return;

            for (int i = 0; i < invManager.equippedWeapons.Length; i++)
            {
                if (invManager.equippedWeapons[i] == weaponData) return;
            }

            if (gameManager.TryConsumeMaterialsForWeapon(weaponData))
            {
                invManager.TryAddWeapon(weaponData);
            }
        }
    }
}