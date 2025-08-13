using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Metalic : MonoBehaviour
{

    public GameObject sparkFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("sparked");
            GameObject sprx = Instantiate(sparkFX, this.transform);
            sprx.transform.position = Input.mousePosition;
            sprx.transform.eulerAngles = new Vector3(0, 0, Random.Range(0,360));
        }
    }
}
