using UnityEngine;
using System.Collections.Generic;

namespace Crosstales.RTVoice.Demo.Util
{
    /// <summary>Enables game objects for a given platform.</summary>
    public class PlatformEnabler : MonoBehaviour
    {

        #region Variables

        public List<Platform> EnabledPlatforms;
        public GameObject[] Objects;

        private Platform currentPlatform;

        #endregion

        #region MonoBehaviour methods

        void Start()
        {
            if (RTVoice.Util.Helper.isWindowsPlatform)
            {
                currentPlatform = Platform.Windows;
            }
            else if (RTVoice.Util.Helper.isMacOSPlatform)
            {
                currentPlatform = Platform.OSX;
            }
            else if (RTVoice.Util.Helper.isAndroidPlatform)
            {
                currentPlatform = Platform.Android;
            }
            else if (RTVoice.Util.Helper.isIOSPlatform)
            {
                currentPlatform = Platform.IOS;
            }
            else
            {
                currentPlatform = Platform.Unsupported;
            }
        }

        void Update()
        {

            foreach (GameObject go in Objects)
            {
                go.SetActive(EnabledPlatforms.Contains(currentPlatform));
            }
        }
    }

    #endregion

    #region Enumeration

    /// <summary>All available platforms.</summary>
    public enum Platform
    {
        OSX,
        Windows,
        IOS,
        Android,
        WSA,
        Unsupported
    }
    #endregion
}
// Copyright 2016-2017 www.crosstales.com