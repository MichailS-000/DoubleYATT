using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text log;

    void Log(string message)
	{
        log.text += message + "\n";
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

	public void JoinRoom()
	{
		PhotonNetwork.JoinRandomOrCreateRoom();
	}

    public void CreateRoom()
	{
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = 8;
		PhotonNetwork.CreateRoom(Random.Range(float.MinValue, float.MaxValue).ToString(), roomOptions);
	}

	public override void OnJoinedRoom()
	{
		PhotonNetwork.LoadLevel(1);
	}
}