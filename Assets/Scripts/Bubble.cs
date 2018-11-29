using UnityEngine;

public class Bubble : MonoBehaviour {
	void Start () {
		MaterialPropertyBlock mpb = new MaterialPropertyBlock();
		mpb.SetColor("_Color", Color.HSVToRGB(Random.value, 1f, 1f));
		GetComponent<MeshRenderer>().SetPropertyBlock(mpb);
	}
}
