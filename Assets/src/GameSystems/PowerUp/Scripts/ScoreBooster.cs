public class ScoreBooster : PowerUpBase
{
    protected override void PowerUp()
    {
        EnvironmentEventBus.OnScoreBoosterPickUp.Publish();
    }
}
