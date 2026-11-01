using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null && ball.HasKey)
        {
            ball.UseKey();
            Open();
        }
    }

    private void Open()
    {
        Destroy(gameObject);
    }
}