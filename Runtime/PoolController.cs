using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController<T> : IPool<T> where T : MonoBehaviour, IPoolable<T> {
    // ==================== VARIABLES ===================
    #region Public Variables
    public int pooledCount { get { return pooledObjects.Count; } }
    #endregion

    #region Private Variables
    private Action<T> pullObject;
    private Action<T> pushObject;
    
    private Stack<T> pooledObjects = new();
    
    private GameObject prefab;
    private Transform container;

    private T temp;
    #endregion

    // ==================== METODOS ====================
    public PoolController(GameObject _pooledObject, Transform _container, int _numToSpawn = 0) {
        this.prefab = _pooledObject;
        this.container = _container;
        
        Spawn(_numToSpawn);
    }

    public PoolController(GameObject _pooledObject, Transform _container, Action<T> _pullObject, Action<T> _pushObject, int _numToSpawn = 0) {
        this.prefab = _pooledObject;
        this.container = _container;
        this.pullObject = _pullObject;
        this.pushObject = _pushObject;

        Spawn(_numToSpawn);
    }

    #region Pull
    public T Pull(Vector3 _position = default) {
        if (pooledCount > 0) temp = pooledObjects.Pop();
        else temp = GameObject.Instantiate(prefab, container).GetComponent<T>();

        temp.gameObject.SetActive(true);
        temp.OnPulled(Push);

        temp.transform.position = _position;

        pullObject?.Invoke(temp);

        return temp;
    }

    public T Pull(Vector3 _position, Quaternion _rotation) {
        temp = Pull(_position);
        temp.transform.rotation = _rotation;

        return temp;
    }
    #endregion

    #region PullGameObject
    public GameObject PullGameObject() {
        return Pull().gameObject;
    }

    public GameObject PullGameObject(Vector3 _position) {
        GameObject _go = Pull().gameObject;
        _go.transform.position = _position;

        return _go;
    }

    public GameObject PullGameObject(Vector3 _position, Quaternion _rotation) {
        GameObject _go = Pull().gameObject;
        _go.transform.SetPositionAndRotation(_position, _rotation);

        return _go;
    }
    #endregion

    #region Push
    public void Push(T _t) {
        pooledObjects.Push(_t);

        pushObject?.Invoke(_t);

        _t.gameObject.SetActive(false);
    }

    public void PushAll() {
        for (int i = 0; i < container.childCount; i++) {
            if (container.GetChild(i).gameObject.activeSelf) {
                container.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    #endregion

    private void Spawn(int _number) {
        for (int i = 0; i < _number; i++) {
            temp = GameObject.Instantiate(prefab, container).GetComponent<T>();
            pooledObjects.Push(temp);
            temp.gameObject.SetActive(false);
        }
    }
}