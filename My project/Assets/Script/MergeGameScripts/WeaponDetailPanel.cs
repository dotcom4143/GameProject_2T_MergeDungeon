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
        recipeText.text = $"필요 재료: Lv.{data.requiredMaterialLevel} 재료 {data.requiredCount}개";

        gameObject.SetActive(true);
    }

    public void OnClickCraft()
    {
        MergeGameManager gameManager = FindObjectOfType<MergeGameManager>();
        
        if (gameManager.TryConsumeMaterials(currentWeaponData.requiredMaterialLevel, currentWeaponData.requiredCount))
        {
            Debug.Log($"{currentWeaponData.weaponName} 제작 성공! 인벤토리에서 재료 차감 완료.");
        }
        else
        {
            Debug.Log("재료가 부족하여 무기를 제작할 수 없습니다.");
        }
    }

    public void ClosePanel() => gameObject.SetActive(false);
}