using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 1;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Transform[] _productItems;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_fadeTime < 0f) _fadeTime = 0;
    }
    #endregion

    public void PanelFadeIn()
    {
        HideAllProducts();
        _canvasGroup.alpha = 0f;
        _rectTransform.transform.localPosition = new Vector3(0f, 550, 0f);
        _rectTransform.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.OutElastic);
        _canvasGroup.DOFade(1, _fadeTime / 2).OnComplete(ShowProducts);
    }

    public void PanelFadeOut()
    {
        _canvasGroup.alpha = 1f;
        _rectTransform.transform.localPosition = Vector3.zero;
        _rectTransform.DOAnchorPos(new Vector2(0f, 550), _fadeTime, false).SetEase(Ease.InOutQuint);
        _canvasGroup.DOFade(0f, _fadeTime);
    }

    private void HideAllProducts()
    {
        foreach (Transform product in _productItems)
            product.localScale = Vector3.zero;
    }

    private void ShowProducts()
    {
        Sequence sequence = DOTween.Sequence();

        foreach (Transform product in _productItems)
            sequence.Append(product.DOScale(1f, _fadeTime / 5));
    }

}
