using System;
using DG.Tweening;
using UnityEngine;

public class ShopWindow : SimpleWindow
{
    [SerializeField] private float _productFadeTime = 0.2f;
    [SerializeField] private Transform[] _productItems;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_productFadeTime < 0f) _productFadeTime = 0f;
    }
    #endregion


    protected override void OnEnable()
    {
        OnAfterAction += ShowProducts;
        base.OnEnable();
    }

    private void OnDisable()
    {
        OnAfterAction -= ShowProducts;
    }


    public override void ShowWindow()
    {
        HideAllProducts();
        Invoke(nameof(ShowProducts), 0.4f);
        base.ShowWindow();
    }

    private void ShowProducts()
    {
        Sequence sequence = DOTween.Sequence();

        foreach (Transform product in _productItems)
        {
            sequence.Append(product.DOScale(1, _productFadeTime).SetEase(Ease.InOutSine));
        }
    }

    private void HideAllProducts()
    {
        foreach (Transform product in _productItems)
        {
            product.localScale = Vector3.zero;
        }
    }

}
