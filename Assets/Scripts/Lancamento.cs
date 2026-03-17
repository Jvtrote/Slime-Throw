using UnityEngine;

public class SlimeLauncher : MonoBehaviour
{
    private Vector3 startPos;
    private bool isDragging = false;
    private Rigidbody2D rb;
    private LineRenderer lr;

    [Header("Configurações de Força")]
    public float power = 10f;
    public float maxDrag = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = gameObject.AddComponent<LineRenderer>(); // Cria a linha visualmente

        // Configuração básica da linha (visual do estilingue)
        lr.positionCount = 2;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.05f;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.white;
        lr.enabled = false;

        // Começa parado no ar (opcional)
        rb.gravityScale = 0;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPos.z = 0;

            // Limita a distância máxima do arrasto
            float distance = Vector3.Distance(startPos, currentPos);
            if (distance > maxDrag)
            {
                Vector3 direction = (currentPos - startPos).normalized;
                currentPos = startPos + direction * maxDrag;
            }

            // Atualiza a linha visual
            lr.SetPosition(0, startPos);
            lr.SetPosition(1, currentPos);
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        startPos = transform.position;
        lr.enabled = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
        lr.enabled = false;

        Vector3 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        endPos.z = 0;

        // Calcula a força baseada na distância do arrasto
        Vector2 force = startPos - endPos;
        float clampedForce = Mathf.Clamp(force.magnitude, 0, maxDrag);

        // Ativa a gravidade e lança!
        rb.gravityScale = 1;
        rb.AddForce(force.normalized * clampedForce * power, ForceMode2D.Impulse);
    }
}