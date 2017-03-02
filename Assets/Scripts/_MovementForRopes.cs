using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class _MovementForRopes : MovementController {

    Rigidbody rigid;
    PlayerIndex playerIndex = PlayerIndex.One;
    GamePadState state;
    public bool Keyboard;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Keyboard)
        {
            rigid.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * MovmentSpeed, ForceMode.Force);
            rigid.AddRelativeTorque(Vector3.up * RotationSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, ForceMode.Force);
        }
        else
        {
            state = GamePad.GetState(playerIndex);
            rigid.AddRelativeForce(Vector3.forward * state.Triggers.Right * MovmentSpeed, ForceMode.Force);
            rigid.AddRelativeTorque(Vector3.up * RotationSpeed * state.ThumbSticks.Left.X * Time.deltaTime, ForceMode.Force);
        }

    }
}
