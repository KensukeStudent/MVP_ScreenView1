using UniRx;

public class TestModel
{
    public ReactiveProperty<int> Count = new(0);

    public void IncreaseTransitionCount()
    {
        Count.Value++;
    }
}
