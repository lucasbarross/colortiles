using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBlock : Block {

    public float downTimeLimit = 0.05f, UpTimeLimit = 0.1f;
    private float timerRandom = 0, timerColor = 0;
    private int icolor = 0;
    private bool colorDefined = false;
    public bool defined = true;
    Color[] availableColors;
    SpriteRenderer sr;

    public override hitInfo hit()
    {
        bool correct = GameManager.instance.checkColor(gameObject, false);
        string feed;

        if (correct)
        {
            feed = "+" + value;
        }
        else {
            feed = "X";
        }

        return new hitInfo(value, correct, feed);
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update () {
        if (!defined)
        {
            if (InGame)
            {
                timerRandom += Time.deltaTime;
                if (timerRandom > Random.Range(downTimeLimit, UpTimeLimit) && !colorDefined)
                {
                    availableColors = GameManager.instance.availableColors;
                    Color colorChosen = availableColors[icolor % availableColors.Length];
                    colorChosen = new Color(colorChosen.r, colorChosen.g, colorChosen.b, sr.color.a);
                    sr.color = colorChosen;
                    Color = colorChosen;
                    icolor++;
                    timerRandom = 0;
                }
          
                timerColor += Time.deltaTime;
                if (timerColor > (Random.Range(6.5f, 8) / Speed))
                {
                    colorDefined = true;
                }
            } else{
                colorDefined = false;
                timerRandom = 0;
                timerColor = 0;
            }
        }
	}
}
