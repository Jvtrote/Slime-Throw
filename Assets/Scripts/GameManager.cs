using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject painelGameOver;
    public Rigidbody2D slimeRb;
    public TextMeshProUGUI textoDistanciaFinal;
    // Removi a referência ao outro script para evitar erros se você ainda não o configurou

    private bool jogoFinalizado = false;

    void Update()
    {
        // Trocamos 'velocity' por 'linearVelocity'
        // E verificamos se ele já foi lançado (gravityScale > 0)
        if (!jogoFinalizado && slimeRb.gravityScale > 0 && slimeRb.linearVelocity.magnitude < 0.1f)
        {
            FinalizarJogo();
        }
    }

    void FinalizarJogo()
    {
        jogoFinalizado = true;

        if (painelGameOver != null)
            painelGameOver.SetActive(true);

        Debug.Log("O Slime parou! Fim de jogo.");
    }

    // Métodos para os botões da UI
    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrParaMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}