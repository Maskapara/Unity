using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButton: MonoBehaviour {

    public void SceneLoad()
    {
        Debug.Log("ToResult");
        SceneManager.LoadScene("Result");
    }

}