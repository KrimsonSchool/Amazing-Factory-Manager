using System.Collections.Generic;
using UnityEngine;

public class Convayor : MonoBehaviour
{
    public GameObject box;
    public List<GameObject> boxes;

    private float timer;
    //timer will go with rate
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            GameObject bx = Instantiate(box, Vector2.zero,  Quaternion.identity);
            bx.transform.SetParent(transform);
            bx.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1045, 134);
            boxes.Add(bx);
            timer = 0;
        }

        foreach (GameObject b in boxes)
        {
            b.transform.position += transform.right * Time.deltaTime * 100;
        }
    }
}
