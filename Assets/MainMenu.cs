using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Gọi khi bấm nút Play
    public void PlayGame()
    {
        // Load scene tiếp theo trong build index, hoặc bạn có thể dùng tên cụ thể
        SceneManager.LoadScene("Game"); // hoặc SceneManager.LoadScene(1);
    }

    // Gọi khi bấm nút Exit
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit(); // Không hoạt động trong editor
    }
}
