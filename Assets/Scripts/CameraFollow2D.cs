using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target; // Arraste o Slime para cá no Inspector
    public float smoothSpeed = 0.125f; // Suavidade da câmera
    public Vector3 offset = new Vector3(5, 2, -10); // Distância da câmera em relação ao Slime

    void FixedUpdate()
    {
        if (target != null)
        {
            // Posição desejada (o Slime + o ajuste de distância)
            Vector3 desiredPosition = target.position + offset;

            // Suaviza o movimento da posição atual para a desejada
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Aplica a posição (mantendo o Z da câmera fixo)
            transform.position = smoothedPosition;
        }
    }
}