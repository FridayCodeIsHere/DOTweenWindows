using TMPro;
using UnityEngine;

public class MessageWindow : SimpleWindow
{
    [SerializeField] private TextMeshProUGUI _textComponent;
    private string _text;


    public void SetMessage(string text)
    {
        _text = text;
    }
    public override void ShowWindow()
    {
        _textComponent.text = _text;
        
        base.ShowWindow();
    }
}
