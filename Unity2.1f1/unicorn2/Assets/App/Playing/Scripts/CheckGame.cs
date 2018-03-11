using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class CheckGame : MonoBehaviour{

	//設置するbombの数
	public int bombCount;

	//呼び出すブロック
	public string blockObject;

	//bombを付加する変数
	public int[] attachBomb;

	private int safeCount;

	// Use this for initialization
	void Start()
	{
		//確認
		Debug.Log("CheckGameが呼ばれている");

		//bombの数設定
		bombCount = (GameManager.Instance.Row * GameManager.Instance.Column) / 8;

		//safeブロックの数決定
		safeCount = (GameManager.Instance.Row * GameManager.Instance.Column) - bombCount;

		attachBomb = new int[bombCount];


		//bombCountの数のブロックにbombを付加するよう決定する
		for (int i = 0; i < bombCount; i++) { 

			var ary = Enumerable.Range(1, GameManager.Instance.Row * GameManager.Instance.Column).OrderBy(n => Guid.NewGuid()).Take(bombCount).ToArray();

			for(int s = 0;s < ary.Length; s++)
			{
				//確認
				//Debug.Log(ary[s]);
				attachBomb[s] = ary[s];
			}
		}

		//attachBombの配列情報を基にPrefabブロックに値をbomb情報を渡す

	}

    // Update is called once per frame
    void Update()
    {

    }

    //クリックしたブロックに対する勝敗確認
    public void Win_Lose()
    {
        //確認
        Debug.Log("Win_Loseが呼ばれている");

		//クリックしたブロックのインスタンス
		GameObject CF = GameObject.Find("/BlockCanvas/AreaGenerator/Masu/" + blockObject);

		//確認
		//Debug.Log(CF.GetComponent<CheckFunction>()._status2_CF);
		Debug.Log("このブロックのsafe・bombステータスは" + CF.GetComponent<CheckFunction>()._status2_CF);

		//クリックしたブロックが地雷かどうか
		if (CF.GetComponent<CheckFunction>()._status2_CF == 4)
		{

			Debug.Log("爆発しました");

			//シーン遷移
			SceneManager.LoadScene("Title");
		}


		//開けたブロックの数カウント
		int j = 0;

		for(int i = 0;i < GameManager.Instance.Row * GameManager.Instance.Column; i++)
		{

			//各ブロックのインスタンス
			GameObject CB = GameObject.Find("/BlockCanvas/AreaGenerator/Masu/" + "AreaPrefab" + (i + 1));

			if(CB.GetComponent<CheckFunction>()._status_CF == 2)
			{
				//インクリメント
				j++;

				if(j == safeCount)
				{
					SceneManager.LoadScene("Result");
				}
			}

		}

		Debug.Log("開けたブロックの数:" + j);
	}

}
