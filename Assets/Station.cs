using UnityEngine;

public class Station : MonoBehaviour
{
    public GameObject worker;
    public GameObject automaton;

    public int stationStatus; //0-empty, 1-worker, 2-automaton

    private FactoryManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager=FindFirstObjectByType<FactoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReplaceWorker()
    {
        if (manager.money >= manager.automationBaseCost)
        {
            manager.money -= manager.automationBaseCost;
            manager.MoneyFloat("-$", manager.automationBaseCost, Color.red);
            stationStatus++;
            FindFirstObjectByType<FactoryManager>().employees--;
            FindFirstObjectByType<FactoryManager>().automatons++;
            RefreshWorker();
        }
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
