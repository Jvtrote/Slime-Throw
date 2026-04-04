using UnityEngine;
using UnityEngine.SceneManagement; // Essencial para trocar de cena!

public class MenuManager : MonoBehaviour
{
    public void IniciarJogo()
    {
        // Troque "NomeDaSuaCena" pelo nome EXATO da sua cena de jogo
        SceneManager.LoadScene("NomeDaSuaCena");
    }

    public void SairDoJogo()
    {
        Debug.Log("O jogador saiu do jogo!");
        Application.Quit(); // Só funciona no jogo depois de exportado (Build)
    }
}