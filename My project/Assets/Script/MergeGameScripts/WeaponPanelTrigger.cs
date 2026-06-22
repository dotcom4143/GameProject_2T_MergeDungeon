using UnityEngine;

public class WeaponPanelToggler : MonoBehaviour
{
    [Header("토글할 무기 인벤토리 패널")]
    public GameObject weaponInventoryPanel;

    public void TogglePanel()
    {
        if (weaponInventoryPanel != null)
        {
            bool currentState = weaponInventoryPanel.activeSelf;
            weaponInventoryPanel.SetActive(!currentState);
        }
    }
}