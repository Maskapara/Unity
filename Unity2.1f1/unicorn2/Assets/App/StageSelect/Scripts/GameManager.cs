using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{ 
	[SerializeField]
    //インスペクタ上の_columnの値
	private int _column;

	[SerializeField]
    //インスペクタ上の_rowの値
	private int _row;

    //ゲッター(参照用)
    public int Column { get { return _column; } }
    public int Row{get{return _row;}}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);
	}

    //インスペクタ上のColumnとRowの値を引数に変更する
	public void Parameter(int column,int row){
		_column = column;
		_row = row;
	}
}