using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    bool Pressed = false;
    JoyStick Stick;

    // Use this for initialization
    void Start()
    {
        IsPlayer = true;
        Stick = JoyStick.Instance;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(Pressed == true && Stick.IsPressed == false)
        {
            AI.ClearAI();

            AI.AutoMode = EAutoMode.Auto_On;
            AI.AddNextAI(EStateType.State_Idle);

            Pressed = false;
        }

        if (Stick.IsPressed)
        {
            Pressed = Stick.IsPressed;

            Vector3 movePosition = transform.position;
            movePosition += new Vector3(Stick.Axis.x, 0, Stick.Axis.y);

            AI.AutoMode = EAutoMode.Auto_Off;

            AI.ClearAI(EStateType.State_Walk);
            AI.AddNextAI(EStateType.State_Walk, null, movePosition);
        }

        base.Update();

    }
}
