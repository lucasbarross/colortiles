using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReliefBlock : Block {
    public override hitInfo hit()
    {
        int reliefed = GameManager.instance.relief(gameObject);
        string feed;
        bool correct = Color == GameManager.instance.actualColor;
        if (correct)
        {
            feed = "+" + reliefed;
        }
        else {
            feed = "X";
        }
        return new hitInfo(reliefed, correct, feed);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            stop();
            GameManager.instance.returnToPool(gameObject);
        }
    }
}
