using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CubeMatch.UI
{
    public class LevelFailed : MonoBehaviour
    {
        [SerializeField] private GameObject failedScreen;

        #region MonoBehaviour Methods
        private void OnEnable()
        {
            StaticEvents.onLevelFailed += OnLevelFailed;
        }

        private void OnDisable()
        {
            StaticEvents.onLevelFailed -= OnLevelFailed;
        }
        #endregion

        #region EVENT LISTENERS
        private void OnLevelFailed()
        {
            failedScreen.SetActive(true);
        }

        public void OnRestartButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        #endregion        
    }
}