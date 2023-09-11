using UnityEngine;
using UnityEngine.Tilemaps;

public class QuebrarTile : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tileQuebrado;
    public Vector2Int quebraRadius;
    public int tilelife = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            // Quebrar os tiles ao redor da posição do ataque
            QuebrarTilesAoRedor(collision.transform.position);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Botão esquerdo do mouse
        {
            // Verificar se o clique atingiu o tile quebrável
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            QuebrarTilesAoRedor(mousePosition);
        }
    }

    private void QuebrarTilesAoRedor(Vector3 position)
    {
        // Obter a posição do tile quebrado
        Vector3Int tilePosition = tilemap.WorldToCell(position);

        // Percorrer a área de quebra ao redor da posição
        for (int x = -quebraRadius.x; x <= quebraRadius.x; x++)
        {
            for (int y = -quebraRadius.y; y <= quebraRadius.y; y++)
            {
                // Calcular a posição do tile atual
                Vector3Int currentTilePosition = tilePosition + new Vector3Int(x, y, 0);

                // Quebrar o tile
                tilemap.SetTile(currentTilePosition, tileQuebrado);
            }
        }
    }
}
