using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Transform[] columns;
    public bool inGame = false;
    double timer;

    void FixedUpdate()
    {
        if (inGame)
        {
            timer += 0.02;
            if (timer > (1 / GameManager.instance.gameSpeed))
            {
                GameObject go = GetComponent<PoolManager>().getRandomObject();
                go.transform.position = columns[0].transform.position;
                go.GetComponent<Block>().trigger();

                GameObject go1 = GetComponent<PoolManager>().getRandomObject();
                go1.transform.position = columns[1].transform.position;
                go1.GetComponent<Block>().trigger();

                GameObject go2 = GetComponent<PoolManager>().getRandomObject();
                go2.transform.position = columns[2].transform.position;
                go2.GetComponent<Block>().trigger();

                GameObject go3 = GetComponent<PoolManager>().getRandomObject();
                go3.transform.position = columns[3].transform.position;
                go3.GetComponent<Block>().trigger();

                timer = 0;
            }
        }
    }
    public void spawn()
    {
        inGame = true;
    }

    public void stop() {
        inGame = false;
    }
}



