using UnityEngine;
using System.Collections;

public class SkyboxControl : MonoBehaviour {

	public void SetTexture (string fileName) {
		Texture2D tex = Resources.Load("Textures/"+fileName) as Texture2D;

		Skybox s = gameObject.GetComponent("Skybox") as Skybox;
		Material m = s.material;

		m.SetTexture("_LeftTex", tex);
		m.SetTexture("_RightTex", tex);
		m.SetTexture("_DownTex", tex);
		m.SetTexture("_UpTex", tex);
		m.SetTexture("_BackTex", tex);
		m.SetTexture("_FrontTex", tex);
	}
}
