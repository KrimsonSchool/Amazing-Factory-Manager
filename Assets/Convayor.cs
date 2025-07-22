using System.Collections.Generic;
using UnityEngine;

public class Convayor : MonoBehaviour
{
    public GameObject box;
    public List<GameObject> boxes;

    private float timer;

    private float spawnInterval;
    //timer will go with rate
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private FactoryManager fm;
    void Start()
    {
        fm = FindFirstObjectByType<FactoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnInterval = 20 - (fm.productRate / 5f);
        spawnInterval = Mathf.Max(spawnInterval, 1f);
        
        timer += Time.deltaTime;
        if (timer > spawnInterval)
        {
            //print("speed: "+spawnInterval);
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
