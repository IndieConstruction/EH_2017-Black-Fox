using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour
{
    /*
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    Player player;


    // Controller Axis
    //Left Stick
    float thumbStickLeftX;
    float thumbStickLeftY;
    //Right Stick
    float thumbStickRightX;
    float thumbStickRightY;
    //Triggers
    float triggerLeft;
    float triggerRight;

    void Start()
    {
        // assegno a playerIndex il numero del giocatore da 0 a 3
        player = GetComponent<Player>();
        playerIndex = (PlayerIndex)player.PlayerNumber;
    }

    void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);


        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            // Action button A
        }
        if (prevState.Buttons.B == ButtonState.Released && state.Buttons.Back == ButtonState.Pressed)
        {
            // Action button B
        }
        if (prevState.Buttons.Y == ButtonState.Released && state.Buttons.Y == ButtonState.Pressed)
        {
            // Action button Y
        }
        if (prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed)
        {
            // Action button X
        }


        if (prevState.Buttons.LeftShoulder == ButtonState.Released && state.Buttons.LeftShoulder == ButtonState.Pressed)
        {
            // Action button LeftShoulder
        }
        if (prevState.Buttons.RightShoulder == ButtonState.Released && state.Buttons.RightShoulder == ButtonState.Pressed)
        {
            // Action button RightShoulder
        }


        if (prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed)
        {
            // Action button Start
        }
        if (prevState.Buttons.Back == ButtonState.Released && state.Buttons.Back == ButtonState.Pressed)
        {
            // Action button Back
        }

    }

    void OnGUI()
    {
        // test stato
        //string text = "Player Number : " + player.PlayerNumber + " \n";
        //text += string.Format("IsConnected {0} Packet #{1}\n", state.IsConnected, state.PacketNumber);
        //text += string.Format("\tTriggers {0} {1}\n", state.Triggers.Left, state.Triggers.Right);
        //text += string.Format("\tD-Pad {0} {1} {2} {3}\n", state.DPad.Up, state.DPad.Right, state.DPad.Down, state.DPad.Left);
        //text += string.Format("\tButtons Start {0} Back {1} Guide {2}\n", state.Buttons.Start, state.Buttons.Back, state.Buttons.Guide);
        //text += string.Format("\tButtons LeftStick {0} RightStick {1} LeftShoulder {2} RightShoulder {3}\n", state.Buttons.LeftStick, state.Buttons.RightStick, state.Buttons.LeftShoulder, state.Buttons.RightShoulder);
        //text += string.Format("\tButtons A {0} B {1} X {2} Y {3}\n", state.Buttons.A, state.Buttons.B, state.Buttons.X, state.Buttons.Y);
        //text += string.Format("\tSticks Left {0} {1} Right {2} {3}\n", state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y, state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
        //GUI.Label(new Rect(0, player.PlayerNumber*50, Screen.width, Screen.height), text);
    }*/
}
