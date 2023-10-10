using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleObjectDisabler : MonoBehaviour {
    // ==================== VARIABLES ===================
    #region Public Variables
    #endregion
    
    #region Private Variables
    #endregion
    
    // ==================== INICIO ====================
    private void OnTriggerEnter(Collider other) {
        other.gameObject.SetActive(false);
    }
    
    // ==================== METODOS ====================
    
}