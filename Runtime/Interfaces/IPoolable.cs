public interface IPoolable<T> {
    void OnPulled(System.Action<T> _returnAction);
    void OnPushed();
}