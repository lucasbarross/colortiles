using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Text Score;
    public Text HighScore;
    public Text comboHUD;
    public GameObject combo;
    public GameObject valor;
    public GameObject heartUI;
    public GameObject heartBroken;
    public GameObject oneUpHeart;
    public GameObject preview;
    public GameObject gameOverMenu;
    public Text lastScore;
    public Text lastCombo;
    public GameObject hud;
    public TextMesh counter;

    public void showCombo(Vector3 pos, int combo) {
        GameObject comboObj = Instantiate(this.combo);
        comboObj.transform.GetChild(0).GetComponent<TextMesh>().text = "COMBO \n" + "+" + combo;
        comboObj.transform.position = new Vector3(pos.x, pos.y, 0);
    }

    public void showFeed(Vector3 pos, string value)
    {
        GameObject valueObj = Instantiate(this.valor);
        valueObj.transform.GetChild(0).GetComponent<TextMesh>().text = value;
        valueObj.transform.position = new Vector3(pos.x, pos.y, 0);
    }

    public void showGameOver() {
        hud.SetActive(false);
        gameOverMenu.SetActive(true);
        lastScore.text = Score.text;
        lastCombo.text = comboHUD.text;

    }

    public void reset() {
        updateCombo(0);
        updateScore(0);
        toggleHeart();
    }

    public void breakHeart() {
        Instantiate(heartBroken);
    }

    public void addHeart() {
        Instantiate(oneUpHeart);
    }

    public void countdown(string count) {
        counter.text = count;
    }

    public void updateScore(int roundScore) {
        this.Score.text = "" + roundScore;
    }

    public void updateCombo(int comboScore) {
        comboHUD.text = "x" + comboScore;
    }

    public void changePreviewColor(Color color) {
        preview.GetComponent<Image>().color = color;
    }

    public void updateHighScore(int highScore)
    {
        this.HighScore.text = "" + highScore;
    }

    public void toggleHeart() {
        heartUI.SetActive(!heartUI.activeSelf);
    }
}
