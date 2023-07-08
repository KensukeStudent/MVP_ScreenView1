using UnityEngine;
using UniRx;

[RequireComponent(typeof(TestView))]
public class TestPresenter : MonoBehaviour
{
    private TestModel model = new TestModel();

    [SerializeField]
    private TestView view = null;

    private void Awake()
    {
        view.Init();
        Bind();
        SetEvent();
    }

    private void Bind()
    {
        // modelのCountを監視
        // viewの遷移回数更新メソッドを呼び出し描画
        // TestPresenterが破棄されたら監視を終了
        model.Count.Subscribe(x => view.SetTransitionCount(x)).AddTo(this);
    }

    private void SetEvent()
    {
        view.NewGameButton.AddLisnner(OnNewGameClick);
        view.ContinueButton.AddLisnner(OnContinueClick);
        view.OptionButton.AddLisnner(OnOptionClick);
    }

    public void OnNewGameClick()
    {
        Debug.Log("他処理A");
        view.LoadImage(model.IncreaseTransitionCount, null);
    }

    public void OnContinueClick()
    {
        Debug.Log("他処理B");
        view.LoadImage(model.IncreaseTransitionCount, null);
    }

    public void OnOptionClick()
    {
        Debug.Log("他処理C");
        view.LoadImage(model.IncreaseTransitionCount, null);
    }
}
