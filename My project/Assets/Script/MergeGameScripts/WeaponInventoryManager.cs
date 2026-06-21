using UnityEngine;
using UnityEngine.UI;

public class WeaponInventoryManager : MonoBehaviour
{
    [Header("무기 가방 데이터 (크기 4)")]
    public WeaponData[] equippedWeapons = new WeaponData[4];

    [Header("UI 슬롯 설정 (크기 4짜리 이미지 배열)")]
    public Image[] slotImages;       
    public Image[] slotHighlights;   

    private int currentSelectedSlot = 0; 

    void Start()
    {
        UpdateInventoryUI();
        SelectSlot(0); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectSlot(3);
    }

    public bool TryAddWeapon(WeaponData newWeapon)
    {
        if (newWeapon == null) return false;

        for (int i = 0; i < 4; i++)
        {
            if (equippedWeapons[i] != null && equippedWeapons[i] == newWeapon)
            {
                return false; 
            }
        }

        for (int i = 0; i < 4; i++)
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

    private void SelectSlot(int slotIndex)
    {
        currentSelectedSlot = slotIndex;

        for (int i = 0; i < 4; i++)
        {
            if (slotHighlights[i] != null)
            {
                slotHighlights[i].enabled = (i == slotIndex);
            }
        }
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < 4; i++)
        {
            if (equippedWeapons[i] != null)
            {
                slotImages[i].sprite = equippedWeapons[i].weaponSprite;
                slotImages[i].color = Color.white;
                slotImages[i].enabled = true;
            }
            else
            {
                slotImages[i].enabled = false;
            }
        }
    }
}