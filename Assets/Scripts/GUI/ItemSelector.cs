using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class ItemSelector : MonoBehaviour {

	public Color activeColor;
	private Player player;

	private ArrayList myKeys;

	private Image[] ItemsGUI;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		ItemsGUI = gameObject.GetComponentsInDirectChildren<Image>();
		ItemsGUI[0].color = activeColor;
		/*foreach (Image image in ItemsGUI) {
			Debug.Log(image);
		}*/
		myKeys = new ArrayList {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3};//, KeyCode.Alpha4, KeyCode.Alpha5};
	}
	
	// Update is called once per frame
	void Update () {
		int index = -1;
		foreach(KeyCode key in myKeys) {
			if (Input.GetKeyDown(key)) {
				index = myKeys.IndexOf(key);
				player.equipedObjectIndex = index;
				player.equipedObject = player.equipableObject[index].GetComponent<UsableObject>();
			}
		}


			foreach (Image image in ItemsGUI) {
				image.color = Color.white;
			}
			ItemsGUI[player.equipedObjectIndex%myKeys.Count].color = activeColor; //new Color(255 / 255.0f, 208 / 255.0f, 171 / 255.0f, 255 / 255.0f);

	}
}
