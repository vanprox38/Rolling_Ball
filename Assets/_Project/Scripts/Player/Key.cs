public class Key : Interactable
{
    protected override void OnBallEnter(Ball ball)
    {
        ball.CollectKey();
        Destroy(gameObject);
    }
}