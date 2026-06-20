using UnityEngine;
using System.Collections.Generic;

public class MergeGameManager : MonoBehaviour
{
    [Header("머지 UI 패널 설정")]
    public GameObject mergePanel;       
    public Transform materialParent;    

    [Header("아이템 설정")]
    public GameObject materialPrefabs;   
    public Sprite[] materialSprites;    
    public int maxMaterialLevel = 4;    

    [Header("머지게임 영역")]
    public float gameWidth = 1700f;
    public float gameHeight = 450f;
    public float mergeDistance = 100f;  

    [Header("재료 리스트 (현재 판 위에 있는 개체들)")]
    public List<DraggableMaterial> activeMaterials = new List<DraggableMaterial>();

    [Header("인벤토리 데이터")]
    public int[] storedMaterialCounts = new int[5]; 

    void Start()
    {
        if (mergePanel != null) mergePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) ToggleMergePanel();
    }

    public void OnClickSpawnMaterialButton()
    {
        if (!MergePanelActive()) return;
        SpawnNewMaterial();
    }

    public void SpawnNewMaterial()
    {
        float randomX = Random.Range(-gameWidth / 2f, gameWidth / 2f);
        float randomY = Random.Range(-gameHeight / 2f, gameHeight / 2f);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        int materialLevel = Random.Range(0, 100) < 80 ? 1 : 2;
        CreateMaterialAtPosition(spawnPosition, materialLevel);
    }

    public void OnClickSaveMaterialsButton()
    {
        if (activeMaterials.Count == 0) return;

        for (int i = activeMaterials.Count - 1; i >= 0; i--)
        {
            DraggableMaterial mat = activeMaterials[i];
            if (mat != null)
            {
                storedMaterialCounts[mat.materialLevel]++;
                
                Destroy(mat.gameObject);
            }
        }

        activeMaterials.Clear();
    }

    public bool TryConsumeMaterials(int level, int count)
    {
        if (storedMaterialCounts[level] >= count)
        {
            storedMaterialCounts[level] -= count;
            return true;
        }
        return false;
    }

    public DraggableMaterial CreateMaterialAtPosition(Vector3 position, int level)
    {
        level = Mathf.Clamp(level, 1, maxMaterialLevel);
        GameObject materialObj = Instantiate(materialPrefabs, materialParent);
        materialObj.name = "MaterialLevel" + level;

        DraggableMaterial material = materialObj.GetComponent<DraggableMaterial>();
        if (material == null) material = materialObj.AddComponent<DraggableMaterial>();

        material.GameManager = this;
        material.SetMaterialLevel(level);
        activeMaterials.Add(material);

        RectTransform rect = materialObj.GetComponent<RectTransform>();
        if (rect != null)
        {
            rect.anchoredPosition = new Vector2(position.x, position.y);
            material.originalPosition = new Vector2(position.x, position.y);
        }
        return material;
    }

    public DraggableMaterial FindClosestMaterial(DraggableMaterial myMaterial)
    {
        DraggableMaterial closestMaterial = null;
        float closestDistance = float.MaxValue;
        RectTransform myRect = myMaterial.GetComponent<RectTransform>();
        if (myRect == null) return null;

        foreach (DraggableMaterial otherMaterial in activeMaterials)
        {
            if (otherMaterial == myMaterial || otherMaterial == null) continue;
            RectTransform otherRect = otherMaterial.GetComponent<RectTransform>();
            if (otherRect == null) continue;

            float distance = Vector2.Distance((Vector2)myRect.position, (Vector2)otherRect.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestMaterial = otherMaterial;
            }
        }
        if (closestDistance <= mergeDistance) return closestMaterial;
        return null;
    }

    public void RemoveMaterial(DraggableMaterial material)
    {
        if (material == null) return;
        if (activeMaterials.Contains(material)) activeMaterials.Remove(material);
        Destroy(material.gameObject);
    }

    public void MergeMaterials(DraggableMaterial draggableMaterial, DraggableMaterial targetMaterial)
    {
        if (draggableMaterial == null || targetMaterial == null || draggableMaterial.materialLevel != targetMaterial.materialLevel)
        {
            if (draggableMaterial != null) draggableMaterial.ReturnToOriginalPosition();
            return;
        }

        int newLevel = targetMaterial.materialLevel + 1;
        if (newLevel > maxMaterialLevel)
        {
            draggableMaterial.ReturnToOriginalPosition();
            return;
        }

        RectTransform targetRect = targetMaterial.GetComponent<RectTransform>();
        Vector3 mergePosition = targetRect != null ? (Vector3)targetRect.anchoredPosition : targetMaterial.transform.localPosition;

        RemoveMaterial(draggableMaterial);
        RemoveMaterial(targetMaterial);
        CreateMaterialAtPosition(mergePosition, newLevel);
    }

    public void ToggleMergePanel() { if (mergePanel != null) mergePanel.SetActive(!mergePanel.activeSelf); }
    public bool MergePanelActive() { return mergePanel != null && mergePanel.activeSelf; }
    
    public bool TryConsumeMaterialsForWeapon(WeaponData weaponData)
    {
        for (int i = 1; i <= 4; i++)
        {
            if (storedMaterialCounts[i] < weaponData.requiredMaterialCounts[i])
            {
                return false;
            }
        }

        for (int i = 1; i <= 4; i++)
        {
            storedMaterialCounts[i] -= weaponData.requiredMaterialCounts[i];
        }

        return true;
    }
}