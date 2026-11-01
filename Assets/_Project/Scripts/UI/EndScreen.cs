using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        panel.SetActive(false);
        restartButton.onClick.AddListener(OnRestartClicked);
    }

    public void Show(bool won, string message)
    {
        titleText.text = won ? "YOU WIN!" : "GAME OVER";
        titleText.color = won ? new Color(0.2f, 0.8f, 0.4f) : new Color(0.9f, 0.3f, 0.3f);
        messageText.text = message;
        panel.SetActive(true);
    }

    private void OnRestartClicked()
    {
        GameManager.Instance.Restart();
    }
}