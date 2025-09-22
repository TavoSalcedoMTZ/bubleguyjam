using UnityEngine;


public class PlayerSighUp : IKeyAction
{
    public void Execute()
    {
        PlayerController.Instance.playerSight.SetSight(ShotSight.Up);

    }
}

public class PlayerSightDown : IKeyAction
{
    public void Execute()
    {
        PlayerController.Instance.playerSight.SetSight(ShotSight.Down);
    }
}

public class PlayerSightLeft : IKeyAction
{
    public void Execute()
    {
        PlayerController.Instance.playerSight.SetSight(ShotSight.Left);
    }
}

public class PlayerSightRight : IKeyAction
{
    public void Execute()
    {
        PlayerController.Instance.playerSight.SetSight(ShotSight.Right );
    }
}

public class EscapeAction : IKeyAction
{
    public void Execute()
    {
        Debug.Log("Escape Action Executed");
        Application.Quit();
    }
}

public class PlayerMoveUp : IKeyAction
{
    public void Execute()
    {
       // PlayerController.Instance.playerMovement.Move(ShotSight.Up);
    }
}

public class PlayerMoveDown : IKeyAction
{
    public void Execute()
    {
       // PlayerController.Instance.playerMovement.Move(ShotSight.Down);
    }
}

public class PlayerMoveLeft : IKeyAction
{
    public void Execute()
    {
      //  PlayerController.Instance.playerMovement.Move(ShotSight.Left);
    }
}

public class PlayerMoveRight : IKeyAction
{
    public void Execute()
    {
       // PlayerController.Instance.playerMovement.Move(ShotSight.Right);
    }
}
