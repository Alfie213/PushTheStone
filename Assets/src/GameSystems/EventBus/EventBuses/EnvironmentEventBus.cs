public static class EnvironmentEventBus
{
    public static readonly CustomAction OnMouseEnterScreen = new CustomAction();
    public static readonly CustomAction OnMouseDownScreen = new CustomAction();
    public static readonly CustomAction OnMouseExitScreen = new CustomAction();

    public static readonly CustomAction OnChunkDestroy = new CustomAction();
    
    public static readonly CustomAction OnStoneCollidedObstacle = new CustomAction();

    public static readonly CustomAction OnPause = new CustomAction();
    public static readonly CustomAction OnRunning = new CustomAction();
    public static readonly CustomAction OnGameOver = new CustomAction();
}