using UnityEngine;

public class Station : MonoBehaviour
{
    public GameObject worker;
    public GameObject automaton;

    public int stationStatus; //0-empty, 1-worker, 2-automaton
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReplaceWorker()
    {
        stationStatus ++;
        FindFirstObjectByType<FactoryManager>().employees--;
        FindFirstObjectByType<FactoryManager>().automatons++;
        RefreshWorker();
    }

    public void RefreshWorker()
    {
        if (stationStatus == 1)
        {
            worker.SetActive(true);
        }

        if (stationStatus == 2)
        {
            worker.SetActive(false);
            automaton.SetActive(true);
        }
    }
}
