using UnityEngine;

public class PauseManager : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1f; // Đảm bảo game đang chạy khi scene bắt đầu
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            GameUIManager.Instance.TogglePause();
        }
    }
}
