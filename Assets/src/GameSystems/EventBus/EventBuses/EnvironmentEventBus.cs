using UnityEngine;

public static class EnvironmentEventBus
{
    public static readonly CustomAction OnMouseEnterScreen = new CustomAction();
    // public static readonly CustomAction OnMouseDownScreen = new CustomAction();
    public static readonly CustomAction OnMouseExitScreen = new CustomAction();

    public static readonly CustomAction OnUnpause = new CustomAction();

    public static readonly CustomAction<GameObject> OnChunkInstantiate = new CustomAction<GameObject>();
    public static readonly CustomAction OnChunkDestroy = new CustomAction();
    
    public static readonly CustomAction OnStoneCollideObstacle = new CustomAction();
    public static readonly CustomAction OnStoneCollideObstacleWithShield = new CustomAction();
    public static readonly CustomAction<Vector3, int> OnStoneCollideObstacleAnnihilation = new CustomAction<Vector3, int>();
    public static readonly CustomAction<int> OnFlyingScoreReachCurrentScore = new CustomAction<int>();

    public static readonly CustomAction OnGameStart = new CustomAction();
    public static readonly CustomAction OnDefaultRunning = new CustomAction();
    public static readonly CustomAction OnAnnihilationRunning = new CustomAction();
    public static readonly CustomAction OnPause = new CustomAction();
    public static readonly CustomAction OnGameOver = new CustomAction();

    public static readonly CustomAction OnGameSceneLoad = new CustomAction();
    public static readonly CustomAction OnStoneMove = new CustomAction();
    public static readonly CustomAction OnPowerUpPickUp = new CustomAction();
    public static readonly CustomAction OnStoneCollideWall = new CustomAction();
    
    public static readonly CustomAction OnShieldPickUp = new CustomAction();
    public static readonly CustomAction OnScoreBoosterPickUp = new CustomAction();
    
    public static readonly CustomAction<float> OnScoreChange = new CustomAction<float>();
}