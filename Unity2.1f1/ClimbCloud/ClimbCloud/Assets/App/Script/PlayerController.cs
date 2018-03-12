using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	private Rigidbody2D rigid2D;
	Animator animator;
	private float jumpForce = 680.0f;
	private float walkforce = 30.0f;
	private float maxwalkSpeed = 2.0f;
	private float threshold = 0.2f;


	// Use this for initialization
	void Start ()
	{
		this.rigid2D = GetComponent<Rigidbody2D>();
		this.animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//ジャンプ
		if (Input.GetMouseButton(0)&& this.rigid2D.velocity.y == 0)
		{
			this.rigid2D.AddForce(transform.up * jumpForce);
		}

		//左右
		int key = 0;
		if (Input.acceleration.x > this.threshold) key = 1;
		if (Input.acceleration.x < this.threshold) key = -1;

		//プレイヤー速度
		float speedx = Mathf.Abs(this.rigid2D.velocity.x);

		//スピード制限
		if (speedx < this.maxwalkSpeed)
		{
			this.rigid2D.AddForce(transform.right * key * this.walkforce);
		}

		

		//方向
		if (key != 0)
		{
			transform.localScale = new Vector3(key,1,1);
		}

		//プレイヤーの速度
		this.animator.speed = speedx / 2.0f;	

		//画面外処理
		if(transform.position.y < -10){
			SceneManager.LoadScene("GameScene");
		}	
	}

	void OnTriggerEnter2D(Collider2D other){
			Debug.Log("ゴール");
			SceneManager.LoadScene("ClearScene");
		}

}
