using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance { get; private set; }

    [SerializeField]
    private RectTransform canvasRectTramsform;
    private RectTransform backgroundRectTransform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform rectTransform;

    private void Awake()
    {
        Instance = this;

        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();

        Hide();
    }

    private void Update()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTramsform.localScale.x;

        if(anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTramsform.rect.width)
        {
            anchoredPosition.x = canvasRectTramsform.rect.width - backgroundRectTransform.rect.width;
        }

        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTramsform.rect.height)
        {
            anchoredPosition.y = canvasRectTramsform.rect.height - backgroundRectTransform.rect.height;
        }

        if(anchoredPosition.x < 0)
        {
            anchoredPosition.x = 0;
        }

        if (anchoredPosition.y < 0)
        {
            anchoredPosition.y = 0;
        }

        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void SetText(string tooltipText)
    {
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(16, 16);
        backgroundRectTransform.sizeDelta = textSize + padding;
    }

    public void Show(string tooltipText)
    {
        gameObject.SetActive(true);
        SetText(tooltipText);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
