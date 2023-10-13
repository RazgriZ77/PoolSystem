using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePoolController : MonoBehaviour {
    // ==================== VARIABLES ===================
    #region Public Variables
    public static PoolController<PoolEntity> PoolController;
    #endregion
    
    #region Private Variables
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int startAmount = 0;
    [Space]
    [SerializeField] private KeyCode spawnKey = KeyCode.Space;
    [SerializeField] private KeyCode returnAllKey = KeyCode.F;

    private SampleObject currentSampleObject;
    #endregion
    
    // ==================== INICIO ====================
    private void Awake() {
        Physics.autoSimulation = true;
        PoolController = new(objectPrefab, transform, CallOnPull, CallOnPush, startAmount);
    }

    private void Update() {
        if (Input.GetKeyDown(spawnKey)) PullFromPool();
        if (Input.GetKeyDown(returnAllKey)) ReturnAll();
    }
    
    // ==================== METODOS ====================
    private void PullFromPool() {
        currentSampleObject = (SampleObject)PoolController.Pull();
        
        currentSampleObject.transform.position = transform.position;
        currentSampleObject.RB.velocity = Vector3.zero;
    }

    private void ReturnAll() {
        PoolController.PushAll();
    }
    
    #region Pool
    private void CallOnPull(PoolEntity _poolEntity) {
        // Se llama cada vez que se hace pull
        Debug.Log("Se ha hecho pull de un objeto: " + name);
    }

    private void CallOnPush(PoolEntity _poolEntity) {
        // Se llama cada vez que se hace push
        Debug.Log("Se ha hecho push de un objeto: " + name);
    }
    #endregion
}