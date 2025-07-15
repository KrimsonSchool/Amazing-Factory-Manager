using System;
using UnityEngine;

public class ShareholderRequest : MonoBehaviour
{
    public int minWorkers;
    public int maxWorkers;
    
    public int minAutomtons;
    public int maxAutomtons;

    [TextArea(8, 16)]
    public string request;

    private FactoryManager fm;
    
    public bool isActive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if workers is between minWorkers and maxWorkers and automatons is between minAutomatons and manAutomatons
        //then this request is active
        fm=FindFirstObjectByType<FactoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((fm.employees >= minWorkers && fm.employees < maxWorkers) &&
            (fm.automatons >= minAutomtons && fm.automatons < maxAutomtons))
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
    }
}
