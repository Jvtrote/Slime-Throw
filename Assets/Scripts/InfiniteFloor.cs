using UnityEngine;
using System.Collections.Generic;

public class InfiniteFloor : MonoBehaviour
{
    public Transform cameraTransform;
    public List<GameObject> floorTiles; // Arraste os 3 blocos para cá no Inspector
    public float tileSize = 20f; // O tamanho X do seu bloco de chão
    private int leftIndex = 0;

    void Update()
    {
        // Se a câmera passou do meio do bloco da direita...
        if (cameraTransform.position.x > (floorTiles[leftIndex].transform.position.x + tileSize * 1.5f))
        {
            MoveTile();
        }
    }

    void MoveTile()
    {
        // Pega o bloco que ficou mais para trás
        GameObject tileToMove = floorTiles[leftIndex];

        // Calcula a nova posição (no final da fila)
        int rightIndex = (leftIndex + floorTiles.Count - 1) % floorTiles.Count;
        float newX = floorTiles[rightIndex].transform.position.x + tileSize;

        tileToMove.transform.position = new Vector3(newX, tileToMove.transform.position.y, 0);

        // Atualiza quem é o novo bloco da esquerda
        leftIndex = (leftIndex + 1) % floorTiles.Count;
    }
}