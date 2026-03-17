using UnityEngine;
using UnityEngine.InputSystem; // Adicione isso no topo!

public class SlimeLauncher : MonoBehaviour
{
    private Vector3 startPos;
    private bool isDragging = false;
    private Rigidbody2D rb;
    private LineRenderer lr;

    public float power = 15f;
    public float maxDrag = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        if (isDragging)
        {
            // Sistema Novo usa Mouse.current ou Touchscreen.current
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(mousePos);
            currentPos.z = 0;

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

    // No sistema novo, OnMouseDown as vezes não funciona bem se não houver um PhysicsRaycaster na câmera.
    // Mas se você mudar a configuração para "Both", o script anterior volta a funcionar perfeito!
}