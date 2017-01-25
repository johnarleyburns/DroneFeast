using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.EditorExt
{
    /// <summary>Editor component for adding the various prefabs.</summary>
    public class RTVoiceMenu
    {

        [MenuItem("Tools/RTVoice/Add/RTVoice", false, EditorHelper.MENU_ID + 20)]
        private static void AddRTVoice()
        {
            EditorHelper.AddRTVoice();
        }

        [MenuItem("Tools/RTVoice/Add/SpeechText", false, EditorHelper.MENU_ID + 40)]
        private static void AddSpeechText()
        {
            PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets" + Util.Constants.PREFAB_PATH + "SpeechText.prefab", typeof(GameObject)));
        }

        [MenuItem("Tools/RTVoice/Add/Sequencer", false, EditorHelper.MENU_ID + 50)]
        private static void AddSequencer()
        {
            PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets" + Util.Constants.PREFAB_PATH + "Sequencer.prefab", typeof(GameObject)));
        }

        [MenuItem("Tools/RTVoice/Add/TextFileSpeaker", false, EditorHelper.MENU_ID + 60)]
        private static void AddTextFileSpeaker()
        {
            PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets" + Util.Constants.PREFAB_PATH + "TextFileSpeaker.prefab", typeof(GameObject)));
        }

        [MenuItem("Tools/RTVoice/Add/Loudspeaker", false, EditorHelper.MENU_ID + 80)]
        private static void AddLoudspeaker()
        {
            PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets" + Util.Constants.PREFAB_PATH + "Loudspeaker.prefab", typeof(GameObject)));
        }

        [MenuItem("Tools/RTVoice/Help/Manual", false, EditorHelper.MENU_ID + 200)]
        private static void ShowManual()
        {
            Application.OpenURL(Util.Constants.ASSET_MANUAL_URL);
        }

        [MenuItem("Tools/RTVoice/Help/API", false, EditorHelper.MENU_ID + 210)]
        private static void ShowAPI()
        {
            Application.OpenURL(Util.Constants.ASSET_API_URL);
        }

        [MenuItem("Tools/RTVoice/Help/Forum", false, EditorHelper.MENU_ID + 220)]
        private static void ShowForum()
        {
            Application.OpenURL(Util.Constants.ASSET_FORUM_URL);
        }

        [MenuItem("Tools/RTVoice/About/Unity AssetStore", false, EditorHelper.MENU_ID + 300)]
        private static void ShowUAS()
        {
            Application.OpenURL(Util.Constants.ASSET_URL);
        }

        [MenuItem("Tools/RTVoice/About/Product", false, EditorHelper.MENU_ID + 310)]
        private static void ShowProduct()
        {
            Application.OpenURL(Util.Constants.ASSET_CT_URL);
        }

        [MenuItem("Tools/RTVoice/About/" + Util.Constants.ASSET_AUTHOR, false, EditorHelper.MENU_ID + 320)]
        private static void ShowCT()
        {
            Application.OpenURL(Util.Constants.ASSET_AUTHOR_URL);
        }

        [MenuItem("Tools/RTVoice/About/Info", false, EditorHelper.MENU_ID + 340)]
        private static void ShowInfo()
        {
            EditorUtility.DisplayDialog(Util.Constants.ASSET_NAME,
               "Version: " + Util.Constants.ASSET_VERSION +
               System.Environment.NewLine +
               System.Environment.NewLine +
               "© 2015-2017 by " + Util.Constants.ASSET_AUTHOR +
               System.Environment.NewLine +
               System.Environment.NewLine +
               Util.Constants.ASSET_AUTHOR_URL +
               System.Environment.NewLine +
               Util.Constants.ASSET_URL +
               System.Environment.NewLine, "Ok");
        }
    }
}
// Copyright 2015-2017 www.crosstales.com