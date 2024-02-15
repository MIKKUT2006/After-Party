using Assets.Entities.Player;
using ModernProgramming;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Volume volume;
    ChannelMixer ChannelMixer;
    [SerializeField] private TextMeshProUGUI timer;
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "_MenuScene")
        {
            volume.profile.TryGet(out ChannelMixer);

            if (PlayerPrefsExtended.GetInt("Kills", 0) > 0)
            {
                // Устанавливаем значения красного канала
                ChannelMixer.redOutGreenIn.value = 200;
                ChannelMixer.redOutRedIn.value = 200;
                ChannelMixer.redOutBlueIn.value = 200;

                // Показываем красный канал
                ChannelMixer.active = true;
                //Debug.Log("Вы убили");
            }
        }

        

        if (PlayerData.speeedrun == true)
        {
            Debug.Log(PlayerData.time);
            //timer.text = "time: " + PlayerData.time;
            timer.text = $"time: {PlayerData.time}sec";
        }
        else
        {
            timer.gameObject.SetActive(false);
        }
    }
    public GameObject selectGamePanel;
    public void ExitGame() // Метод выхода из игры
    {
        Application.Quit();
    }
    public void PlayGame() // Метод выхода из игры
    {
        if (selectGamePanel.activeSelf == false)
        {
            selectGamePanel.SetActive(true);
        }
        
    }

    public void NewGame()
    {
        PlayerData.speeedrun = false;

        SceneManager.LoadScene("FallenCity");
    }

    public void NewGameSpeedRun()
    {
        PlayerData.speeedrun = true;
        SceneManager.LoadScene("FallenCity");
    }

    public void LoadGame()
    {
        PlayerData.speeedrun = false;
        PlayerData.isNewGame = false;
        SceneManager.LoadScene("FallenCity");
    }
}
