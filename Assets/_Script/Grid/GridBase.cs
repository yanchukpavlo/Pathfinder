using System.Collections.Generic;
using UnityEngine;

public abstract class GridBase : ScriptableObject
{
    [SerializeField] public TileBase tilePrefab;
    public abstract Dictionary<Vector2, TileBase> GenerateGrid(Transform parent, LocalPool pool, int amountX, int amountY, float tileSizeOffcet = 0, float wallPercentage = 0.1f);
}
