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
    public T Pull() {
        T _t;

        if (pooledCount > 0) _t = pooledObjects.Pop();
        else _t = GameObject.Instantiate(prefab, container).GetComponent<T>();

        _t.gameObject.SetActive(true);
        _t.Initialize(Push);

        pullObject?.Invoke(_t);

        return _t;
    }

    public T Pull(Vector3 _position) {
        T _t = Pull();
        _t.transform.position = _position;

        return _t;
    }

    public T Pull(Vector3 _position, Quaternion _rotation) {
        T _t = Pull();
        _t.transform.SetPositionAndRotation(_position, _rotation);

        return _t;
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

    public void Push(T _t) {
        pooledObjects.Push(_t);

        pushObject?.Invoke(_t);

        _t.gameObject.SetActive(false);
    }

    private void Spawn(int _number) {
        T _t;

        for (int i = 0; i < _number; i++) {
            _t = GameObject.Instantiate(prefab, container).GetComponent<T>();
            pooledObjects.Push(_t);
            _t.gameObject.SetActive(false);
        }
    }
}