public interface IPool<T> {
    T Pull();
    void Push(T _t);
}