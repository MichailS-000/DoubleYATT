using TMPro;
using UnityEngine;

public class Clocks : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text recordText;

    void Update()
    {
        if (GameNetworkManager.instance)
		{
            text.text = "Time: " + Mathf.Round(GameNetworkManager.instance.GetTime());

            if (GameNetworkManager.instance.GetBestTime() < float.MaxValue)
			{
                recordText.text = "Best time: " + Mathf.Round(GameNetworkManager.instance.GetBestTime());
			}
		}
    }
}
