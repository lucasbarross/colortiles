using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour {
    private bool inGame = false;
    private float speed;
    private string feed;
    private Color color;
    public bool randomColorInField = true;
    public int value;

    public Color Color
    {
        get
        {
            return color;
        }

        set {
            color = value;
        }
    }

    public string Feed
    {
        get
        {
            return feed;
        }

        set
        {
            feed = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public bool InGame
    {
        get
        {
            return inGame;
        }

        set
        {
            inGame = value;
        }
    }

    public struct hitInfo {
        public int value;
        public bool correct;
        public string feed;

        public hitInfo(int value, bool correct, string feed) {
            this.value = value;
            this.correct = correct;
            this.feed = feed;
        }
    }

    private void FixedUpdate()
    {
        /*
            RaycastHit2D right;
        RaycastHit2D down;

        right = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y), Vector2.right);
        down = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), Vector2.down);

        GameObject[] hits = { down.collider.gameObject, right.collider.gameObject };
        foreach(GameObject go in hits) {
            if (go.GetComponent<Block>().Color == this.color) {
                go.GetComponent<SpriteGlow.SpriteGlow>().enabled = true;
                GetComponent<SpriteGlow.SpriteGlow>().enabled = true;
            }
        } 
        */
    }

    public abstract hitInfo hit();

    public void updateVelocity () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * GameManager.instance.gameSpeed);
    }

    public void trigger()
    {
        Speed = GameManager.instance.gameSpeed;
        if (randomColorInField)
        {
            Color[] availableColors = GameManager.instance.availableColors;
            this.color = availableColors[Random.Range(0, availableColors.Length)];
            GetComponent<SpriteRenderer>().color = color;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * Speed);
        inGame = true;
    }

    public void stop()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        inGame = false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            GameManager.instance.checkColor(this.gameObject, true);
        }
    }
}
