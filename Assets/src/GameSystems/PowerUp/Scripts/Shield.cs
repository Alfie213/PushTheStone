public class Shield : PowerUpBase
{
    protected override void PowerUp()
    {
        EnvironmentEventBus.OnShieldPickUp.Publish();
    }
}
