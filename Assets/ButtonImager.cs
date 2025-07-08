using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImager : MonoBehaviour
{
    public Sprite[] images;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonDown()
    {
        GetComponent<Image>().sprite = images[2];
    }

    public void ButtonUp()
    {
        GetComponent<Image>().sprite = images[0];

    }

    public void ButtonHover()
    {
        GetComponent<Image>().sprite = images[1];
    }
}
