using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckFunction : MonoBehaviour {

	[SerializeField]
	private enum Status
	{

		//ブロックのステータス(open・close)
		CLOSED = 1,
		OPENED = 2

	}

	[SerializeField]
	private enum Status2
	{
		//ブロックのステータス(bomb・safe)
		SAFE = 3,
		BOMB = 4

	}

	//ブロックのステータス(open・close)
	public int _status_CF;

	//ブロックのステータス2(safe・bomb)
	public int _status2_CF;

	//ブロックに付加するボタン
	[SerializeField]
	private Button _button;

	//周囲のブロック郡
	private int[] causedBlocks;

	//オープンにする候補のブロック郡
	private HashSet<int> willOpenblocks;	

	//各ブロックに対して呼ばれる
	void Start() {

		//確認
		Debug.Log("CheckFunctionのスタートメソッドが呼ばれている");

		//PlayingDirectorの初期化
		GameObject PD = GameObject.Find("/PlayingDirector");

		//CheckGame側でCLOSED状態へ
		_status_CF = (int)Status.CLOSED;

		//safe(enum)を入力
		this._status2_CF = (int)Status2.SAFE;

		//bomb(enum)を入力
		foreach (int j in PD.GetComponent<CheckGame>().attachBomb)
		{
			Debug.Log("bombが入るのは" + j);

			if (this.gameObject.name == ("AreaPrefab" + j))
			{
				//bomb設定
				this._status2_CF = (int)Status2.BOMB;
				
			}

			Debug.Log("ステータス" + this.gameObject.name + "は" + this._status2_CF);
		}

		//ブロックを開ける処理
		this._button.onClick.AddListener(Open);
	}

	// Update is called once per frame
	void Update() {

	}

	//ブロックのオープン処理
	private void Open()
	{
		//PlayingDirectorの初期化
		GameObject PD = GameObject.Find("/PlayingDirector");

		//クリックしたブロックの名前をCheckGameに渡す
		PD.GetComponent<CheckGame>().blockObject = this.gameObject.name;
		
		//子オブジェクトのテキストコンポーネント取得
		Text bombCounter = GetComponentInChildren<Text>();


		//ブロックのオープン処理分岐
		if (_status_CF != (int)Status.OPENED)
		{

			//ブロックのステータスをオープンに変更
			_status_CF = (int)Status.OPENED;

			//オープンした際にブロックの色を変更する(open)
			//this.GetComponent<Text>().text = int.Parse(this.gameObject.name.Substring(10)).ToString();

			//ブロックに周囲の爆弾数を書き込む
			bombCounter.text = haveArroundBomb(int.Parse(this.gameObject.name.Substring(10))).ToString();

			//周りに地雷が無い場合
			if (haveArroundBomb(int.Parse(this.gameObject.name.Substring(10))) == 0)
			{
				int[] openBlocks = moreOpenDecide(int.Parse(this.gameObject.name.Substring(10)));

				foreach(int j in openBlocks)
				{
					//インスタンス初期化
					GameObject CF = GameObject.Find("/BlockCanvas/AreaGenerator/Masu/" + "AreaPrefab" + j.ToString());

					//オープン連鎖
					CF.GetComponent<CheckFunction>().Open();

				}
			}			

		} else if (_status_CF == (int)Status.OPENED){

			//オープンブロックをオープンしようとしたときの処理
			Debug.Log("このブロックは既にオープンになっています");

			//周囲にあるブロック数の表示
			bombCounter.text = haveArroundBomb(int.Parse(this.gameObject.name.Substring(10))).ToString();
		}

		//勝敗確認
		PD.GetComponent<CheckGame>().Win_Lose();

	}

	//周囲ブロックの生成
	public int[] moreOpenDecide(int centerNumber)
	{

		//8方向のブロック生成
		int[] otherBlock = new int[8];

		//空いていないブロックのみのList生成
		List<int> unOpend = new List<int>();

		//出力配列
		int[] unOpendBlocks;

		//左下
		if (centerNumber == 1)
		{
			otherBlock[0] = centerNumber + 1;
			otherBlock[1] = centerNumber + GameManager.Instance.Column;
			otherBlock[2] = centerNumber + 1 + GameManager.Instance.Column;

		}//左上
		else if (centerNumber == GameManager.Instance.Column)
		{
			otherBlock[0] = centerNumber - 1;
			otherBlock[1] = centerNumber + GameManager.Instance.Column - 1;
			otherBlock[2] = centerNumber + GameManager.Instance.Column;

		}//右下
		else if (centerNumber == (1 + (GameManager.Instance.Row - 1) * GameManager.Instance.Column))
		{
			otherBlock[0] = (1 + (GameManager.Instance.Row - 2) * GameManager.Instance.Column);
			otherBlock[1] = (2 + (GameManager.Instance.Row - 2) * GameManager.Instance.Column);
			otherBlock[2] = (2 + (GameManager.Instance.Row - 1) * GameManager.Instance.Column);

		}//右上
		else if (centerNumber == (GameManager.Instance.Column + (GameManager.Instance.Row - 1) * GameManager.Instance.Column))
		{
			otherBlock[0] = (GameManager.Instance.Column - 1 + (GameManager.Instance.Row - 2) * GameManager.Instance.Column);
			otherBlock[1] = (GameManager.Instance.Column + (GameManager.Instance.Row - 2) * GameManager.Instance.Column);
			otherBlock[2] = (GameManager.Instance.Column + (GameManager.Instance.Row - 1) * GameManager.Instance.Column);

		}//左端沿い
		else if (1 < centerNumber && centerNumber < GameManager.Instance.Column)
		{
			otherBlock[0] = centerNumber - 1;
			otherBlock[1] = centerNumber + 1;
			otherBlock[2] = centerNumber + GameManager.Instance.Column - 1;
			otherBlock[3] = centerNumber + GameManager.Instance.Column;
			otherBlock[4] = centerNumber + GameManager.Instance.Column + 1;

		}//右端沿い
		else if ((1 + (GameManager.Instance.Row - 1) * GameManager.Instance.Column) < centerNumber && centerNumber < (GameManager.Instance.Column + (GameManager.Instance.Row - 1) * GameManager.Instance.Column))
		{
			otherBlock[0] = centerNumber - GameManager.Instance.Column - 1;
			otherBlock[1] = centerNumber - GameManager.Instance.Column;
			otherBlock[2] = centerNumber - GameManager.Instance.Column + 1;
			otherBlock[3] = centerNumber - 1;
			otherBlock[4] = centerNumber + 1;

		}//下端
		else if (1 < centerNumber && centerNumber % GameManager.Instance.Column == 1 && centerNumber < (1 + (GameManager.Instance.Row - 1) * GameManager.Instance.Column))
		{
			otherBlock[0] = centerNumber - GameManager.Instance.Column;
			otherBlock[1] = centerNumber - GameManager.Instance.Column + 1;
			otherBlock[2] = centerNumber + 1;
			otherBlock[3] = centerNumber + GameManager.Instance.Column;
			otherBlock[4] = centerNumber + GameManager.Instance.Column + 1;

		}//上端
		else if (GameManager.Instance.Column < centerNumber && centerNumber % GameManager.Instance.Column == 0 && centerNumber < (GameManager.Instance.Column + (GameManager.Instance.Row - 1) * GameManager.Instance.Column))
		{
			otherBlock[0] = centerNumber - GameManager.Instance.Column - 1;
			otherBlock[1] = centerNumber - GameManager.Instance.Column;
			otherBlock[2] = centerNumber - 1;
			otherBlock[3] = centerNumber + GameManager.Instance.Column - 1;
			otherBlock[4] = centerNumber + GameManager.Instance.Column;

		}//中央
		else
		{
			otherBlock[0] = centerNumber - GameManager.Instance.Column - 1;
			otherBlock[1] = centerNumber - GameManager.Instance.Column;
			otherBlock[2] = centerNumber - GameManager.Instance.Column + 1;
			otherBlock[3] = centerNumber - 1;
			otherBlock[4] = centerNumber + 1;
			otherBlock[5] = centerNumber + GameManager.Instance.Column - 1;
			otherBlock[6] = centerNumber + GameManager.Instance.Column;
			otherBlock[7] = centerNumber + GameManager.Instance.Column + 1;

		}

		foreach (int check in otherBlock)
		{
			//インスタンス初期化
			GameObject CF = GameObject.Find("/BlockCanvas/AreaGenerator/Masu/" + "AreaPrefab" + check.ToString());

			//実値のみ
			if (check != 0)
			{
				//まだ空いていないブロックのみ
				if (CF.GetComponent<CheckFunction>()._status_CF != (int)Status.OPENED)
				{
					unOpend.Add(check);
				}
			}

		}

		//空いていないブロックのみの配列
		unOpendBlocks = unOpend.ToArray();

		return unOpendBlocks;
	}

	//ブロックの周囲に地雷がいくつあるか
	public int haveArroundBomb(int dBlock)
	{

		int count = 0;

		//クリックしてオープンしたブロックの隣接に地雷があるかどうか調べる

		foreach (int doubt in moreOpenDecide(dBlock))
		{

			if (dBlock != 0)
			{
				//インスタンス初期化
				GameObject CF = GameObject.Find("/BlockCanvas/AreaGenerator/Masu/" + "AreaPrefab" + doubt.ToString());

				//doubts内に地雷が合った場合trueへ
				if (CF.GetComponent<CheckFunction>()._status2_CF == (int)Status2.BOMB)
				{
					count++;
				}
			}
		}

		return count;
	}

}