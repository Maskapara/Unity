using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectDifficult:MonoBehaviour{

	[SerializeField]
	private int _column;

	[SerializeField]
	private int _row;

	[SerializeField]
	private Button _button;

	//難易度選択
	void Start () { 
		//StageSelect画面に遷移
		_button.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
        Debug.Log(this.gameObject.name + "のボタンが押された");

        //インスペクタ上のGameManagerのColumnとRowを引数の値に
		GameManager.Instance.Parameter(_column,_row);
		SceneManager.LoadScene("Playing");
	}
}
