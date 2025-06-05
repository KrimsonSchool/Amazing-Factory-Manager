using TMPro;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    //Income/Expense | Shareholder requests Shareholder requests | Employees/Employee Income | Automation/Cost | Day
    [Header("Values")] 
    public int money = 10000;

    public int income;
    public int expenses;

    public string shareholderRequest;

    public int employees = 144;
    public int employeeCost;

    public int automatons;
    public int automatonCost;

    public int day;

    public int productRate;
    public int productPrice = 35;

    private float timer;

    [Header("Changers")] public float term; //how many seconds per day

    public int fees; //like rent,electricity etc...

    public TextMeshProUGUI infoText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
    }

    public void IncrementDay()
    {
        day++;
        employeeCost = 96 * employees;
        automatonCost = 43 * automatons;

        productRate = employees * 6 + (automatons * 22);
        //product price should randomly go up/down

        expenses = employeeCost + automatonCost + fees;
        income = productRate * productPrice;

        money += income;
        money -= expenses;

        infoText.text = "Money: $" + money + " | Income/Expenses: +" + income + "/-" + expenses +
                        " :: $" + (income - expenses) + " | Shareholder Requests: " + shareholderRequest +
                        " | Employees/Costs " +
                        employees + "/$" + employeeCost + " | Automations/Cost " + automatons + "/$" +
                        automatonCost + " | Production Rate: " + productRate + " | Product price: $" + productPrice +
                        " | Day: " + day;
    }
}