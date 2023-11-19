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
        OnPushed();
    }
    
    // ==================== METODOS ====================
    #region IPoolable
    public virtual void OnPulled(Action<PoolEntity> _returnAction) {
        this.returnToPool = _returnAction;
    }

    public virtual void OnPushed() {
        returnToPool?.Invoke(this);
    }
    #endregion

}