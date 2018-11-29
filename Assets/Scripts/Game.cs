using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Game : MonoBehaviourPunCallbacks {
	const string GameVersion = "1";
	const string RoomName = "room";

	[SerializeField] BubbleGenerator bubbleGenerator;
	[SerializeField] MyWorldMapManager myWorldMapManager;

	void Start()
	{
		PhotonNetwork.AutomaticallySyncScene = true;
		PhotonNetwork.GameVersion = GameVersion;
		PhotonNetwork.ConnectUsingSettings();
	}

	public override void OnConnectedToMaster()
	{
		PhotonNetwork.JoinLobby();
	}

	public override void OnJoinedLobby()
	{
		var roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = 128;
		roomOptions.CleanupCacheOnLeave = true;
		roomOptions.IsOpen = true;
		roomOptions.IsVisible = true;
		PhotonNetwork.JoinOrCreateRoom(RoomName, roomOptions, new TypedLobby());
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("I'm in the room.");
		bubbleGenerator.gameObject.SetActive(true);
		myWorldMapManager.gameObject.SetActive(true);
	}
}