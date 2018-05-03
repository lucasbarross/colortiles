using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseBlock : Block {
    public ColorPack[] universesColor;

    public override hitInfo hit()
    {
        GameManager.instance.changeUniverse(gameObject);
        string feed = "NEW COLORS! \n YEY!";
        return new hitInfo(value, true, feed);
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
