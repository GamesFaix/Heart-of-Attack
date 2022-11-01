using UnityEngine;
using System.Collections;

public class StartRoutine : MonoBehaviour {

	void Start () {
		gameObject.AddComponent("GUIQueue");
		gameObject.AddComponent("GUIInspector");
		gameObject.AddComponent("GUILog");
		gameObject.AddComponent("GUITools");
		CMD.New("start kata cara mawt kabu demo mein pano deci rook smas conf ashe batt garg griz talo meta ultr revo piec repr oldt lich bees myco mart blac pris aren prie drea recy necr mout mono");

		GUILog.ScrollToBottom();
	}

}
