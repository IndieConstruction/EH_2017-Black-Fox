using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class ShowRoom : MonoBehaviour
    {
        List<AvatarData> avatars;
        Transform currentModel;
        Transform nextModel;
        Transform prevModel;

        public void Init(AvatarData[] _data)
        {
            foreach (AvatarData avatar in _data)
            {
                avatars.Add(avatar);
            }
            
        }

    }
}
