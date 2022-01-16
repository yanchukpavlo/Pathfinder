using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Grid Square New", menuName = "ScriptableObjects/Grid/Grid Square")]
public class GridImage : GridBase
{
    public override Dictionary<Vector2, TileBase> GenerateGrid(Transform parent, LocalPool pool, int amountX, int amountY, float tileSizeOffcet, float wallPercentage)
    {
        Vector3 position;
        GameObject obj;
        var tiles = new Dictionary<Vector2, TileBase>();

        RectTransform rectTransform = parent as RectTransform;
        float tileSizeX = (Screen.width + rectTransform.sizeDelta.x) / amountX;
        float tileSizeY = (Screen.height + rectTransform.sizeDelta.y) / amountY;
        float xPositionOffset = rectTransform.offsetMin.x + tileSizeX * 0.5f;
        float yPositionOffset = -rectTransform.offsetMax.y + tileSizeY * 0.5f;

        for (int i = 0; i < amountX; i++)
        {
            for (int q = 0; q < amountY; q++)
            {
                position = new Vector3(i * tileSizeX + xPositionOffset, Screen.height - (q * tileSizeY + yPositionOffset), 0);
                obj = pool.PoolObjectGet(position, Quaternion.identity);
                if (obj.TryGetComponent(out TileBase tile))
                {
                    tile.Rect.sizeDelta = new Vector2(tileSizeX - tileSizeOffcet, tileSizeY - tileSizeOffcet);
                    tile.Init(Random.value > wallPercentage, new SquareCoords { Pos = new Vector3(i + 1, q + 1) });
                    tiles.Add(new Vector2(i + 1, q + 1), tile);
                }
                else
                {
                    Debug.LogError("Spawned object don`t have Tile component.");
                    break;
                }
            }
        }

        return tiles;
    }
}
