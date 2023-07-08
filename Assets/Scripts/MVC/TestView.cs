using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TestView : MonoBehaviour
{
    /// <summary>
    /// タイトルテキスト
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI titleText = null;

    /// <summary>
    /// 遷移回数テキスト
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI transitionCountText = null;

    /// <summary>
    /// ニューゲームボタン
    /// </summary>
    [SerializeField]
    private UTButton newGameButton = null;

    public UTButton NewGameButton => newGameButton;

    /// <summary>
    /// 続けるボタン
    /// </summary>
    [SerializeField]
    private UTButton continueButton = null;

    public UTButton ContinueButton => continueButton;

    /// <summary>
    /// オプションボタン
    /// </summary>
    [SerializeField]
    private UTButton optionButton = null;

    public UTButton OptionButton => optionButton;

    /// <summary>
    /// 選択矢印オブジェクト
    /// </summary>
    [SerializeField]
    private RectTransform arrowRect = null;

    [SerializeField]
    private Outline buttonFiledOutline = null;

    /// <summary>
    /// アウトラインサイズ
    /// </summary>
    private float buttonFiledOutlineSize => buttonFiledOutline.effectDistance.x;

    [SerializeField]
    private VerticalLayoutGroup buttonFiledVertical = null;

    private Vector2 buttonSize => (newGameButton.transform as RectTransform).sizeDelta;

    /// <summary>
    /// 遷移画像
    /// </summary>
    [SerializeField]
    private Image backLoadImage = null;

    public void Init()
    {
        titleText.text = TitleConst.TitleName;
        StartCoroutine(DoColorHSV());
        transitionCountText.text = TitleConst.TransitionCount;
        newGameButton.SetText(TitleConst.NewGameText);
        continueButton.SetText(TitleConst.ContinueText);
        optionButton.SetText(TitleConst.OptionText);
        arrowRect.gameObject.SetSafeActive(false);
        SetButtonEvent();
    }

    private IEnumerator DoColorHSV()
    {
        float value = 0;
        while (true)
        {
            titleText.color = Color.HSVToRGB(value / 360, 0.5f, 1);
            value = value > 360 ? 0 : ++value;
            yield return null;
        }
    }

    private void SetButtonEvent()
    {
        // 選択矢印
        newGameButton.OnEnter = () => SelectArrowPosition(0);
        continueButton.OnEnter = () => SelectArrowPosition(1);
        optionButton.OnEnter = () => SelectArrowPosition(2);

        newGameButton.OnExit = () => arrowRect.gameObject.SetSafeActive(false);
        continueButton.OnExit = () => arrowRect.gameObject.SetSafeActive(false);
        optionButton.OnExit = () => arrowRect.gameObject.SetSafeActive(false);
    }

    private void SelectArrowPosition(int buttonIndex)
    {
        arrowRect.gameObject.SetSafeActive(true);

        const float XPosition = 80;
        var rect = arrowRect.anchoredPosition;
        rect.x = XPosition;
        // -5(outline) - 30(初期値Top)
        float initY = -(buttonFiledOutlineSize + buttonFiledVertical.padding.top + buttonSize.y / 2);
        //  スペース + ボタンサイズY / 2;
        rect.y = initY - (buttonFiledVertical.spacing + buttonSize.y) * buttonIndex;
        arrowRect.anchoredPosition = rect;
    }

    public void SetTransitionCount(int transitionCount)
    {
        transitionCountText.SetText(string.Format(TitleConst.TransitionCount, transitionCount));
    }

    /// <summary>
    /// 3秒間操作不可 / 明転・暗転
    /// </summary>
    public void LoadImage(Action fadeOutCallBack = null, Action fadeInCallBack = null)
    {
        backLoadImage.gameObject.SetSafeActive(true);
        backLoadImage.color = Vector4.zero;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(backLoadImage.DOFade(1, 1.25f))
        .AppendCallback(() => fadeOutCallBack?.Invoke())
        .AppendInterval(0.5f)
        .Append(backLoadImage.DOFade(0, 1.25f))
        .OnComplete(() =>
        {
            fadeInCallBack?.Invoke();
            backLoadImage.gameObject.SetSafeActive(false);
        });
    }
}
