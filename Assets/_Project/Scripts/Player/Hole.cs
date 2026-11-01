public class Hole : Interactable
{
    protected override void OnBallEnter(Ball ball)
    {
        ball.FallInto(transform.position);
        HandleResult();
    }

    protected virtual void HandleResult()
    {
        GameManager.Instance.Lose("The ball fell into a hole.");
    }
}