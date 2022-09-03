using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SimpleWindow : MonoBehaviour
{
    [Header("Table properties")]
    [SerializeField] private float _tableFadeTime = 1f;

    [Header("Background properties")]
    [SerializeField] private float _backgroundFadeTime = 0.5f;
    [SerializeField] private float _backgroundMaxAlpha = 0.6f;
    [SerializeField] private Image _backgroundImage;

    [Header("Other properties")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _rectTransform;

    private Vector2 _startPosition;
    public Action OnBeforeAction;
    public TweenCallback OnAfterAction;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_tableFadeTime < 0f) _tableFadeTime = 0f;
        if (_backgroundFadeTime < 0f) _backgroundFadeTime = 0f;
        if (_backgroundMaxAlpha < 0f) _backgroundMaxAlpha = 0f;
    }
    #endregion

    protected virtual void OnEnable()
    {
        ShowWindow();
    }

    private void Start()
    {
        _startPosition = _rectTransform.anchoredPosition;
        DOTween.Init();
    }

    public virtual void ShowWindow()
    {
        
        OnBeforeAction?.Invoke();

        //Setup
        _canvasGroup.alpha = 0f;
        _rectTransform.transform.localPosition = new Vector2(0, 550f);

        //Table
        _rectTransform.DOAnchorPos(Vector2.zero, _tableFadeTime, false).SetEase(Ease.OutBack);
        _canvasGroup.DOFade(1, _tableFadeTime).OnComplete(OnAfterAction);

        //Backrgound
        _backgroundImage.gameObject.SetActive(true);
        _backgroundImage.DOFade(_backgroundMaxAlpha, _backgroundFadeTime);
    }

    public virtual void HideWindow()
    {
        _rectTransform.DOAnchorPos(_startPosition, _tableFadeTime).SetEase(Ease.InBack);
        _canvasGroup.DOFade(0, _tableFadeTime);
        _backgroundImage.DOFade(0, _backgroundFadeTime).OnComplete(Deactivate);
    }

    private void Deactivate()
    {
        _backgroundImage.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
