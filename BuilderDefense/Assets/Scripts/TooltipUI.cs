using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance { get; private set; }
    [SerializeField] private RectTransform canvasRectTransform;
    private TextMeshProUGUI _textMeshProUGUI;
    private RectTransform _backgroundRectTransform;
    private RectTransform _rectTransform;
    private TooltipTimer _tooltipTimer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _textMeshProUGUI = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        _backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        _rectTransform = GetComponent<RectTransform>();
        Hide();
    }

    private void Update()
    {
        HandleFollowMouse();

        if (_tooltipTimer != null)
        {
            _tooltipTimer.timer -= Time.deltaTime;
            if (_tooltipTimer.timer <= 0)
            {
                Hide();
            }
        }
    }

    private void HandleFollowMouse()
    {
        var anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        if (anchoredPosition.x + _backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
            anchoredPosition.x = canvasRectTransform.rect.width - _backgroundRectTransform.rect.width;
        if (anchoredPosition.y + _backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
            anchoredPosition.y = canvasRectTransform.rect.height - _backgroundRectTransform.rect.height;

        _rectTransform.anchoredPosition = anchoredPosition;
    }

    private void SetText(string tooltipText)
    {
        _textMeshProUGUI.SetText(tooltipText);
        _textMeshProUGUI.ForceMeshUpdate();
        var textSize = _textMeshProUGUI.GetRenderedValues(false);
        var padding = new Vector2(8, 8);
        _backgroundRectTransform.sizeDelta = textSize + padding;
    }

    public void Show(string tooltipText, TooltipTimer tooltipTimer = null)
    {
        _tooltipTimer = tooltipTimer;
        gameObject.SetActive(true);
        SetText(tooltipText);
    }

    public void Hide()
    {
        _tooltipTimer = null;
        gameObject.SetActive(false);
    }


    public class TooltipTimer
    {
        public float timer;
    }
}