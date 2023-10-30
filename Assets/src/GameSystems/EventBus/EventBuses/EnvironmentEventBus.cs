using UnityEngine;

public static class EnvironmentEventBus
{
    public static readonly CustomAction OnMouseEnterScreen = new CustomAction();
    // public static readonly CustomAction OnMouseDownScreen = new CustomAction();
    public static readonly CustomAction OnMouseExitScreen = new CustomAction();

    public static readonly CustomAction OnPauseUIClick = new CustomAction();

    public static readonly CustomAction<GameObject> OnChunkInstantiate = new CustomAction<GameObject>();
    public static readonly CustomAction OnChunkDestroy = new CustomAction();
    
    public static readonly CustomAction OnStoneCollidedObstacle = new CustomAction();

    public static readonly CustomAction OnPause = new CustomAction();
    public static readonly CustomAction OnRunning = new CustomAction();
    public static readonly CustomAction OnGameOver = new CustomAction();

    public static readonly CustomAction OnGameSceneLoad = new CustomAction();
    public static readonly CustomAction OnStoneMove = new CustomAction();
    public static readonly CustomAction OnPowerUpPickUp = new CustomAction();
    public static readonly CustomAction OnStoneCollideWall = new CustomAction();
}