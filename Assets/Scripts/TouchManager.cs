using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TouchManager : MonoBehaviour {
    private int[] combos;
    private int[] combosValue;
    private float[] timer;
    private string lastFeed = "";
    private bool[] isValid;

    private void Start()
    {
        combos = new int[2];
        combosValue = new int[2];
        timer = new float[2];
        isValid = new bool[2];
   
        for (int i = 0; i < 2; i++) {
            isValid[i] = true;
        }
    }

    void Update () {
        if (GameManager.instance.inGame)
        {
            Touch[] myTouches = Input.touches;
            foreach (Touch t in myTouches)
            {
                Vector3 position = Camera.main.ScreenToWorldPoint(t.position);
                timer[t.fingerId] += 0.02f;

                if (t.phase == TouchPhase.Began)
                {
                    checkTouch(t.position, t.fingerId);
                }

                if (t.phase == TouchPhase.Moved)
                {
                    checkTouch(t.position, t.fingerId);
                }

                if (t.phase == TouchPhase.Ended || timer[t.fingerId] > 0.5)
                {
                    if (combos[t.fingerId] > 1)
                    {
                        GameManager.instance.showCombo(position, combosValue[t.fingerId]);
                    }
                    else if (combos[t.fingerId] == 1 && isValid[t.fingerId])
                    {

                        GameManager.instance.showFeed(position, lastFeed);
                    }

                    combos[t.fingerId] = 0;
                    combosValue[t.fingerId] = 0;
                    isValid[t.fingerId] = true;
                    timer[t.fingerId] = 0;
                }
            }
        }
    }
 
    private void checkTouch(Vector3 pos, int touch)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);
        GameObject block = hit.collider.gameObject;

        if (hit.collider.tag == "Block")
        {
            combos[touch]++;
            int originalValue = block.GetComponent<Block>().value;
            float value = block.GetComponent<Block>().value * combos[touch] * GameManager.instance.universeBonus;
            block.GetComponent<Block>().value = (int)value;
            Block.hitInfo hf = block.GetComponent<Block>().hit();
            isValid[touch] = hf.correct;
            if (hf.correct)
            {
                combosValue[touch] += hf.value;
            }
            else
            {
                GameManager.instance.showFeed(wp, hf.feed);
            }
            block.GetComponent<Block>().value = originalValue;
            lastFeed = hf.feed;
        }
    }
}
