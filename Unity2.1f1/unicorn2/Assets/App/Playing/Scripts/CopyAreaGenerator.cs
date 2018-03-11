using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyAreaGenerator : MonoBehaviour
{

	public GameObject AreaPrefab;

    [SerializeField]
    private Transform _masu;

    public void Start ()
	{   //確認                        
		GameObject[,] Areas = new GameObject[GameManager.Instance.Column,GameManager.Instance.Row];        

		//生成開始する座標中心
		gameObject.transform.localPosition = new Vector2(0,0);

		//ブロックのナンバリング(1～ブロックの個数)
		int bn = 1;

		//行
		for (int i = 0; i < GameManager.Instance.Row; i++)
		{
			
			//列
			for (int j = 0; j < GameManager.Instance.Column; j++)
			{
				Areas[i, j] = Instantiate(AreaPrefab, _masu) as GameObject;

				//ブロック毎に座標情報を付加して命名
				Areas[i, j].name = "AreaPrefab" + bn;
				Areas[i, j].transform.localPosition = new Vector2((i * 50) - (GameManager.Instance.Row * 25) + 25, (j * 50) - (GameManager.Instance.Column * 25) + 25);

				//子オブジェクト生成(周囲の爆弾数を後で記述する)
				GameObject bombCounter = new GameObject();
				bombCounter.name = "bombCounter" + bn;
				bombCounter.AddComponent<Text>();
				bombCounter.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
				bombCounter.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
				bombCounter.GetComponent<Text>().color = new Color(0,0,0,1);


				//判定を親オブジェクトと同サイズへ
				//bombCounter.GetComponent<RectTransform>().sizeDelta = new Vector2(Areas[i, j].GetComponent<RectTransform>().sizeDelta.x, Areas[i, j].GetComponent<RectTransform>().sizeDelta.y);


				//テキストコンポーネントの判定を消す
				bombCounter.GetComponent<Text>().raycastTarget = false;

				bombCounter.transform.SetParent(Areas[i, j].transform, false);
				bombCounter.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);


				bn++;
			}
		}

		//動かない
		//_button.onClick.AddListener(OnClick);
	}

    private void OnClick()
    {
        Debug.Log(this.gameObject.name + "がCopyAreaGeneratorから押されている");
    }

}