using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Configurações do Jogo")]
    public GameObject painelGameOver;
    public GameObject painelPause; // NOVO: Arraste o Painel de Pause aqui
    public Rigidbody2D slimeRb;

    [Header("Configurações de Distância/Recorde")]
    public TextMeshProUGUI textoRecorde;
    public TextMeshProUGUI textoDistanciaAtual;

    private bool jogoFinalizado = false;
    private bool jogoPausado = false; // NOVO: Controla o estado do pause
    private float recordeMaximo;
    private Vector2 posicaoInicial;
    private bool slimeLancado = false;

    void Start()
    {
        // Garante que o tempo do jogo está normal ao iniciar/reiniciar
        Time.timeScale = 1f;

        recordeMaximo = PlayerPrefs.GetFloat("RecordeDistancia", 0f);

        if (slimeRb != null)
        {
            posicaoInicial = slimeRb.position;
        }

        AtualizarTextoRecorde();
    }

    void Update()
    {
        // NOVO: Atalho no teclado/celular se quiser pausar apertando Esc ou voltando
        if (Input.GetKeyDown(KeyCode.Escape) && !jogoFinalizado)
        {
            if (jogoPausado) AlternarPause(false);
            else AlternarPause(true);
        }

        if (slimeRb == null || jogoFinalizado || jogoPausado) return;

        if (!slimeLancado && (slimeRb.gravityScale > 0 || slimeRb.linearVelocity.magnitude > 0.5f))
        {
            slimeLancado = true;
        }

        if (slimeLancado)
        {
            float distanciaAtual = Vector2.Distance(posicaoInicial, slimeRb.position);

            if (textoDistanciaAtual != null)
            {
                textoDistanciaAtual.text = "Distância: " + distanciaAtual.ToString("F1") + "m";
            }

            if (slimeRb.linearVelocity.magnitude < 0.1f && slimeRb.gravityScale > 0)
            {
                FinalizarPartida(distanciaAtual);
            }
        }
    }

    // NOVO: FUNÇÃO PARA O BOTÃO DE PAUSE E RETORNAR
    public void BotaoPause(bool pausar)
    {
        if (jogoFinalizado) return; // Não deixa pausar se já deu Game Over
        AlternarPause(pausar);
    }

    void AlternarPause(bool pausar)
    {
        jogoPausado = pausar;

        if (painelPause != null)
        {
            painelPause.SetActive(pausar); // Liga ou desliga o menu visual
        }

        // Se pausar, o tempo vira 0 (congela tudo). Se despausar, volta para 1 (normal).
        Time.timeScale = pausar ? 0f : 1f;
    }

    void FinalizarPartida(float distanciaFinal)
    {
        jogoFinalizado = true;

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

    void AtualizarTextoRecorde()
    {
        if (textoRecorde != null)
        {
            textoRecorde.text = "Maior Distância: " + recordeMaximo.ToString("F1") + "m";
        }
    }

    public void ReiniciarODogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // NOVO: FUNÇÃO PARA O BOTÃO DE SAIR PARA O MENU
    public void VoltarParaOMenu()
    {
        SceneManager.LoadScene("menu"); // Coloque o nome exato da sua cena de menu
    }
}