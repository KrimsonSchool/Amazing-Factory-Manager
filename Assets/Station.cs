using UnityEngine;
using UnityEngine.UI;

public class Station : MonoBehaviour
{
    public GameObject worker;
    public GameObject automaton;

    public int stationStatus; //0-empty, 1-worker, 2-automaton

    private FactoryManager manager;

    public float morale;
    public int moraleMax;
    public Slider moraleBar;

    public int prodRate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = FindFirstObjectByType<FactoryManager>();
        print(manager);
        morale = moraleMax;
        moraleBar.maxValue = moraleMax;
        
        RefreshWorker();
    }

    // Update is called once per frame
    void Update()
    {
        moraleBar.value = morale;
        if (morale < moraleMax)
        {
            morale += Time.deltaTime;
        }

        if (morale > moraleMax)
        {
            morale = moraleMax;
        }

        if (morale < 0)
        {
            morale = 0;
        }

        if (morale <= moraleMax / 4)
        {
            prodRate = 0;
        }
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
            manager.ReduceMorale();
            RefreshWorker();
        }
    }

    public void RefreshWorker()
    {
        if (stationStatus == 1)
        {
            worker.SetActive(true);
            prodRate = manager.employeeProductionRate;
        }

        if (stationStatus == 2)
        {
            worker.SetActive(false);
            automaton.SetActive(true);
            prodRate = manager.automationProductionRate;
        }
    }
}