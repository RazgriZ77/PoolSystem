using System;
using UnityEngine;

public class PoolEntity : MonoBehaviour, IPoolable<PoolEntity> {
    // ==================== VARIABLES ===================
    #region Public Variables
    #endregion

    #region Private Variables
    private Action<PoolEntity> returnToPool;
    #endregion

    // ==================== INICIO ====================
    private void OnDisable() {
        ReturnToPool();
    }
    
    // ==================== METODOS ====================
    #region IPoolable
    public void Initialize(Action<PoolEntity> _returnAction) {
        this.returnToPool = _returnAction;
    }

    public void ReturnToPool() {
        returnToPool?.Invoke(this);
    }
    #endregion

}