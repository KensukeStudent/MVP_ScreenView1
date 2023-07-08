using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// Buttonクラスをプロジェクト用に拡張したクラス
/// <para>UTはUniRxTestプロジェクトの略</para>
/// </summary>
public class UTButton : Button
{
    private TextMeshProUGUI buttonText = null;

    public Action OnEnter = null;

    public Action OnExit = null;

    protected override void Awake()
    {
        if (buttonText == null)
        {
            buttonText = transform.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void AddLisnner(UnityEngine.Events.UnityAction call)
    {
        onClick.AddListener(call);
    }

    public void RemoveAllListeners()
    {
        onClick.RemoveAllListeners();
    }

    public void RemoveListener(UnityEngine.Events.UnityAction call)
    {
        onClick.RemoveListener(call);
    }

    public void SetText(string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            buttonText.text = text;
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        OnEnter?.Invoke();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        OnExit?.Invoke();
    }
}
