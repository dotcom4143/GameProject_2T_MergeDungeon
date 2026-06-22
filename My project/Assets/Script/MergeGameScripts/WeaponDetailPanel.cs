using UnityEngine;
using UnityEngine.UI;

public class WeaponDetailPanel : MonoBehaviour
{
    [Header("바꿔 끼울 UI 컴포넌트들")]
    public Text titleText;
    public Image weaponIcon;
    public Text recipeText;

    private WeaponData currentWeaponData;

    public void OpenPanel(WeaponData data)
    {
        currentWeaponData = data;

        titleText.text = data.weaponName;
        weaponIcon.sprite = data.weaponSprite;

        string recipeInfo = "필요 재료:\n";
        bool hasRecipe = false;

        for (int i = 1; i <= 4; i++)
        {
            if (data.requiredMaterialCounts[i] > 0)
            {
                recipeInfo += $"Lv.{i} 재료 {data.requiredMaterialCounts[i]}개\n";
                hasRecipe = true;
            }
        }

        if (!hasRecipe) recipeInfo = "필요한 재료가 없습니다.";
        recipeText.text = recipeInfo;

        gameObject.SetActive(true);
    }

    public void OnClickCraft()
    {
        if (currentWeaponData == null) return;

        MergeGameManager gameManager = Object.FindAnyObjectByType<MergeGameManager>();
        WeaponInventoryManager invManager = Object.FindAnyObjectByType<WeaponInventoryManager>();
        
        if (gameManager != null && invManager != null)
        {
            if (invManager.CanAddWeapon(currentWeaponData))
            {
                if (gameManager.TryConsumeMaterialsForWeapon(currentWeaponData))
                {
                    invManager.TryAddWeapon(currentWeaponData);
                }
            }
        }
    }

    public void ClosePanel() => gameObject.SetActive(false);
}