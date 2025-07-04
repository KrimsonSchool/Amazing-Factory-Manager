using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FactoryManager : MonoBehaviour
{
    //Income/Expense | Shareholder requests Shareholder requests | Employees/Employee Income | Automation/Cost | Day
    [Header("Values")] public int money;

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

    private float timer;

    public List<Station> stations;

    [Header("Changers")] public float term; //how many seconds per day

    public int fees; //like rent,electricity etc...

    public TextMeshProUGUI infoText;
    public Slider timeSlider;

    [Header("Tabs")] public GameObject Stats;
    public GameObject Management;

    [Header("Values")] public int employeeWage;
    public int automationWage;

    public int employeeProductionRate;
    public int automationProductionRate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IncrementDay();

        //individual employee cost: $96 per person a day ($12*8)
        //Individual automaton cost: $43

        //employee product rate: 6 a day
        //automaton product rate: 22 a day
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= term)
        {
            IncrementDay();
            timer = 0;
        }

        timeSlider.value = timer;
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

        infoText.text =
            $"Factory Automation: ??%\n\nProductivity: {productRate} Products / s\n({employees}workers, {automatons} machines)\n\nIncome: + ${income}\n(workers cost: ${employeeCost}, machines Cost: ${automatonCost}, expenses: ${fees})\n\nMoney: ${money}";

        /*"Money: $" + money + " | Income/Expenses: +" + income + "/-" + expenses +
                        " :: $" + (income - expenses) + " | Shareholder Requests: " + shareholderRequest +
                        " | Employees/Costs " +
                        employees + "/$" + employeeCost + " | Automations/Cost " + automatons + "/$" +
                        automatonCost + " | Production Rate: " + productRate + " | Product price: $" + productPrice +
                        " | Day: " + day;*/
    }

    public void SwitchTabs()
    {
        if (Stats.activeSelf)
        {
            Stats.SetActive(false);
            Management.SetActive(true);
        }
        else
        {
            Stats.SetActive(true);
            Management.SetActive(false);
        }
    }

    public void BuyWorker()
    {
        employees++;
        
        if (!stations.Contains(FindFirstObjectByType<Station>()))
        {
            stations.Add(FindFirstObjectByType<Station>());
        }
        else
        {
            //try agin to find other obj.... IDK....
        }

        RefreshWorkers();
    }

    public void RefreshWorkers()
    {
        foreach (Station stst in stations)
        {
            stst.stationStatus = 1;
            stst.RefreshWorkers();
        }
    }
}