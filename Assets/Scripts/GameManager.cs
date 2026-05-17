using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Essencial para o texto do recorde

public class GameManager : MonoBehaviour
{
    [Header("Configurações do Jogo")]
    public GameObject painelGameOver;
    public Rigidbody2D slimeRb;

    [Header("Configurações de Distância/Recorde")]
    public TextMeshProUGUI textoRecorde; // Arraste o seu Texto da UI aqui
    public TextMeshProUGUI textoDistanciaAtual; // OPCIONAL: Se quiser mostrar a distância subindo em tempo real

    private bool jogoFinalizado = false;
    private float recordeMaximo;
    private Vector2 posicaoInicial;
    private bool slimeLancado = false;

    void Start()
    {
        // Carrega o recorde de distância salvo (usamos GetFloat para números quebrados)
        recordeMaximo = PlayerPrefs.GetFloat("RecordeDistancia", 0f);

        // Guarda a posição de onde o slime começou a partida
        if (slimeRb != null)
        {
            posicaoInicial = slimeRb.position;
        }

        AtualizarTextoRecorde();
    }

    void Update()
    {
        if (slimeRb == null || jogoFinalizado) return;

        // Detecta se o jogador lançou o slime (quando a gravidade ativa ou ele ganha velocidade)
        if (!slimeLancado && (slimeRb.gravityScale > 0 || slimeRb.linearVelocity.magnitude > 0.5f))
        {
            slimeLancado = true;
        }

        if (slimeLancado)
        {
            // Calcula a distância atual entre o ponto inicial e onde o slime está agora
            float distanciaAtual = Vector2.Distance(posicaoInicial, slimeRb.position);

            // Se você colocou o texto da distância atual, ele atualiza em tempo real (com 1 casa decimal)
            if (textoDistanciaAtual != null)
            {
                textoDistanciaAtual.text = "Distância: " + distanciaAtual.ToString("F1") + "m";
            }

            // Verifica se o Slime parou totalmente após ser lançado
            if (slimeRb.linearVelocity.magnitude < 0.1f && slimeRb.gravityScale > 0)
            {
                FinalizarPartida(distanciaAtual);
            }
        }
    }

    void FinalizarPartida(float distanciaFinal)
    {
        jogoFinalizado = true;

        // Se a distância dessa jogada for maior que o recorde antigo, salva o novo
        if (distanciaFinal > recordeMaximo)
        {
            recordeMaximo = distanciaFinal;
            PlayerPrefs.SetFloat("RecordeDistancia", recordeMaximo);
            PlayerPrefs.Save();

            AtualizarTextoRecorde();
        }

        if (painelGameOver != null)
        {
            painelGameOver.SetActive(true);
        }
    }

    void UpdateTextoRecorde() // Apenas para compatibilidade caso mude o nome
    {
        AtualizarTextoRecorde();
    }

    void AtualizarTextoRecorde()
    {
        if (textoRecorde != null)
        {
            // Mostra o recorde com "m" de metros e apenas uma casa decimal (ex: Recorde: 25.4m)
            textoRecorde.text = "Maior Distância: " + recordeMaximo.ToString("F1") + "m";
        }
    }

    public void ReiniciarODogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}