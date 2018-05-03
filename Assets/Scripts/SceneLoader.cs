using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    

    public void LoadScene(int i) {
        SceneManager.LoadScene(i);
    }
    public void LoadScene(string i)
    {
        SceneManager.LoadScene(i);
    }
}
