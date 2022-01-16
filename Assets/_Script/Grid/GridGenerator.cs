using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : Singletone<GridGenerator>
{
    [SerializeField] GridBase _grid;
    [SerializeField] [Range(0, 1)] float _wallPercentage = 0.1f;
    [SerializeField] float _tileSizeOffcet = 1f;
    [SerializeField] [Min (1)] int _startPoolSize = 100;

    public Dictionary<Vector2, TileBase> Tiles { get; private set; }
    LocalPool pool;

    protected override void Awake()
    {
        base.Awake();
        pool = new LocalPool(transform, _grid.tilePrefab.gameObject, _startPoolSize, 20);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    public TileBase GetTileAtPosition(Vector2 pos) => Tiles.TryGetValue(pos, out var tile) ? tile : null;

    public void Generate(int amountX, int amountY)
    {
        OffTiles();

        Tiles = _grid.GenerateGrid(transform, pool, amountX, amountY, _tileSizeOffcet, _wallPercentage);
        foreach (var tile in Tiles.Values) tile.CacheNeighbors();
    }

    void OffTiles()
    {
        var tiles = GetComponentsInChildren<TileBase>();
        if (tiles.Length > 0)
            foreach (var item in tiles)
            {
                item.SetOff();
                pool.PoolObjectSet(item.gameObject);
            }
    }
}
