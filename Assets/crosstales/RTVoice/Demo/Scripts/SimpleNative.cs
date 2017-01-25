using UnityEngine;
using UnityEngine.UI;
using System;
using Crosstales.RTVoice.Model.Event;

namespace Crosstales.RTVoice.Demo
{
    /// <summary>Simple native TTS example.</summary>
    public class SimpleNative : MonoBehaviour
    {

        #region Variables

        public Text TextSpeakerA;
        public Text TextSpeakerB;
        public Text TextSpeakerC;

        public Text PhonemeSpeakerA;
        public Text PhonemeSpeakerB;
        public Text PhonemeSpeakerC;

        public Text VisemeSpeakerA;
        public Text VisemeSpeakerB;
        public Text VisemeSpeakerC;

        [Range(0f, 3f)]
        public float RateSpeakerA = 1.25f;
        [Range(0f, 3f)]
        public float RateSpeakerB = 1.75f;
        [Range(0f, 3f)]
        public float RateSpeakerC = 2.5f;

        public bool PlayOnStart = false;

        private Guid uidSpeakerA;
        private Guid uidSpeakerB;
        private Guid uidSpeakerC;

        private string textA;
        private string textB;
        private string textC;

        #endregion

        #region MonoBehaviour methods

        void Start()
        {
            // Subscribe event listeners
            Speaker.OnSpeakNativeCurrentWord += speakCurrentWordMethod;
            Speaker.OnSpeakNativeCurrentPhoneme += speakCurrentPhonemeMethod;
            Speaker.OnSpeakNativeCurrentViseme += speakCurrentVisemeMethod;
            Speaker.OnSpeakStart += speakStartMethod;
            Speaker.OnSpeakComplete += speakCompleteMethod;

            textA = TextSpeakerA.text;
            textB = TextSpeakerB.text;
            textC = TextSpeakerC.text;

            if (PlayOnStart)
            {
                Play();
            }
        }

        void OnDestroy()
        {
            // Unsubscribe event listeners
            Speaker.OnSpeakNativeCurrentWord -= speakCurrentWordMethod;
            Speaker.OnSpeakNativeCurrentPhoneme -= speakCurrentPhonemeMethod;
            Speaker.OnSpeakNativeCurrentViseme -= speakCurrentVisemeMethod;
            Speaker.OnSpeakStart -= speakStartMethod;
            Speaker.OnSpeakComplete -= speakCompleteMethod;
        }

        #endregion

        #region Public methods

        public void Play()
        {
            TextSpeakerA.text = textA;
            TextSpeakerB.text = textB;
            TextSpeakerC.text = textC;

            SpeakerA(); //start with speaker A
        }

        public void SpeakerA()
        {
            uidSpeakerA = Speaker.SpeakNative(TextSpeakerA.text, Speaker.VoiceForCulture("en"), RateSpeakerA);
        }

        public void SpeakerB()
        {
            uidSpeakerB = Speaker.SpeakNative(TextSpeakerB.text, Speaker.VoiceForCulture("en", 1), RateSpeakerB);
        }

        public void SpeakerC()
        { //default voice
            uidSpeakerC = Speaker.SpeakNative(TextSpeakerC.text, Speaker.VoiceForCulture("en", 2), RateSpeakerC);
        }

        public void Silence()
        {
            Speaker.Silence();
            //Speaker.Silence(speakerC);

            TextSpeakerA.text = textA;
            TextSpeakerB.text = textB;
            TextSpeakerC.text = textC;

            VisemeSpeakerC.text = PhonemeSpeakerC.text = VisemeSpeakerB.text = PhonemeSpeakerB.text = VisemeSpeakerA.text = PhonemeSpeakerA.text = "-";
        }

        #endregion

        #region Callback methods

        private void speakStartMethod(object sender, SpeakEventArgs e)
        {
            if (e.Wrapper.Uid.Equals(uidSpeakerA))
            {
                Debug.Log("Speaker A - Speech start: " + e);
            }
            else if (e.Wrapper.Uid.Equals(uidSpeakerB))
            {
                Debug.Log("Speaker B - Speech start: " + e);
            }
            else if (e.Wrapper.Uid.Equals(uidSpeakerC))
            {
                Debug.Log("Speaker C - Speech start: " + e);
            }
            else
            {
                Debug.LogWarning("Unknown speaker: " + e);
            }
        }

        private void speakCompleteMethod(object sender, SpeakEventArgs e)
        {
            if (e.Wrapper.Uid.Equals(uidSpeakerA))
            {
                Debug.Log("Speaker A - Speech complete: " + e);
                TextSpeakerA.text = e.Wrapper.Text;
                VisemeSpeakerA.text = PhonemeSpeakerA.text = "-";
                SpeakerB();
            }
            else if (e.Wrapper.Uid.Equals(uidSpeakerB))
            {
                Debug.Log("Speaker B - Speech complete: " + e);
                TextSpeakerB.text = e.Wrapper.Text;
                VisemeSpeakerB.text = PhonemeSpeakerB.text = "-";

                SpeakerC();
                //Invoke("Silence", 3f);
            }
            else if (e.Wrapper.Uid.Equals(uidSpeakerC))
            {
                Debug.Log("Speaker C - Speech complete: " + e);
                TextSpeakerC.text = e.Wrapper.Text;
                VisemeSpeakerC.text = PhonemeSpeakerC.text = "-";
            }
            else
            {
                Debug.LogWarning("Unknown speaker: " + e);
            }
        }

        private void speakCurrentWordMethod(object sender, CurrentWordEventArgs e)
        {
            if (e.Wrapper.Uid.Equals(uidSpeakerA))
            {
                TextSpeakerA.text = RTVoice.Util.Helper.MarkSpokenText(e.SpeechTextArray, e.WordIndex);
            }
            else if (e.Wrapper.Uid.Equals(uidSpeakerB))
            {
                TextSpeakerB.text = RTVoice.Util.Helper.MarkSpokenText(e.SpeechTextArray, e.WordIndex);
            }
            else if (e.Wrapper.Uid.Equals(uidSpeakerC))
            {
                TextSpeakerC.text = RTVoice.Util.Helper.MarkSpokenText(e.SpeechTextArray, e.WordIndex);
            }
            else
            {
                Debug.LogWarning("Unknown speaker: " + e);
            }
        }

        private void speakCurrentPhonemeMethod(object sender, CurrentPhonemeEventArgs e)
        {
            if (e.Wrapper.Uid.Equals(uidSpeakerA))
            {
                PhonemeSpeakerA.text = e.Phoneme;
            }
            else if (e.Wrapper.Uid.Equals(uidSpeakerB))
            {
                PhonemeSpeakerB.text = e.Phoneme;
            }
            else if (e.Wrapper.Uid.Equals(uidSpeakerC))
            {
                PhonemeSpeakerC.text = e.Phoneme;
            }
            else
            {
                Debug.LogWarning("Unknown speaker: " + e);
            }
        }

        private void speakCurrentVisemeMethod(object sender, CurrentVisemeEventArgs e)
        {
            if (e.Wrapper.Uid.Equals(uidSpeakerA))
            {
                VisemeSpeakerA.text = e.Viseme;
            }
            else if (e.Wrapper.Uid.Equals(uidSpeakerB))
            {
                VisemeSpeakerB.text = e.Viseme;
            }
            else if (e.Wrapper.Uid.Equals(uidSpeakerC))
            {
                VisemeSpeakerC.text = e.Viseme;
            }
            else
            {
                Debug.LogWarning("Unknown speaker: " + e);
            }
        }

        #endregion
    }
}
// Copyright 2015-2017 www.crosstales.com