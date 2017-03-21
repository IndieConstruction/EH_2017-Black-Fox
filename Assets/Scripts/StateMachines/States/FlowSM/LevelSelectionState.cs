using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox
{
    public class LevelSelectionState : StateBase
    {
        Object canvasLevelSelection;

        public override void OnStart()
        {
            Debug.Log("LevelSelectionState");
            canvasLevelSelection = GameObject.Instantiate(Resources.Load("Prefabs/UI/CanvasLevelSelection"));
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (OnStateEnd != null)
                    OnStateEnd();
            }
        }

        public override void OnEnd()
        {
            GameObject.Destroy(canvasLevelSelection);
        }
    }
}