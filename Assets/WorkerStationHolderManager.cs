using TMPro;
using UnityEngine;

public class WorkerStationHolderManager : MonoBehaviour
{
    public GameObject workerHolder;

    [SerializeField]
    private float moveDist;
    private float currentDist;
    private float baseDist;
    
    int workerCount;

    private FactoryManager fm;

    public GameObject downButton;
    public TextMeshProUGUI moveDistText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseDist = 428.47f; 
        currentDist = baseDist;

        moveDist = 428.47f;  

        //moves to 925

        fm= FindFirstObjectByType<FactoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //print("Current pos: " + transform.position.y);

        workerCount = fm.employees;
        
        moveDistText.text = "Dist: "+ moveDist;
        
        if (workerCount > 14) {
            downButton.SetActive(true);
            moveDist = 496.53f + Mathf.Floor((workerCount - 14) / 7f) * 496.53f;        
        }
    }

    //move down 496.53, max out at moveDist
    public void MoveDown()
    {
        print("Moving to: " + moveDist);
        workerHolder.transform.position += new Vector3(0, 496.53f);

        /*workerHolder.transform.position = new Vector3(
            workerHolder.transform.position.x,
            Mathf.Clamp(workerHolder.transform.position.y, moveDist, 0f),
            workerHolder.transform.position.z);
            */
    }
}
