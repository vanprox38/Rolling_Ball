using UnityEngine;
using UnityEngine.InputSystem;

// Đọc chuột, vẽ đường ngắm và ra lệnh cho Ball bắn đi.
public class BallLauncher : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private LineRenderer aimLine;
    [SerializeField] private float power = 4f;           // vận tốc cho mỗi đơn vị kéo
    [SerializeField] private float maxDragDistance = 3f; // kéo xa hơn cũng không mạnh hơn

    private Camera cam;
    private Vector2 dragStart;
    private bool isAiming;

    private void Awake()
    {
        cam = Camera.main;
        aimLine.positionCount = 2;
        aimLine.enabled = false;
    }

    private void Update()
    {
        // Hết ván hoặc bóng đang lăn thì không cho ngắm.
        if (!GameManager.Instance.IsPlaying || ball.IsMoving)
        {
            StopAiming();
            return;
        }

        Mouse mouse = Mouse.current;
        if (mouse == null) return;

        Vector2 mouseWorld = cam.ScreenToWorldPoint(mouse.position.ReadValue());

        if (mouse.leftButton.wasPressedThisFrame)
        {
            isAiming = true;
            dragStart = mouseWorld;
            aimLine.enabled = true;
        }

        if (!isAiming) return;

        // Kéo ngược hướng muốn bắn, giống kéo dây ná.
        Vector2 drag = Vector2.ClampMagnitude(dragStart - mouseWorld, maxDragDistance);
        Vector2 ballPos = ball.transform.position;
        aimLine.SetPosition(0, ballPos);
        aimLine.SetPosition(1, ballPos + drag);

        if (mouse.leftButton.wasReleasedThisFrame)
        {
            ball.Launch(drag * power);
            StopAiming();
        }
    }

    private void StopAiming()
    {
        isAiming = false;
        aimLine.enabled = false;
    }
}