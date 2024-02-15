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
                // ������������� �������� �������� ������
                ChannelMixer.redOutGreenIn.value = 200;
                ChannelMixer.redOutRedIn.value = 200;
                ChannelMixer.redOutBlueIn.value = 200;

                // ���������� ������� �����
                ChannelMixer.active = true;
                //Debug.Log("�� �����");
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
    public void ExitGame() // ����� ������ �� ����
    {
        Application.Quit();
    }
    public void PlayGame() // ����� ������ �� ����
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
