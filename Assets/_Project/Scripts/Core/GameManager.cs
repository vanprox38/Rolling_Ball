using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Playing, Won, Lost }

public class GameManager : MonoBehaviour
{
    // Singleton: cả game chỉ có một GameManager, gọi qua GameManager.Instance.
    public static GameManager Instance { get; private set; }

    [SerializeField] private EndScreen endScreen;
    [SerializeField] private float endScreenDelay = 0.6f; // chờ hiệu ứng bóng rơi

    public GameState State { get; private set; } = GameState.Playing;
    public bool IsPlaying => State == GameState.Playing;

    private void Awake()
    {
        Instance = this;
    }

    public void Win()
    {
        EndGame(GameState.Won, "You reached the goal!");
    }

    public void Lose(string reason)
    {
        EndGame(GameState.Lost, reason);
    }

    private void EndGame(GameState result, string message)
    {
        if (!IsPlaying) return; // ván chơi chỉ kết thúc một lần

        State = result;
        StartCoroutine(ShowEndScreenAfterDelay(result == GameState.Won, message));
    }

    private IEnumerator ShowEndScreenAfterDelay(bool won, string message)
    {
        yield return new WaitForSeconds(endScreenDelay);
        endScreen.Show(won, message);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}