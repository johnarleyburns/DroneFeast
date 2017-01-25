﻿using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.EditorExt
{
    /// <summary>Base class for editor windows.</summary>
    public abstract class ConfigBase : EditorWindow
    {

        #region Variables

        protected static string updateText = UpdateCheck.TEXT_NOT_CHECKED;

        private static System.Threading.Thread worker;

        #endregion

        #region Protected methods

        protected static void showConfiguration()
        {
            GUILayout.Label("General Settings", EditorStyles.boldLabel);

            Util.Constants.ASSET_PATH = EditorGUILayout.TextField(new GUIContent("Asset Path", "Path to the asset inside the Unity-project (default: " + Util.Constants.DEFAULT_ASSET_PATH + ")."), Util.Constants.ASSET_PATH);

            Util.Constants.DEBUG = EditorGUILayout.Toggle(new GUIContent("Debug", "Enable or disable debug logs (default: off)."), Util.Constants.DEBUG);

            Util.Constants.UPDATE_CHECK = EditorGUILayout.BeginToggleGroup(new GUIContent("Update check", "Enable or disable the update-check (default: on)."), Util.Constants.UPDATE_CHECK);
            EditorGUI.indentLevel++;
            Util.Constants.UPDATE_OPEN_UAS = EditorGUILayout.Toggle(new GUIContent("Open UAS-site", "Automatically opens the direct link to 'Unity AssetStore' if an update was found (default: off)."), Util.Constants.UPDATE_OPEN_UAS);
            EditorGUI.indentLevel--;
            EditorGUILayout.EndToggleGroup();

            //Constants.DONT_DESTROY_ON_LOAD = EditorGUILayout.Toggle(new GUIContent("Don't destroy on load", "Don't destroy RTVoice during scene switches (default: on, off is NOT RECOMMENDED!)."), Constants.DONT_DESTROY_ON_LOAD);
            Util.Constants.PREFAB_AUTOLOAD = EditorGUILayout.Toggle(new GUIContent("Prefab auto-load", "Enable or disable auto-loading of the prefabs to the scene (default: on)."), Util.Constants.PREFAB_AUTOLOAD);

            Util.Constants.AUDIOFILE_PATH = EditorGUILayout.TextField(new GUIContent("Audio path", "Path to the generated audio files (default: " + Util.Constants.DEFAULT_AUDIOFILE_PATH + ")."), Util.Constants.AUDIOFILE_PATH);
            Util.Constants.AUDIOFILE_AUTOMATIC_DELETE = EditorGUILayout.Toggle(new GUIContent("Audio auto-delete", "Enable or disable auto-delete of the generated audio files (default: on)."), Util.Constants.AUDIOFILE_AUTOMATIC_DELETE);

            EditorHelper.SeparatorUI();
            GUILayout.Label("Windows Settings", EditorStyles.boldLabel);
            Util.Constants.ENFORCE_32BIT_WINDOWS = EditorGUILayout.Toggle(new GUIContent("Enforce 32bit voices", "Enforce 32bit versions of voices under Windows (default: off)."), Util.Constants.ENFORCE_32BIT_WINDOWS);

            //EditorHelper.SeparatorUI();
            //GUILayout.Label("macOS Settings", EditorStyles.boldLabel);
            //Constants.TTS_MACOS = EditorGUILayout.TextField(new GUIContent("TTS-command", "TTS-command under macOS (default: " + Constants.DEFAULT_TTS_MACOS + ")."), Constants.TTS_MACOS);
        }

        protected static void showAbout()
        {
            GUILayout.Label(Util.Constants.ASSET_NAME, EditorStyles.boldLabel);
            GUILayout.Label("Version:\t" + Util.Constants.ASSET_VERSION);

            GUILayout.Space(6);
            GUILayout.Label("Web:\t" + Util.Constants.ASSET_AUTHOR_URL);
            GUILayout.Label("Email:\t" + Util.Constants.ASSET_CONTACT);

            GUILayout.Space(12);
            GUILayout.Label("© 2015-2017 by " + Util.Constants.ASSET_AUTHOR);

            EditorHelper.SeparatorUI();

            if (worker == null || (worker != null && !worker.IsAlive))
            {
                if (GUILayout.Button(new GUIContent("Check for update", "Checks for available updates of " + Util.Constants.ASSET_NAME)))
                {

                    worker = new System.Threading.Thread(() => UpdateCheck.UpdateCheckForEditor(out updateText));
                    worker.Start();
                }
            }
            else
            {
                GUILayout.Label("Checking... Please wait.", EditorStyles.boldLabel);
            }

            Color fgColor = GUI.color;

            if (updateText.Equals(UpdateCheck.TEXT_NOT_CHECKED))
            {
                GUI.color = Color.cyan;
                GUILayout.Label(updateText);
            }
            else if (updateText.Equals(UpdateCheck.TEXT_NO_UPDATE))
            {
                GUI.color = Color.green;
                GUILayout.Label(updateText);
            }
            else
            {
                GUI.color = Color.yellow;
                GUILayout.Label(updateText);

                if (GUILayout.Button(new GUIContent("Download", "Opens the 'Unity AssetStore' for downloading the latest version.")))
                {
                    Application.OpenURL(Util.Constants.ASSET_URL);
                }
            }

            GUI.color = fgColor;

            EditorHelper.SeparatorUI();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(new GUIContent("Manual", "Opens the manual.")))
            {
                Application.OpenURL(Util.Constants.ASSET_MANUAL_URL);
            }
            if (GUILayout.Button(new GUIContent("API", "Opens the API.")))
            {
                Application.OpenURL(Util.Constants.ASSET_API_URL);
            }
            if (GUILayout.Button(new GUIContent("Forum", "Opens the forum page.")))
            {
                Application.OpenURL(Util.Constants.ASSET_FORUM_URL);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(new GUIContent("Unity AssetStore", "Opens the 'Unity AssetStore' page.")))
            {
                Application.OpenURL(Util.Constants.ASSET_URL);
            }

            if (GUILayout.Button(new GUIContent("Product", "Opens the product page.")))
            {
                Application.OpenURL(Util.Constants.ASSET_CT_URL);
            }

            if (GUILayout.Button(new GUIContent(Util.Constants.ASSET_AUTHOR, "Opens the 'crosstales' page.")))
            {
                Application.OpenURL(Util.Constants.ASSET_AUTHOR_URL);
            }
            EditorGUILayout.EndHorizontal();
        }

        protected static void save()
        {

            Util.Constants.Save();

            if (Util.Constants.DEBUG)
                Debug.Log("Config data saved");
        }

        #endregion
    }
}
// Copyright 2016-2017 www.crosstales.com