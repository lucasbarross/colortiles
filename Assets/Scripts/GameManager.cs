using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityScript.Lang;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public ColorPack colorBlindColors;
    public ColorPack defaultColors;
    public float universeBonus;

    public Color[] availableColors;
    public Color actualColor;

    public int maxLives = 1;
    private int lives;
    public float gameSpeed;
    
    private UIManager ui;
    private PoolManager pool;
    private Spawner spawner;

    int roundScore = 0;
    int comboScore = 0;
    public bool inGame;

    void Awake () {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }

        ui = FindObjectOfType<UIManager>();
        pool = FindObjectOfType<PoolManager>();
        spawner = FindObjectOfType<Spawner>();
        begin();
    }
  
    public void begin() {
        Time.timeScale = 1;
        bool colorBlind = PlayerPrefs.GetInt("colorBlind") != 0;
        availableColors = colorBlind ? colorBlindColors.colors : defaultColors.colors;
        universeBonus = defaultColors.bonus;
        lives = maxLives;
        ui.updateScore(0);
        ui.updateHighScore(PlayerPrefs.GetInt("score"));
        chooseColor();
        spawner.spawn();
        inGame = true;
    }

    void chooseColor() {
        actualColor = availableColors[Random.Range(0, 4)];
        ui.changePreviewColor(actualColor);
        ui.preview.GetComponent<Animator>().SetTrigger("changeColor");
    }

    public bool checkColor(GameObject obj, bool crossLimit) {
        Block block = obj.GetComponent<Block>();
        Color rec = block.Color;
        bool correctColor = rec == actualColor;

        if ((correctColor && crossLimit) || (!correctColor && !crossLimit) && rec != Color.white) {
            if(lives > 0){
                lives--;
                ui.toggleHeart();
                ui.breakHeart();
                blink();
                comboScore = 0;
            } else {
                gameOver();
            }
        } else if (!crossLimit){
            roundScore += (int)(obj.GetComponent<Block>().value * universeBonus);
            comboScore += 1;
            ui.updateScore(roundScore);
        }
        ui.updateCombo(comboScore);
        block.stop();
        returnToPool(obj);
        return correctColor || rec == Color.white;
    }

    public void showCombo(Vector3 pos, int value){
        ui.showCombo(pos, value);
    }

    public void showFeed(Vector3 pos, string value) {
        ui.showFeed(pos, value);
    }

    public int relief(GameObject reliefBlock) {
        int totalRelieved = 0;

        checkColor(reliefBlock, false);

        GameObject[] aux = pool.getInGameObj();

        foreach (GameObject go in aux)
        {
            Block block = go.GetComponent<Block>();
            if (block.Color == actualColor)
            {
                int oldRoundScore = roundScore;
                checkColor(go, false);
                totalRelieved += roundScore-oldRoundScore;
            }
        }

        return totalRelieved;
    }

    public bool getLife(GameObject lifeObj) {
        bool answer = false;
        if (checkColor(lifeObj, false))
        {
            if (lives < maxLives)
            {
                lives += 1;
                ui.toggleHeart();
                ui.addHeart();
            }
            answer = true;
        }
        return answer;
    }

    public void changeUniverse(GameObject universeObj) {
        UniverseBlock block = universeObj.GetComponent<UniverseBlock>();
        ColorPack newColorPack = block.universesColor[Random.Range(0, block.universesColor.Length)];
        availableColors = newColorPack.colors;
        universeBonus = newColorPack.bonus;
        chooseColor();
        pool.standBy(universeObj);
        block.stop();
        StartCoroutine(getAllWhite(true));
        StartCoroutine(returnDefaultColors(universeObj));
    }

    public void increaseSpeed() {
        if (gameSpeed < 2.3)
        {
            gameSpeed += 0.1f;
            updateInGameSpeed();
        }
    }

    public void updateInGameSpeed() {
        GameObject[] aux = pool.getInGameObj();
        foreach (GameObject go in aux) {
            go.GetComponent<Block>().updateVelocity();
        }
    }

    IEnumerator returnDefaultColors(GameObject universeObj) {
        yield return new WaitForSeconds(22);
        int countdown = 3;

        while (countdown > 0) {
            ui.countdown(countdown + "");
            countdown--;
            yield return new WaitForSeconds(1);
        }

        ui.countdown("");

        availableColors = defaultColors.colors;
        universeBonus = defaultColors.bonus;
        chooseColor();
        returnToPool(universeObj);
        StartCoroutine(getAllWhite(false));
        increaseSpeed();
    }

    IEnumerator getAllWhite(bool downUp) {
        GameObject[] aux = new GameObject[pool.inGameObjects.Count];
        pool.inGameObjects.CopyTo(aux);

        if (downUp)
        {
            System.Array.Reverse(aux);
        }

        foreach (GameObject go in aux)
        {
            Block randBlock = go.GetComponent<Block>();

            if (!(randBlock is RandomBlock))
            {
                randBlock.stop();
                returnToPool(randBlock.gameObject);
            }
            else
            {
                randBlock.Color = Color.white;
                randBlock.GetComponent<SpriteRenderer>().color = Color.white;
            }
            randBlock.InGame = false;
            yield return new WaitForSeconds(0.01f);
        }
    }
    

    public void blink() {
        GameObject[] aux = new GameObject[pool.inGameObjects.Count];
        pool.inGameObjects.CopyTo(aux);

        foreach (GameObject go in aux) {
            StartCoroutine(blink(go.GetComponent<SpriteRenderer>()));
        }
    }

    IEnumerator blink(SpriteRenderer obj) {
        for (int i = 0; i < 2; i++)
        {
            obj.color -= new Color(0,0,0,0.8f);
            yield return new WaitForSeconds(0.1f);
            obj.color += new Color(0, 0, 0, 0.8f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void gameOver() {
        inGame = false;
        StartCoroutine(gameOverEnum());
    }

    IEnumerator gameOverEnum() {
        if (roundScore > PlayerPrefs.GetInt("score"))
        {
            PlayerPrefs.SetInt("score", roundScore);
        }

        if (comboScore > PlayerPrefs.GetInt("combo"))
        {
            PlayerPrefs.SetInt("combo", comboScore);
        }

        spawner.stop();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Feed")) {
            Destroy(go);
        }

        Time.timeScale = 0;

        float pauseEndTime = Time.realtimeSinceStartup + 1;

        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return null;
        }

        ui.showGameOver();
    }

    public void returnToPool(GameObject obj) {
        obj.GetComponent<Block>().stop();
        pool.returnToPool(obj);
    }
}
