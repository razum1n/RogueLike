using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{

    public TextMeshProUGUI headerfield;

    public TextMeshProUGUI contentField;

    public RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string content, string header = "")
    {
        if(string.IsNullOrEmpty(header))
        {
            headerfield.gameObject.SetActive(false);
        }
        else
        {
            headerfield.gameObject.SetActive(true);
            headerfield.text = header;
        }

        contentField.text = content;

    }

    private void Update()
    {
        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        //rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = position;
    }
}
