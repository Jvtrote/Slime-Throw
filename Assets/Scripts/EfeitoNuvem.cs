using UnityEngine;

public class EfeitoNuvem : MonoBehaviour
{
    private Transform cameraTransform;
    public float tamanhoDaNuvem = 20f; // A largura da imagem da sua nuvem no cenário
    public float velocidadeParallax = 0.8f; // 1 significa que ela anda junto com a câmera. Menos que 1 ela anda mais devagar (dá efeito de fundo)

    private float posicaoXAnteriorCam;

    void Start()
    {
        // Encontra a câmera principal do jogo automaticamente
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
            posicaoXAnteriorCam = cameraTransform.position.x;
        }
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        // Efeito opcional de Parallax (fazer a nuvem mover um pouco mais devagar para dar profundidade)
        float movimentoCam = cameraTransform.position.x - posicaoXAnteriorCam;
        transform.position += Vector3.right * (movimentoCam * velocidadeParallax);
        posicaoXAnteriorCam = cameraTransform.position.x;

        // O SEGREDO DA RECICLAGEM:
        // Se a câmera andou mais para a frente do que a posição da nuvem + o tamanho dela...
        if (cameraTransform.position.x - transform.position.x > tamanhoDaNuvem)
        {
            // ...jogamos a nuvem para a frente!
            float novaPosicaoX = transform.position.x + (tamanhoDaNuvem * 2f);

            // Dá uma variada leve na altura (Y) para as nuvens não ficarem todas numa linha perfeita
            float alturaAleatoria = Random.Range(2f, 5f);

            transform.position = new Vector3(novaPosicaoX, alturaAleatoria, transform.position.z);
        }
    }
}