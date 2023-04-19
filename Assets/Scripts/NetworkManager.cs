using Photon.Pun;
using TMPro;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text log;

    void Log(string message)
	{
        log.text.Insert(0, message + "\n");
	}

    void Start()
    {
        PhotonNetwork.GameVersion = Application.version;
        PhotonNetwork.ConnectUsingSettings();
    }

	public override void OnConnectedToMaster()
	{
        Log("Connected to master server");
	}
}
