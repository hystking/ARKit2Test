using UnityEngine;
using Photon.Pun;

public class BubbleGenerator : MonoBehaviour {
	[SerializeField] string bubblePrefabName;
	
	void Update () {
		if (Input.GetButtonDown("Fire1"))
		{
			PhotonNetwork.Instantiate(bubblePrefabName, transform.position, Quaternion.identity);
		}
	}
}