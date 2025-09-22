using System.Collections.Generic;
using UnityEngine;

public class InputHandlerMainScene : InputHandler
{
    public override void Initialize()
    {
        keyActions = new Dictionary<KeyCode, IKeyAction>
        {
            { KeyCode.UpArrow, new PlayerSighUp() },
            { KeyCode.DownArrow, new PlayerSightDown() },
            { KeyCode.LeftArrow, new PlayerSightLeft() },
            { KeyCode.RightArrow, new PlayerSightRight() },
            { KeyCode.Escape, new EscapeAction() },
            {KeyCode.W, new PlayerMoveUp() },
            {KeyCode.S, new PlayerMoveDown() },
            {KeyCode.A, new PlayerMoveLeft() },
            {KeyCode.D, new PlayerMoveRight() }


        };
    }
}