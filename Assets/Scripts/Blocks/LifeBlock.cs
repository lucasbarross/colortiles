using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBlock : Block {

    public float downTimeLimit = 1f, UpTimeLimit = 1.5f;
    private float timerRandom = 0;
    private int icolor = 0;
    Color[] availableColors;
    public Image timer;
    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (InGame)
        {
            timerRandom += Time.deltaTime;
            timer.fillAmount += Time.deltaTime;
            if (timerRandom > Random.Range(downTimeLimit, UpTimeLimit))
            {
                availableColors = GameManager.instance.availableColors;
                Color colorChosen = availableColors[icolor % availableColors.Length];
                sr.color = colorChosen;
                Color = colorChosen;
                icolor++;
                timerRandom = 0;
                timer.fillAmount = 0;
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            stop();
            GameManager.instance.returnToPool(gameObject);
        }
    }

    public override hitInfo hit()
    {
        bool correct = GameManager.instance.getLife(gameObject);
        string feed;
        if (correct)
        {
            feed = "+1 life";
        }
        else {
            feed = "";
        }
        return new hitInfo(value, correct, feed);
    }
}
