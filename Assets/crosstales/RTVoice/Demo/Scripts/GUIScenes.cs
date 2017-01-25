using UnityEngine;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace Crosstales.RTVoice.Demo
{
    /// <summary>Main GUI scene manager for all demo scenes.</summary>
    public class GUIScenes : MonoBehaviour
    {

        #region Variables

        [Tooltip("Name of the previous scene.")]
        public string PreviousScene;

        [Tooltip("Name of the next scene.")]
        public string NextScene;

        public void LoadPrevoiusScene()
        {
            Speaker.Silence();
#if UNITY_5_3_OR_NEWER
            SceneManager.LoadScene(PreviousScene);
#else
         Application.LoadLevel(PreviousScene);
#endif
        }

        #endregion

        #region Public methods

        public void LoadNextScene()
        {
            Speaker.Silence();
#if UNITY_5_3_OR_NEWER
            SceneManager.LoadScene(NextScene);
#else
         Application.LoadLevel(NextScene);
#endif
        }

        #endregion
    }
}
// Copyright 2015-2017 www.crosstales.com