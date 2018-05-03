using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestroy : MonoBehaviour {
    public float time;

	void Start () {
        Destroy(this.gameObject, time);
	}
}
