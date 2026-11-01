using System.Collections;
using UnityEngine;

public class Laser : Interactable
{
    [SerializeField] private float onDuration = 2f;
    [SerializeField] private float offDuration = 1.5f;

    private SpriteRenderer spriteRenderer;
    private Collider2D beamCollider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        beamCollider = GetComponent<Collider2D>();
    }

    // Start có thể là coroutine: vòng lặp chạy suốt ván chơi.
    private IEnumerator Start()
    {
        while (true)
        {
            SetBeam(true);
            yield return new WaitForSeconds(onDuration);
            SetBeam(false);
            yield return new WaitForSeconds(offDuration);
        }
    }

    private void SetBeam(bool isOn)
    {
        spriteRenderer.enabled = isOn;
        beamCollider.enabled = isOn;
    }

    protected override void OnBallEnter(Ball ball)
    {
        ball.Disappear();
        GameManager.Instance.Lose("The ball hit the laser.");
    }
}