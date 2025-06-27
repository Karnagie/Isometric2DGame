using UnityEngine;
#if UNITY_EDITOR
using UnityEngine.SceneManagement;
using Zenject;
#endif

namespace Code.Infrastructure.Loading
{
  // Has execution order to start before every other script
  public class SwitchToEntrySceneInEditor : MonoBehaviour
  {
#if UNITY_EDITOR
    private const int EntrySceneIndex = 0;
    
    public bool _enabled = true;

    private void Awake()
    {
      if (ProjectContext.HasInstance || _enabled == false) 
        return;
      
      foreach (GameObject root in gameObject.scene.GetRootGameObjects()) 
        root.SetActive(false);
      
      SceneManager.LoadScene(EntrySceneIndex);
    }
#endif
  }
}