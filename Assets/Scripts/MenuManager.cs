using UnityEngine;
using UnityEngine.SceneManagement; // Essencial para trocar de cena

public class MenuManager : MonoBehaviour
{
    // Esta função vai aparecer no botão se o script estiver no objeto da cena
    public void IniciarJogo()
    {
        // Carrega a cena chamada "jogo"
        SceneManager.LoadScene("jogo");
    }

    public void SairDoJogo()
    {
        Debug.Log("Saiu do Jogo");
        Application.Quit();
    }
}