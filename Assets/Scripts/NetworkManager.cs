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
		Debug.Log(message);
	}

    void Start()
    {
        PhotonNetwork.GameVersion = Application.version;
		PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

	public override void OnConnectedToMaster()
	{
        Log("Connected to master server");
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		CreateRoom();
	}

	public void JoinRoom()
	{
		PhotonNetwork.JoinRandomRoom();
	}

    public void CreateRoom()
	{
		string name = Random.Range(float.MinValue, float.MaxValue).ToString();
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = 8;
		Debug.Log(name.GetHashCode());
		roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
		roomOptions.CustomRoomProperties.Add("seed", name.GetHashCode());
		PhotonNetwork.CreateRoom(name, roomOptions);
	}

	public override void OnJoinedRoom()
	{
		PhotonNetwork.LoadLevel(1);
	}
}