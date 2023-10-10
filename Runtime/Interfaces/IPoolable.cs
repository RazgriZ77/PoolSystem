public interface IPoolable<T> {
    void Initialize(System.Action<T> _returnAction);
    void ReturnToPool();
}