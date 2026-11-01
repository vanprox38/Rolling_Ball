using UnityEngine;

// Lớp cha trừu tượng: mọi vật mà bóng chạm vào thì có chuyện xảy ra.
// abstract = không gắn trực tiếp lên GameObject, chỉ dùng qua lớp con.
[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameManager.Instance.IsPlaying) return;

        Ball ball = other.GetComponent<Ball>();
        if (ball != null)
        {
            OnBallEnter(ball);
        }
    }

    // Mỗi lớp con BẮT BUỘC tự định nghĩa phản ứng của mình.
    protected abstract void OnBallEnter(Ball ball);
}