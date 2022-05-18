using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_Initilize : MonoBehaviour
{
    private static bool isOpened = false;
    [SerializeField] private CH_Func _sceneLoadedEventChannel;
    [SerializeField] private CH_Func disableButtons;
    [SerializeField] private S_Transition m_transitionSO;
    AsyncOperation operationSceneLoad;
    //#if UNITY_EDITOR

    private void Awake()
    {
        if (!isOpened)
        {
            m_transitionSO._currentLocation = SceneManager.GetActiveScene().name;
            operationSceneLoad = SceneManager.LoadSceneAsync("_PersistentManagers", LoadSceneMode.Additive);
            operationSceneLoad.completed += LoadSceneGlobals;
            isOpened = true;
        }
    }

    private void OnDisable()
    {
        if (operationSceneLoad != null)
            operationSceneLoad.completed -= LoadSceneGlobals;
    }

    public void LoadSceneGlobals(AsyncOperation sc)
    {
        _sceneLoadedEventChannel.RaiseEvent();
        operationSceneLoad.completed -= LoadSceneGlobals;
    }
}
