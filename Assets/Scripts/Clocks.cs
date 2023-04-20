using TMPro;
using UnityEngine;

public class Clocks : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    
    void Update()
    {
        if (GameNetworkManager.instance)
		{
            text.text = "Time: " + Mathf.Round(GameNetworkManager.instance.GetTime()); 
		}
    }
}
