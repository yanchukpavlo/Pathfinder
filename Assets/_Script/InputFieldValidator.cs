using UnityEngine;

public class InputFieldValidator : MonoBehaviour
{
    TMPro.TMP_InputField inputField;
    const int DEFAULT_VALUE = 5;

    private void Awake()
    {
        inputField = GetComponent<TMPro.TMP_InputField>();
        Validate();
    }

    public void Validate()
    {
        if (string.IsNullOrEmpty(inputField.text) || int.Parse(inputField.text) == 0)
        {
            inputField.text = DEFAULT_VALUE.ToString();
        }
    }
}
