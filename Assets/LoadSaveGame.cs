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
        // ������� ������ �� ���� ����������
        List<GameObject> crystals = GameObject.FindGameObjectsWithTag("Crystal").ToList();
        if (PlayerData.isNewGame == false)
        {
            PlayerData.Crystals.Clear();
            //Debug.Log(PlayerPrefsExtended.GetInt("CrystalsCount", 0));

            // ���������� � ��������� ��������� ��� ��������� ��������� � ����������
            for (int i = 0; i < PlayerPrefsExtended.GetInt("CrystalsCount", 0); i++)
            {
                PlayerData.Crystals.Add(PlayerPrefsExtended.GetVector3(i.ToString(), Vector3.zero));
                //Debug.Log(PlayerPrefsExtended.GetVector3(i.ToString(), Vector3.zero));
            }

            // ��������� �������� �����
            for (int i = 0; i < PlayerPrefsExtended.GetInt("DoorsCount", 0); i++)
            {
                PlayerData.Doorname.Add(PlayerPrefsExtended.GetString(i.ToString(), " "));
            }

            //Debug.Log("������� ������" + PlayerPrefsExtended.GetVector3("PlayerPosition", Vector3.zero));
            transform.position = PlayerPrefsExtended.GetVector3("PlayerPosition", Vector3.zero);

            // ������������� ��������� �������� (������� ��� ���)
            PlayerData.Light = PlayerPrefsExtended.GetBool("Light", PlayerData.Light);
            PlayerData.Mechanism = PlayerPrefsExtended.GetBool("Mechanism", PlayerData.Mechanism);
            PlayerData.Heart = PlayerPrefsExtended.GetBool("Heart", PlayerData.Heart);

            // ��������� ��������� �������
            PlayerData.RedWire = PlayerPrefsExtended.GetBool("RedWire", PlayerData.RedWire);
            PlayerData.WhiteWire = PlayerPrefsExtended.GetBool("WhiteWire", PlayerData.WhiteWire);
            PlayerData.BlueWire = PlayerPrefsExtended.GetBool("BlueWire", PlayerData.BlueWire);

            // ��������� ���������� �������
            PlayerData.Deaths = PlayerPrefsExtended.GetInt("Deaths", 0);

            // ��������� ������������� �����
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

            // �� ������ ������ ��� ��� �� ������ � �� ���������� ��������
            //if (PlayerData.BlueWire == true)
            //{
            //    Destroy(GameObject.FindGameObjectWithTag("Blue Wire"));

            //}

            // ���������� ��������� ���������
            for (int i = 0; i < crystals.Count; i++)
            {
                //Debug.Log("���������� ��������� " + crystals.Count);

                foreach (Vector3 j in PlayerData.Crystals)
                {
                    //Debug.Log("����������� �������" + j);
                    //Debug.Log("������� ���������" + crystals[i].transform.position);
                    if (crystals[i].transform.position == j)
                    {
                        // ����� ���� �� ����� ����� ��������
                        //Debug.Log("�������� ������");
                        Destroy(crystals[i]);
                    }
                }
            }
        }
    }
}
