using UnityEngine;

public class SlimeLauncher : MonoBehaviour
{
    [Header("Configurações de Voo")]
    public float power = 15f;      // Força do lançamento
    public float maxDrag = 3f;     // Limite de estiramento do "estilingue"

    private Vector3 startPos;      // Posição onde o clique começou
    private bool isDragging = false;
    private bool foiLancado = false; // A NOVA TRAVA: Impede múltiplos lançamentos

    private Rigidbody2D rb;
    private LineRenderer lr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Configura o LineRenderer (a linha visual do estilingue)
        lr = gameObject.GetComponent<LineRenderer>();
        if (lr == null) lr = gameObject.AddComponent<LineRenderer>();

        lr.positionCount = 2;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.05f;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.white;
        lr.endColor = new Color(1, 1, 1, 0); // Fica transparente na ponta
        lr.enabled = false;

        // Começa sem gravidade para não cair antes de ser lançado
        rb.gravityScale = 0;
    }

    void Update()
    {
        // Se estivermos arrastando, atualizamos a linha visual
        if (isDragging && !foiLancado)
        {
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPos.z = 0;

            // Limita visualmente o tamanho da linha
            float distance = Vector3.Distance(startPos, currentPos);
            if (distance > maxDrag)
            {
                Vector3 direction = (currentPos - startPos).normalized;
                currentPos = startPos + direction * maxDrag;
            }

            lr.SetPosition(0, startPos);
            lr.SetPosition(1, currentPos);
        }
    }

    void OnMouseDown()
    {
        // Só permite iniciar o arrasto se ainda não foi lançado
        if (!foiLancado)
        {
            isDragging = true;
            startPos = transform.position;
            lr.enabled = true;
        }
    }

    void OnMouseUp()
    {
        // Só faz o lançamento se estiver arrastando e ainda não tiver lançado
        if (isDragging && !foiLancado)
        {
            isDragging = false;
            lr.enabled = false;
            foiLancado = true; // ATIVA A TRAVA AQUI

            Vector3 mouseEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseEndPos.z = 0;

            // Calcula o vetor de força (posição inicial menos a posição final do mouse)
            Vector2 force = startPos - mouseEndPos;

            // Clampa a força para não passar do limite máximo
            float forceMagnitude = Mathf.Clamp(force.magnitude, 0, maxDrag);

            // Ativa a física e aplica o impulso
            rb.gravityScale = 1;
            rb.AddForce(force.normalized * forceMagnitude * power, ForceMode2D.Impulse);

            Debug.Log("Slime Lançado! Trava ativada.");
        }
    }
}