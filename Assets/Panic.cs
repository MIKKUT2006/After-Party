using Assets.Entities.Player;
using ModernProgramming;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Panic : MonoBehaviour
{
    [SerializeField] Image _image;
    private bool end = false;
    [SerializeField] AudioSource ambient;
    [SerializeField] GameObject pause;
    public void SetPanic()
    {
        PlayerData.Panic = true;
    }

    public void Kill()
    {
        PlayerData.KillAll += 1;
        PlayerPrefsExtended.SetInt("Kills", PlayerData.KillAll);
        PlayerPrefsExtended.Save();
    }

    public void GiveBoomstick()
    {
        PlayerData.Boomstick = true;
        Destroy(gameObject);
        GameObject.FindGameObjectWithTag("BoomstickItem").gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void setGoodEnding()
    {
        PlayerData.isGoodEnding = true;
    }

    public void ShowAuthors()
    {
        //SceneManager.LoadScene("End_Game");
        end = true;
    }

    Color color = new Color(0, 0, 0, 0);
    private void FixedUpdate()
    {
        if (end == true)
        {
            _image.enabled = true;
            _image.color = Color.Lerp(_image.color, Color.black, 0.5f * Time.deltaTime);
            ambient.volume -= 0.1f;
            if (_image.color.a >= 0.95f)
            {
                _image.color = Color.black;
                SceneManager.LoadScene("End_Game");
            }
        }
        
    }

    public void SetKillVar(string name)
    {
        
    }

    public void InMenu()
    {

        if (PlayerData.speeedrun == false)
        {
            //PlayerData.spawnPoint = gameObject.transform.position;
            //PlayerData.spawnPoint.y += 1;

            Vector3 PlayerVector = GameObject.FindGameObjectWithTag("Player").transform.position;

            // Сохранение данных
            PlayerPrefsExtended.SetVector3("PlayerPosition", PlayerVector);

            // Сохранение позиции игрока
            PlayerPrefsExtended.SetFloat("TestPlayerY", PlayerVector.y);
            PlayerPrefsExtended.SetVector3("cordstest", new Vector3(PlayerVector.x, PlayerVector.y, PlayerVector.z));
            //Debug.Log("Позиция игрока " + collision.transform.position);

            // Количество сохраненных кристаллов
            PlayerPrefsExtended.SetInt("CrystalsCount", PlayerData.Crystals.Count);
            PlayerPrefsExtended.SetInt("DoorsCount", PlayerData.Doorname.Count);

            PlayerPrefsExtended.SetInt("Kills", PlayerData.KillAll);

            PlayerPrefsExtended.SetFloat("Y", PlayerData.spawnPoint.y);

            // сохраняем кристаллы !!!!
            for (int i = 0; i < PlayerData.Crystals.Count; i++)
            {
                PlayerPrefsExtended.SetVector3(i.ToString(), PlayerData.Crystals[i]);
                Debug.Log("Сохранились коодринаты кристалла = " + PlayerPrefsExtended.GetVector3(i.ToString(), PlayerData.Crystals[i]));
            }

            // Сохраняем открытые двери
            for (int i = 0; i < PlayerData.Doorname.Count; i++)
            {
                PlayerPrefsExtended.SetString(i.ToString(), PlayerData.Doorname[i]);
            }

            // Сохранение подобранных элементов
            PlayerPrefsExtended.SetBool("Light", PlayerData.Light);
            PlayerPrefsExtended.SetBool("Mechanism", PlayerData.Mechanism);
            PlayerPrefsExtended.SetBool("Heart", PlayerData.Heart);

            // Запоминаем собранные провода
            PlayerPrefsExtended.SetBool("WhiteWire", PlayerData.WhiteWire);
            PlayerPrefsExtended.SetBool("RedWire", PlayerData.RedWire);
            PlayerPrefsExtended.SetBool("BlueWire", PlayerData.BlueWire);

            PlayerPrefsExtended.SetBool("CurseDeath", PlayerData.CurseDeath);
            PlayerPrefsExtended.SetInt("Deaths", PlayerData.Deaths);


            PlayerPrefsExtended.Save();

            //Debug.Log(PlayerPrefsExtended.GetVector3("PlayerPosition", Vector3.zero));
            //Debug.Log(PlayerPrefsExtended.GetFloat("Y", 0));

            // Тесты
            //Debug.Log("Координата по Y" + (PlayerPrefsExtended.GetFloat("TestPlayerY", 0)));
            Debug.Log("Координаты" + PlayerPrefsExtended.GetVector3("cordstest", Vector3.zero));

            PlayerPrefsExtended.SetFloat("LightIntensivity", PlayerData.lightIntensivity);

        }
        SceneManager.LoadScene("_MenuScene");

    }

    public void Continue()
    {
        pause.SetActive(false);
    }

    
}
