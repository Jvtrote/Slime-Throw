using UnityEngine;
using UnityEngine.SceneManagement; // Essencial para reiniciar cenas

public class GameManager : MonoBehaviour
{
    [Header("Configurações")]
    public GameObject painelGameOver; // Arraste o Painel aqui
    public Rigidbody2D slimeRb;      // Arraste o Slime aqui

    private bool jogoFinalizado = false;

    void Update()
    {
        // 1. Verificamos se o Slime já foi lançado (gravity > 0)
        // 2. Verificamos se a velocidade (linearVelocity) é quase zero
        // 3. Verificamos se já não finalizamos o jogo antes
        if (!jogoFinalizado && slimeRb.gravityScale > 0 && slimeRb.linearVelocity.magnitude < 0.1f)
        {
            MostrarMenuDeRestart();
        }
    }

    void MostrarMenuDeRestart()
    {
        jogoFinalizado = true;
        if (painelGameOver != null)
        {
            painelGameOver.SetActive(true); // Faz o painel aparecer na tela
        }
    }

    // ESTA FUNÇÃO PRECISA SER PUBLIC PARA APARECER NO BOTÃO
    public void ReiniciarODogo()
    {
        // Carrega a cena atual novamente
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}