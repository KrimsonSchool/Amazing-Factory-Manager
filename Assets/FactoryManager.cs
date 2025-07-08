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
    public int money;
    
    public int income;
    public int expenses;
    
    public string shareholderRequest;
    
    public int employees;
    public int employeeCost;
    
    public int automatons;
    public int automatonCost;
    
    public int day;
    
    public int productRate;
    public int productPrice;
    private float _timer;
    
    public Station[] allStations;
    public List<Station> stations;
    private float _automationPercent;

    [Header("Changers")] public float term; //how many seconds per day

    public int fees; //like rent,electricity etc...

    public TextMeshProUGUI infoText;
    public Slider timeSlider;

    [Header("Tabs")] 
    public GameObject stats;
    public GameObject management;

    [Header("Values")] 
    public int employeeWage;
    public int automationWage;

    public int employeeProductionRate;
    public int automationProductionRate;

    void Start()
    {
        allStations = FindObjectsByType<Station>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        
        IncrementDay();

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

    public void SwitchTabs()
    {
        if (stats.activeSelf)
        {
            stats.SetActive(false);
            management.SetActive(true);
        }
        else
        {
            stats.SetActive(true);
            management.SetActive(false);
        }
    }

    public void BuyWorker()
    {
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

    public void RefreshWorkers()
    {
        foreach (Station stst in stations)
        {
            stst.stationStatus = 1;
            stst.RefreshWorker();
        }
    }
}