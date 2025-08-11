using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialPanel;

    public GameObject[] tutorials;
    public int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseTutorial()
    {
        tutorials[index].SetActive(false);
    }

    public void ActivateNextTutorial()
    {
        tutorials[index].SetActive(true);
    }
    
    

    public void IncrimentIndex()
    {
        index++;
    }
}
