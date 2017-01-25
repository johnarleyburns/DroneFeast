using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.EditorExt
{
    /// <summary>Custom editor for the 'Speaker'-class.</summary>
    [CustomEditor(typeof(Speaker))]
    public class SpeakerEditor : Editor
    {

        #region Variables

        private System.Collections.Generic.List<string> voices = new System.Collections.Generic.List<string>(50);
        private int voiceIndex;
        private float rate = 1f;
        private float volume = 1f;
        private Speaker script;

        private bool showVoices = false;

        #endregion

        #region Editor methods

        public void OnEnable()
        {
            script = (Speaker)target;

            loadVoices();
        }

        public void OnDisable()
        {
            if (Util.Helper.isEditorMode)
            {
                Speaker.Silence();
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorHelper.SeparatorUI();

            if (script.isActiveAndEnabled)
            {
                GUILayout.Label("Data", EditorStyles.boldLabel);

                showVoices = EditorGUILayout.Foldout(showVoices, "Voices (" + Speaker.Voices.Count + ")");
                if (showVoices)
                {
                    EditorGUI.indentLevel++;

                    foreach (Model.Voice voice in Speaker.Voices)
                    {
                        EditorGUILayout.SelectableLabel(voice.ToShortString());
                    }

                    EditorGUI.indentLevel--;
                }

                EditorHelper.SeparatorUI();

                if (voices.Count > 0)
                {
                    GUILayout.Label("Test-Drive", EditorStyles.boldLabel);

                    if (Util.Helper.isEditorMode)
                    {
                        voiceIndex = EditorGUILayout.Popup("Voice", voiceIndex, voices.ToArray());
                        rate = EditorGUILayout.Slider("Rate", rate, 0f, 3f);

                        if (Util.Helper.isWindowsPlatform)
                        {
                            volume = EditorGUILayout.Slider("Volume", volume, 0f, 1f);
                        }

                        GUILayout.Space(8);

                        if (GUILayout.Button(new GUIContent("Preview Voice", "Preview the selected voice.")))
                        {
                            Speaker.SpeakNativeInEditor("You have selected " + Speaker.Voices[voiceIndex].Name, Speaker.Voices[voiceIndex], rate, volume);
                        }

                        if (GUILayout.Button(new GUIContent("Silence", "Silence all active previews.")))
                        {
                            Speaker.Silence();
                        }
                    }
                    else
                    {
                        GUILayout.Label("Disabled in Play-mode!");
                    }
                }
                else
                {
                    loadVoices();
                }
            }
            else
            {
                GUILayout.Label("Script is disabled!", EditorStyles.boldLabel);
            }
        }

        #endregion

        #region Private methods

        private void loadVoices()
        {
            voices.Clear();

            for (int voiceNumber = 0; voiceNumber < Speaker.Voices.Count; voiceNumber++)
            {
                voices.Add(voiceNumber + ": " + Speaker.Voices[voiceNumber].Name);
            }
        }

        #endregion
    }
}
// Copyright 2016-2017 www.crosstales.com