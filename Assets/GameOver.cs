using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private static GameOver i;
    void Awake() {
        i = this;
        Button button = gameObject.GetComponentInChildren<Button>();
        button.onClick.AddListener(this.OnClick);
        Hide();
    }

    void Show() {
        gameObject.SetActive(true);
    }

    void Hide() {
        gameObject.SetActive(false);
    }

    public static void ShowStatic() {
        i.Show();
    }
    void OnClick() {
        Loader.Load(Loader.Scene.GameScene);
    }

}
