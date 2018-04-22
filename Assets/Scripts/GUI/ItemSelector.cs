using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class ItemSelector : MonoBehaviour {

	public Color activeColor;

	private ArrayList myKeys;

	private Image[] ItemsGUI;
	// Use this for initialization
	void Start () {
		ItemsGUI = gameObject.GetComponentsInDirectChildren<Image>();
		foreach (Image image in ItemsGUI) {
			Debug.Log(image);
		}
		myKeys = new ArrayList {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5};
	}
	
	// Update is called once per frame
	void Update () {
		int index = -1;
		foreach(KeyCode key in myKeys) {
			if (Input.GetKeyDown(key)) {
				index = myKeys.IndexOf(key);
			}
		}
		if (index != -1) {
			foreach (Image image in ItemsGUI) {
				image.color = Color.white;
			}
			ItemsGUI[index].color = activeColor; //new Color(255 / 255.0f, 208 / 255.0f, 171 / 255.0f, 255 / 255.0f);
		}
	}
}
