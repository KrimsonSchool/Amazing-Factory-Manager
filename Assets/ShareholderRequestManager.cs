using TMPro;
using UnityEngine;

public class ShareholderRequestManager : MonoBehaviour
{
    public ShareholderRequest[] requests;
    
    public TextMeshProUGUI shareholderRequest;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < requests.Length; i++)
        {
            if (requests[i].isActive)
            {
                shareholderRequest.text = requests[i].request;
            }
        }
    }
}
