using System.Collections;
using UnityEngine;

// Quả bóng của người chơi: chỉ lo di chuyển và trạng thái của chính nó.
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float stopSpeed = 0.15f;   // chậm hơn mức này coi như đứng yên
    [SerializeField] private float fallDuration = 0.4f; // thời gian hiệu ứng rơi xuống lỗ

    [SerializeField] private Rigidbody2D rb;

    // Đóng gói: lớp khác chỉ được ĐỌC, chỉ Ball được GHI.
    public bool HasKey { get; private set; }

    public bool IsMoving => rb.simulated && rb.linearVelocity.magnitude > stopSpeed;

    // private void Awake()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    // }

    private void FixedUpdate()
    {
        // Bóng đã rất chậm thì cho dừng hẳn, để người chơi bắn tiếp được.
        if (rb.simulated && !IsMoving)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    public void Launch(Vector2 velocity)
    {
        rb.linearVelocity = velocity;
    }

    public void CollectKey()
    {
        HasKey = true;
    }

    public void UseKey()
    {
        HasKey = false;
    }

    // Rơi vào lỗ: tắt vật lý, trượt về tâm lỗ và thu nhỏ dần.
    public void FallInto(Vector3 holeCenter)
    {
        StopPhysics();
        StartCoroutine(FallRoutine(holeCenter));
    }

    // Biến mất ngay (dùng khi chạm laser).
    public void Disappear()
    {
        StopPhysics();
        gameObject.SetActive(false);
    }

    private void StopPhysics()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.simulated = false;
    }

    private IEnumerator FallRoutine(Vector3 target)
    {
        Vector3 startPos = transform.position;
        Vector3 startScale = transform.localScale;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / fallDuration;
            transform.position = Vector3.Lerp(startPos, target, t);
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);
            yield return null;
        }
    }
}