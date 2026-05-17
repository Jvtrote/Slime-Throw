using UnityEngine;
using TMPro; // Essencial para mexer com TextMeshPro

public class GerenciadorRecorde : MonoBehaviour
{
    public TextMeshProUGUI textoRecorde; // Arraste o seu Texto aqui no Inspector
    private int recordeAtual;

    void Start()
    {
        // O PlayerPrefs tenta carregar o recorde salvo. Se não existir nada, ele começa em 0.
        recordeAtual = PlayerPrefs.GetInt("RecordeMaximo", 0);

        // Atualiza o texto na tela logo que o jogo começa
        AtualizarTextoInterface();
    }

    // Chame essa função sempre que o jogador pontuar ou quando o jogo der Game Over
    public void VerificarEAtualizarRecorde(int pontuacaoDoJogador)
    {
        // Se a pontuação atual for MAIOR que o recorde guardado
        if (pontuacaoDoJogador > recordeAtual)
        {
            recordeAtual = pontuacaoDoJogador;

            // Grava o novo recorde permanentemente na memória do PC/Celular
            PlayerPrefs.SetInt("RecordeMaximo", recordeAtual);
            PlayerPrefs.Save(); // Garante a gravação dos dados

            AtualizarTextoInterface();
        }
    }

    void AtualizarTextoInterface()
    {
        if (textoRecorde != null)
        {
            textoRecorde.text = "Recorde: " + recordeAtual.ToString();
        }
    }
}