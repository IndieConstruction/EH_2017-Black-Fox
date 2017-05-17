using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public class ShipChanger : MonoBehaviour {

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                GameManager.Instance.PlayerMng.Players[0].AvatarData = Instantiate(Resources.Load("ShipModels/Bull") as AvatarData);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                GameManager.Instance.PlayerMng.Players[1].AvatarData = Instantiate(Resources.Load("ShipModels/Hummingbird") as AvatarData);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                GameManager.Instance.PlayerMng.Players[2].AvatarData = Instantiate(Resources.Load("ShipModels/Shark") as AvatarData);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                GameManager.Instance.PlayerMng.Players[3].AvatarData = Instantiate(Resources.Load("ShipModels/Owl") as AvatarData);
        }
    }
}
