using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class TileBase : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    protected Color _obstacleColor;

    public ICoords Coords;
    public float GetDistance(TileBase other) => Coords.GetDistance(other.Coords);
    public bool Walkable { get; private set; }
    public RectTransform Rect { get; private set; }
    protected bool _selected;
    protected Color _defaultColor;

    protected TextMeshProUGUI _textPro;

    protected static TileBase _Start;
    protected static TileBase _End;

    protected virtual void Awake()
    {
        Rect = transform as RectTransform;
    }

    public virtual void Init(bool walkable, ICoords coords)
    {
        Walkable = walkable;
        Coords = coords;
    }

    public abstract void SetOff();

    public abstract void Click();

    #region Pathfinding

    //[Header("Pathfinding")]
    //[SerializeField]
    //private TextMeshPro _fCostText;

    //[SerializeField] private TextMeshPro _gCostText, _hCostText;
    public List<TileBase> Neighbors { get; protected set; }
    public TileBase Connection { get; private set; }
    public float G { get; private set; }
    public float H { get; private set; }
    public float F => G + H;

    public abstract void CacheNeighbors();

    public void SetConnection(TileBase nodeBase)
    {
        Connection = nodeBase;
    }

    public void SetG(float g)
    {
        G = g;
        //SetText();
    }

    public void SetH(float h)
    {
        H = h;
        //SetText();
    }

    //private void SetText()
    //{
    //    if (_selected) return;
    //    _gCostText.text = G.ToString();
    //    _hCostText.text = H.ToString();
    //    _fCostText.text = F.ToString();
    //}

    public abstract void SetColor(Color color);

    #endregion
}


public interface ICoords {
    public float GetDistance(ICoords other);
    public Vector2 Pos { get; set; }
}