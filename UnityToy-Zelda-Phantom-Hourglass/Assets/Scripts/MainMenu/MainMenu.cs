using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToScene(int sceneIndex)
    {
        Debug.Log("Clicou em trocar de cena");
        if (sceneIndex < 0) {
            Debug.Log("Nome da cena está vazio");
        }

        SceneManager.LoadScene(1);
    }


    public void QuitApp()
    {
        Debug.Log("Clicou em fechar o jogo");
        Application.Quit();
    }
}
