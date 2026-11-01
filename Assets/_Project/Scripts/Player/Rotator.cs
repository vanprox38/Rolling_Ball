using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rotator : MonoBehaviour
{
    [SerializeField] private float degreesPerSecond = 60f; // số âm = quay ngược chiều

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.MoveRotation(rb.rotation + degreesPerSecond * Time.fixedDeltaTime);
    }
}