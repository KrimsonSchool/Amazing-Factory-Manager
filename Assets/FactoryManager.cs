using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    //Income/Expense | Shareholder requests Shareholder requests | Employees/Employee Income | Automation/Cost | Day
    public int money;
    
    public int income;
    public int expenses;

    public string shareholderRequest;

    public int employees;
    public int employeeCost;

    public int automatons;
    public int automatonCost;

    public int day;

    private float timer;

    public float term;//how many seconds per day

    public int fees;//like rent,electricity etc...
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        expenses = employeeCost + automatonCost + fees;
        
        money += income;
        money -= expenses;
    }
}
