using UnityEngine;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour
{
    Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = (1.0f / Time.deltaTime).ToString("0.0");
    }
}
