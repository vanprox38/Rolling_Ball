public class GoalHole : Hole
{
    protected override void HandleResult()
    {
        GameManager.Instance.Win();
    }
}