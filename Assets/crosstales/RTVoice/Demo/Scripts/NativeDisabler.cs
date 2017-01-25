using UnityEngine;

namespace Crosstales.RTVoice.Demo
{
    public class NativeDisabler : MonoBehaviour
    {

        public GameObject[] Objects;

        void Update()
        {
            foreach (GameObject go in Objects)
            {
                go.SetActive(!GUISpeech.isNative);
            }
        }
    }
}
// Copyright 2016-2017 www.crosstales.com