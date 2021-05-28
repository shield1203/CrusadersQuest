using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TextColor
{
    _white,
    _black,
}

public class FloatingText : MonoBehaviour
{
    public void InitializeFloatingText(string text, TextColor color)
    {
        for (int index = 0; index < text.Length; index++)
        {
            string prefabPath = "FloatingText/" + text[index] + color.ToString();

            GameObject textPrefab = Instantiate(Resources.Load(prefabPath) as GameObject);
            textPrefab.transform.SetParent(gameObject.transform.Find("Text"));
            textPrefab.transform.localPosition = new Vector3(index * 0.34f, 0, index * -0.001f);
        }
    }

    void Start()
    {
        Random random = new Random();
        transform.position += new Vector3(Random.Range(-0.5f, -0.7f), Random.Range(2.0f, 2.3f), 0);
        Destroy(gameObject, 1.0f);
    }   
}
