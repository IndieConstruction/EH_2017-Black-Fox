using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BlackFox {
    [CreateAssetMenu(fileName = "AvatarData", menuName = "Avatar/NewShipAudio", order = 3)]
    public class ShipAudioData : ScriptableObject
    {
        public AudioClip PinPlaced;
        public AudioClip Death;
        public AudioClip Movement;
        public AudioClip NoAmmo;
        public List<AudioClip> Shoots;
        public List<AudioClip> Collisions;
    }
}