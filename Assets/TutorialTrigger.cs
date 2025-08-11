using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTutorialIndex(int index)
    {
        if (this.enabled)
        {
            FindFirstObjectByType<Tutorial>().index = index;
            FindFirstObjectByType<Tutorial>().ActivateNextTutorial();
            this.enabled = false;
            print("Tutorial Index Set");
        }
    }
}
