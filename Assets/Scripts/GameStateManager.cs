using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    private int currentImageIndex;

    public Image IntroScreen;
    public Image IntroScreenBG;
    public List<Sprite> IntroImages = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        StartIntro();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StartIntro()
    {
        IntroScreenBG.gameObject.SetActive(true);
        IntroScreen.gameObject.SetActive(true);
        IntroScreenBG.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        float scale = Screen.height / IntroImages[0].rect.size.y;
        IntroScreen.rectTransform.sizeDelta = IntroScreen.rectTransform.sizeDelta * scale;
        SetNextImage();
    }

    private void SetNextImage()
    {
        IntroScreen.sprite = IntroImages[currentImageIndex++];
        if (currentImageIndex < IntroImages.Count)
            Invoke("SetNextImage", 5);
        else
            Invoke("StopIntro", 5);
    }

    private void StopIntro()
    {
        IntroScreenBG.gameObject.SetActive(false);
        IntroScreen.gameObject.SetActive(false);
    }

    public void StartGameOver()
    {
        Application.Quit();
    }

    public void StartVictory()
    {
        Invoke("SwitchVictoryScene", 3);
    }

    private void SwitchVictoryScene()
    {
        SceneManager.LoadScene("VictoryScene");
    }
}
