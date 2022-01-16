using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TileImage : TileBase
{
    Image _image;
    Button _button;

    protected override void Awake()
    {
        base.Awake();
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _textPro = GetComponentInChildren<TMPro.TextMeshProUGUI>(true);

        _defaultColor = _image.color;
    }

    //private static readonly List<Vector2> Dirs = new List<Vector2>() {
    //        new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1), new Vector2(1, 0),
    //        new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1), new Vector2(-1, 1)
    //    };

    private static readonly List<Vector2> Dirs = new List<Vector2>() {
            new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1), new Vector2(1, 0)
        };

    public override void CacheNeighbors()
    {
        Neighbors = new List<TileBase>();

        foreach (var tile in Dirs.Select(dir => GridGenerator.Instance.GetTileAtPosition(Coords.Pos + dir)).Where(tile => tile != null))
        {
            Neighbors.Add(tile);
        }
    }

    public override void Init(bool walkable, ICoords coords)
    {
        base.Init(walkable, coords);
        _button.interactable = walkable;
    }

    public override void SetColor(Color color)
    {
        _image.color = color;
    }

    public override void Click()
    {
        if (_Start == null)
        {
            _selected = true;
            _Start = this;
            _textPro.text = $"{Coords.Pos.x}, {Coords.Pos.y}";
            _textPro.gameObject.SetActive(true);
            SetColor(Pathfinder.StartColor);
        }
        else if (_End == null)
        {
            _selected = true;
            _End = this;
            _textPro.text = $"{Coords.Pos.x}, {Coords.Pos.y}";
            _textPro.gameObject.SetActive(true);
            SetColor(Pathfinder.EndColor);

            Pathfinder.FindPath(_Start , _End);
        }
    }

    public override void SetOff()
    {
        _button.interactable = true;
        gameObject.SetActive(false);
        _selected = false;
        SetColor(_defaultColor);

        if (_Start == this)
        {
            _textPro.gameObject.SetActive(false);
            _Start = null;
        }
        else if (_End == this)
        {
            _textPro.gameObject.SetActive(false);
            _End = null;
        }
    }
}

public struct SquareCoords : ICoords {

    public float GetDistance(ICoords other) {
        var dist = new Vector2Int(Mathf.Abs((int)Pos.x - (int)other.Pos.x), Mathf.Abs((int)Pos.y - (int)other.Pos.y));

        var lowest = Mathf.Min(dist.x, dist.y);
        var highest = Mathf.Max(dist.x, dist.y);

        var horizontalMovesRequired = highest - lowest;

        return lowest * 14 + horizontalMovesRequired * 10 ;
    }

    public Vector2 Pos { get; set; }
}
