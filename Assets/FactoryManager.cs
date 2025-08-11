using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FactoryManager : MonoBehaviour
{
    //Income/Expense | Shareholder requests Shareholder requests | Employees/Employee Income | Automation/Cost | Day
    [Header("Values")]
    [HideInInspector]
    public int money;
    
    [HideInInspector]
    public int income;
    [HideInInspector]
    public int expenses;
    
    [HideInInspector]
    public string shareholderRequest;
    
    [HideInInspector]
    public int employees;
    [HideInInspector]
    public int employeeCost;
    
    [HideInInspector]
    public int automatons;
    [HideInInspector]
    public int automatonCost;
    
    [HideInInspector]
    public int day;
    
    [HideInInspector]
    public int productRate;
    //[HideInInspector]
    public int productPrice;
    [HideInInspector]
    private float _timer;
    
    [HideInInspector]
    public Station[] allStations;
    [HideInInspector]
    public List<Station> stations;
    [HideInInspector]
    private float _automationPercent;

    [Header("Changers")] public float term; //how many seconds per day

    public int fees; //like rent,electricity etc...

    public TextMeshProUGUI infoText;
    public Slider timeSlider;

    public GameObject purchaseFloater; //floats up when buy shii

    public TextMeshProUGUI statsMoney;
    public TextMeshProUGUI statsIncome;

    [Header("Tabs")] public GameObject[] setTabs;

    [Header("Values")] 
    public int employeeWage;
    public int automationWage;

    public int employeeProductionRate;
    public int automationProductionRate;
    
    public int automationBaseCost;
    public int employeeBaseCost;

    public float[] xPositions;
    private int currentIndex;
    private float currentY;

    public GameObject workerObject;
    public GameObject workerHolder;
    void Start()
    {
        currentY = 1107;
        allStations = FindObjectsByType<Station>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        System.Array.Sort(allStations, (a,b) => a.gameObject.name.CompareTo(b.gameObject.name));
        
        IncrementDay();

        money = 10;

        //individual employee cost: $96 per person a day ($12*8)
        //Individual automaton cost: $43

        //employee product rate: 6 a day
        //automaton product rate: 22 a day
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= term)
        {
            IncrementDay();
            _timer = 0;
        }

        timeSlider.value = _timer;
        
        statsMoney.text = "$" + money;
        statsIncome.text = "+$" + income+"/day";
    }

    public void IncrementDay()
    {
        day++;
        employeeCost = employeeWage * employees;
        automatonCost = automationWage * automatons;

        productRate = employees * employeeProductionRate + (automatons * automationProductionRate);
        //product price should randomly go up/down

        expenses = employeeCost + automatonCost + fees;
        income = productRate * productPrice;

        money += income;
        money -= expenses;

        float c1 = automatons + employees;
        if (c1 > 0)
        {
            float c2 = automatons / c1;
            _automationPercent = c2 * 100;
        }

        infoText.text = 
            $"Factory Automation: {_automationPercent}%\n\nProductivity: {productRate} Products / s\n({employees} workers, {automatons} machines)\n\nIncome: + ${income}\n(workers cost: ${employeeCost}, machines Cost: ${automatonCost}, expenses: ${fees})\n\nMoney: ${money}";

        /*"Money: $" + money + " | Income/Expenses: +" + income + "/-" + expenses +
                        " :: $" + (income - expenses) + " | Shareholder Requests: " + shareholderRequest +
                        " | Employees/Costs " +
                        employees + "/$" + employeeCost + " | Automations/Cost " + automatons + "/$" +
                        automatonCost + " | Production Rate: " + productRate + " | Product price: $" + productPrice +
                        " | Day: " + day;*/
    }

    public void SwitchTabs(int tab)
    {
        foreach (var t in setTabs)
        {
            t.SetActive(false);
        }
        setTabs[tab].SetActive(true);
    }

    public void BuyWorker()
    {
        //y = 151    -240
        //instantiate worker, -670, -387, -122, 142, 386, 678
        //then move down -391
        if (money >= employeeBaseCost)
        {
            money -= employeeBaseCost;
            MoneyFloat("-$",  employeeBaseCost, Color.red);
            
            employees++;
            SpawnWorker();
            
            foreach (Station allStns in allStations)
            {
                if (!stations.Contains(allStns))
                {
                    stations.Add(allStns);
                    RefreshWorkers();
                    return;
                }
            }
        }
    }

    public void RefreshWorkers()
    {
        foreach (Station stst in stations)
        {
            stst.stationStatus = 1;
            stst.RefreshWorker();
        }
    }

    public void MoneyFloat(string preText, int amount, Color textColor)
    {
        GameObject mf =
        Instantiate(purchaseFloater, transform.position, Quaternion.identity);
        TextMeshProUGUI mt =  mf.GetComponentInChildren<TextMeshProUGUI>();
        mt.text = preText+amount;;
        mt.color = textColor;
        mt.transform.SetParent(transform);
    }

    public void SpawnWorker()
    {
        print("Spawn Worker");
        Vector3 spawnPosition = new Vector2(xPositions[currentIndex], currentY);
        GameObject wrkr = Instantiate(workerObject, workerHolder.transform);
        wrkr.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
        wrkr.GetComponent<Station>().stationStatus = 1;
        wrkr.GetComponent<Station>().RefreshWorker();
        
        currentIndex++;
        if (currentIndex >= 6)
        {
            currentIndex = 0;
            currentY -= 391;
        }
    }
}