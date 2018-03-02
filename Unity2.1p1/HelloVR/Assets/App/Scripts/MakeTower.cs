using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTower : MonoBehaviour {

    public GameObject Cube;

    public int tate;
    public int yoko;
    public int takasa;

    // Use this for initialization
    void Start()
    {

        GameObject[,,] Cubes = new GameObject[tate, takasa, yoko];

        //タテ
        for (int i = 0; i < takasa; i++)
        {
            //ヨコ
            for (int j = 0; j < yoko; j++)
            {

                //高さ
                for (int k = 0; k < tate; k++)
                {

                    Cubes[i, j, k] = Instantiate(Cube) as GameObject;
                    Cubes[i, j, k].transform.localPosition = new Vector3(5 + i, j * 2, 50 + k);

                }


            }


        }

    }


    //非効率
    //void Update()
    //{

    //    GameObject[,,] Cubes = new GameObject[tate, yoko, takasa];

    //    //タテ
    //    for (int i = 0; i < tate; i++)
    //    {
    //        //ヨコ
    //        for (int j = 0; j < yoko; j++)
    //        {

    //            //高さ
    //            for (int k = 0; k < takasa; k++)
    //            {

    //                Cubes[i, j, k] = Instantiate(Cube) as GameObject;
    //                Cubes[i, j, k].transform.localPosition = new Vector3(5, 30, 50);

    //            }


    //        }


    //    }

    //}

}
