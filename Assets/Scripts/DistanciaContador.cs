using UnityEngine;
using TMPro; // Importante para o TextMeshPro

public class DistanciaContador : MonoBehaviour
{
    public Transform slimeTransform; // Arraste o Slime para cá
    public TextMeshProUGUI textoMetros; // Arraste o texto para cá

    private float distanciaInicial;

    void Start()
    {
        if (slimeTransform != null)
        {
            // Guarda a posição de onde o Slime começou
            distanciaInicial = slimeTransform.position.x;
        }
    }

    void Update()
    {
        if (slimeTransform != null)
        {
            // Calcula quanto ele andou desde o início
            float distanciaAtual = slimeTransform.position.x - distanciaInicial;

            // Se ele voltar para trás, não queremos metros negativos
            if (distanciaAtual < 0) distanciaAtual = 0;

            // Atualiza o texto (o "F0" faz mostrar apenas números inteiros)
            textoMetros.text = "Distância: " + distanciaAtual.ToString("F0") + "m";
        }
    }
}