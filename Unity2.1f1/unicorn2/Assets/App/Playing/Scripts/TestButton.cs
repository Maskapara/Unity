using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestButton : MonoBehaviour {

    [SerializeField]
    Button _button;

    // Use this for initialization
    void Start () {
        _button.onClick.AddListener(OnClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnClick()
    {
        Debug.Log(this.gameObject.name + "のボタンが押された");
        GameManager.Instance.Parameter(GameManager.Instance.Column + 1,GameManager.Instance.Row + 1);
        Debug.Log("行が増えるはず" + GameManager.Instance.Column + "列が増えるはず" + GameManager.Instance.Row);

        //タイトルに戻る
        SceneManager.LoadScene("Title");
    }
}