using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableMaterial : MonoBehaviour,
    IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public int materialLevel = 1;
    public float snapBackSpeed = 20f;

    public bool isDragging = false;
    public Vector2 originalPosition;

    public Image uiImage;
    public MergeGameManager GameManager;

    private RectTransform rectTransform;
    private RectTransform parentRect;
    private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        GameManager = FindAnyObjectByType<MergeGameManager>();
        canvas = GetComponentInParent<Canvas>();
        parentRect = transform.parent?.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (!isDragging && Vector2.Distance(rectTransform.anchoredPosition, originalPosition) > 0.5f)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(
                rectTransform.anchoredPosition, originalPosition, snapBackSpeed * Time.deltaTime);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        originalPosition = rectTransform.anchoredPosition;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            eventData.position,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out Vector2 localPoint
        );

        localPoint.x = Mathf.Clamp(localPoint.x, -GameManager.gameWidth / 2f, GameManager.gameWidth / 2f);
        localPoint.y = Mathf.Clamp(localPoint.y, -GameManager.gameHeight / 2f, GameManager.gameHeight / 2f);

        rectTransform.anchoredPosition = localPoint;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

        DraggableMaterial targetMaterial = GameManager.FindClosestMaterial(this);

        if (targetMaterial != null)
        {
            if (targetMaterial.materialLevel == this.materialLevel)
                GameManager.MergeMaterials(this, targetMaterial);
            else
                ReturnToOriginalPosition();
        }
        else
        {
            originalPosition = rectTransform.anchoredPosition;
        }
    }

    public void ReturnToOriginalPosition()
    {
        if (rectTransform != null)
            rectTransform.anchoredPosition = originalPosition;
    }

    public void SetMaterialLevel(int level)
    {
        materialLevel = level;

        if (uiImage == null) uiImage = GetComponent<Image>();
        if (GameManager == null) GameManager = FindAnyObjectByType<MergeGameManager>();

        if (GameManager != null && GameManager.materialSprites.Length >= level)
        {
            if (uiImage != null)
                uiImage.sprite = GameManager.materialSprites[level - 1];
        }
    }
}