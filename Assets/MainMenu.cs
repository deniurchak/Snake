using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    private Button play;
    private Button quit;

    // Start is called before the first frame update
    void Start()
    {
        play = GameObject.Find("PlayButton").GetComponent<Button>();
        play.onClick.AddListener(() => Loader.Load(Loader.Scene.GameScene));
        quit = GameObject.Find("QuitButton").GetComponent<Button>();
        quit.onClick.AddListener(() => Application.Quit());
    }
}
