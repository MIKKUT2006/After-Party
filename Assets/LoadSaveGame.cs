using Assets.Entities.Player;
using ModernProgramming;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadSaveGame : MonoBehaviour
{
    
    void Start()
    {
        // Создаем список из всех кристаллов
        List<GameObject> crystals = GameObject.FindGameObjectsWithTag("Crystal").ToList();
        if (PlayerData.isNewGame == false)
        {
            PlayerData.Crystals.Clear();
            //Debug.Log(PlayerPrefsExtended.GetInt("CrystalsCount", 0));

            // Записываем в собранные кристаллы все собранные кристаллы в сохранении
            for (int i = 0; i < PlayerPrefsExtended.GetInt("CrystalsCount", 0); i++)
            {
                PlayerData.Crystals.Add(PlayerPrefsExtended.GetVector3(i.ToString(), Vector3.zero));
                //Debug.Log(PlayerPrefsExtended.GetVector3(i.ToString(), Vector3.zero));
            }

            // Загружаем открытые двери
            for (int i = 0; i < PlayerPrefsExtended.GetInt("DoorsCount", 0); i++)
            {
                PlayerData.Doorname.Add(PlayerPrefsExtended.GetString(i.ToString(), " "));
            }

            //Debug.Log("Позиция игрока" + PlayerPrefsExtended.GetVector3("PlayerPosition", Vector3.zero));
            transform.position = PlayerPrefsExtended.GetVector3("PlayerPosition", Vector3.zero);

            // Устанавливаем собранные элементы (собраны или нет)
            PlayerData.Light = PlayerPrefsExtended.GetBool("Light", PlayerData.Light);
            PlayerData.Mechanism = PlayerPrefsExtended.GetBool("Mechanism", PlayerData.Mechanism);
            PlayerData.Heart = PlayerPrefsExtended.GetBool("Heart", PlayerData.Heart);

            // Загружаем собранные провода
            PlayerData.RedWire = PlayerPrefsExtended.GetBool("RedWire", PlayerData.RedWire);
            PlayerData.WhiteWire = PlayerPrefsExtended.GetBool("WhiteWire", PlayerData.WhiteWire);
            PlayerData.BlueWire = PlayerPrefsExtended.GetBool("BlueWire", PlayerData.BlueWire);

            // Загружаем количество смертей
            PlayerData.Deaths = PlayerPrefsExtended.GetInt("Deaths", 0);

            // Загружаем интенсивность света
            PlayerData.lightIntensivity = PlayerPrefsExtended.GetFloat("LightIntensivity", PlayerData.lightIntensivity);

            if (PlayerData.RedWire == true)
            {
                Destroy(GameObject.FindGameObjectWithTag("Red Wire"));
            }

            if (PlayerData.WhiteWire == true)
            {
                Destroy(GameObject.FindGameObjectWithTag("White Wire"));
            }

            
            PlayerData.CurseDeath = PlayerPrefsExtended.GetBool("CurseDeath", false);

            // На данный момент его нет на уровне и он получается диалогом
            //if (PlayerData.BlueWire == true)
            //{
            //    Destroy(GameObject.FindGameObjectWithTag("Blue Wire"));

            //}

            // Уничтожаем собранные кристаллы
            for (int i = 0; i < crystals.Count; i++)
            {
                //Debug.Log("Количество собранных " + crystals.Count);

                foreach (Vector3 j in PlayerData.Crystals)
                {
                    //Debug.Log("Сохраненная позиция" + j);
                    //Debug.Log("Позиция кристалла" + crystals[i].transform.position);
                    if (crystals[i].transform.position == j)
                    {
                        // здесь игра не может найти кристалл
                        //Debug.Log("Кристалл удален");
                        Destroy(crystals[i]);
                    }
                }
            }
        }
    }
}
