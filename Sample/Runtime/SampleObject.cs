using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleObject : PoolEntity {
    // ==================== VARIABLES ===================
    #region Public Variables
    public Rigidbody RB { get { return rB; } }
    #endregion
    
    #region Private Variables
    [SerializeField] private Rigidbody rB;
    #endregion
    
    // ==================== INICIO ====================
    
    // ==================== METODOS ====================
    
}