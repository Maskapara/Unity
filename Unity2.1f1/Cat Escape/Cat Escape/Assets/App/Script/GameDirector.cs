using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
	GameObject hpGage;

	// Use this for initialization
	void Start () {
	this.hpGage = GameObject.Find("hpGage");
	}
	
	// Update is called once per frame
	public void DecreaseHp() {
		this.hpGage.GetComponent<Image>().fillAmount -= 0.1f;
	}
}
