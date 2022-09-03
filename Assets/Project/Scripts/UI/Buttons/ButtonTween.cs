using UnityEngine.Events;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ButtonTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool Interactable = true;
    public UnityEvent OnClickAction;

    private Transform _transform;
    private TweenParams _tweenParams;
    private bool _isProcessing = false;


    private void Start()
    {
        _transform = GetComponent<Transform>();
        _tweenParams = new TweenParams().SetEase(Ease.InBack).SetLoops(2, LoopType.Yoyo).OnComplete(ButtonCallback);
    }

    private void ButtonCallback()
    {
        _isProcessing = false;
        OnClickAction?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _transform.DOScale(1.1f, 0.2f).SetEase(Ease.InOutSine);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _transform.DOScale(1f, 0.2f).SetEase(Ease.InOutSine);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isProcessing && Interactable)
        {
            _isProcessing = true;
            _transform.DOScale(0.7f, 0.2f).SetAs(_tweenParams);
        }
    }
}
