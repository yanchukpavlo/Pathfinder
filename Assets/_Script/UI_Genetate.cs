using UnityEngine;
using TMPro;


public class UI_Genetate : MonoBehaviour
{
    [SerializeField] TMP_InputField xInpud;
    [SerializeField] TMP_InputField yInpud;

    public void ButtonGenetate()
    {
        int x, y;

        x = Mathf.Abs(int.Parse(xInpud.text));
        y = Mathf.Abs(int.Parse(yInpud.text));

        GridGenerator.Instance.Generate(x, y);
    }
}
