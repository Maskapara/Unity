using System.Collections;
using UnityEngine;

public class RouletteController : MonoBehaviour
{
	public float rotSpeed = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	//マウスが押されたら回転速度を設定する
		if (Input.GetMouseButtonDown(0))
		{
			this.rotSpeed = 25;
		}

		//回転速度分、ルーレットを回転させる
		transform.Rotate(0, 0, this.rotSpeed);

		//ルーレットを減速させる
		this.rotSpeed *= 0.98f;
	}
}
