using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.EditorExt
{
    /// <summary>Loads the configuration of the asset.</summary>
    [InitializeOnLoad]
    public static class ConfigLoader
    {

        #region Constructor

        static ConfigLoader()
        {
            Util.Constants.Load();

            if (Util.Constants.DEBUG)
                Debug.Log("Config data loaded");
        }

        #endregion
    }
}
// Copyright 2016-2017 www.crosstales.com