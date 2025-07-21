using System.Collections.Generic;
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

    void Start()
    {
        allStations = FindObjectsByType<Station>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        
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
        if (money >= employeeBaseCost)
        {
            money -= employeeBaseCost;
            MoneyFloat("-$",  employeeBaseCost, Color.red);
            
            employees++;

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
}