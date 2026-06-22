using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScrollViewManager : MonoBehaviour
{
    [Header("무기 데이터 리스트 (여기에 파일 20개 드래그)")]
    public List<WeaponData> weaponDataList;

    [Header("복사해서 쓸 원본 버튼 프리팹")]
    public GameObject weaponButtonPrefab;

    [Header("버튼들이 생성될 Content 오브젝트")]
    public Transform contentParent;

    void Start()
    {
        GenerateWeaponButtons();
    }

    public void GenerateWeaponButtons()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (WeaponData data in weaponDataList)
        {
            if (data == null) continue;

            GameObject newButton = Instantiate(weaponButtonPrefab);
            newButton.transform.SetParent(contentParent, false);
            
            WeaponCraftButton craftScript = newButton.GetComponent<WeaponCraftButton>();
            if (craftScript != null)
            {
                craftScript.SetupButton(data);
            }
        }

        Canvas.ForceUpdateCanvases();
        RectTransform contentRect = contentParent.GetComponent<RectTransform>();
        if (contentRect != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);
        }
    }
}