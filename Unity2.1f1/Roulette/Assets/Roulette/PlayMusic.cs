using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{

	public AudioClip audioClip1;
	private AudioSource audioSource;

    private GameObject RouletteController;
    private float sp;

    RouletteController RC = new RouletteController();


    // Use this for initialization
    void Start()
	{
        

        audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.clip = audioClip1;
		audioSource.Play();
	}

    private void Update()
    {
        sp = RC.rotSpeed / 25;
        this.audioSource.volume *= sp;
    }

}

