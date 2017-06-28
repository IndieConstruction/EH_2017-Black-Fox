using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    [CreateAssetMenu(fileName = "MenuAudioData", menuName = "Audio/NewMenuAudioData", order = 2)]
    public class MenuAudioData : ScriptableObject
    {
        public AudioParameter MenuMovementAudio;
        public AudioParameter MenuSelectionAudio;
        public AudioParameter MenuGoBackAudio;
        public AudioParameter CountDownAudio;
        public AudioParameter GameplayMusic;
        public AudioParameter MenuMusic;
    }
}
