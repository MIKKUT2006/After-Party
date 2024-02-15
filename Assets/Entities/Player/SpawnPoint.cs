using Assets.Entities.Player;
using ModernProgramming;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private AudioSource checkpointSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerData.spawnPoint = gameObject.transform.position;
            PlayerData.spawnPoint.y += 1;
            checkpointSound.Play();

            // ���������� ������
            PlayerPrefsExtended.SetVector3("PlayerPosition", PlayerData.spawnPoint);

            PlayerPrefsExtended.SetFloat("TestPlayerY", collision.transform.position.y);
            PlayerPrefsExtended.SetVector3("cordstest", new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z));
            Debug.Log("������� ������ " + collision.transform.position);

            // ���������� ����������� ����������
            PlayerPrefsExtended.SetInt("CrystalsCount", PlayerData.Crystals.Count);
            PlayerPrefsExtended.SetInt("DoorsCount", PlayerData.Doorname.Count);
            PlayerPrefsExtended.SetInt("Deaths", PlayerData.Deaths);


            PlayerPrefsExtended.SetInt("Kills", PlayerData.KillAll);

            PlayerPrefsExtended.SetFloat("Y", PlayerData.spawnPoint.y);

            // ��������� ��������� !!!!
            for (int i = 0; i < PlayerData.Crystals.Count; i++)
            {
                PlayerPrefsExtended.SetVector3(i.ToString(), PlayerData.Crystals[i]);
                Debug.Log("����������� ���������� ��������� = " + PlayerPrefsExtended.GetVector3(i.ToString(), PlayerData.Crystals[i]));
            }

            // ��������� �������� �����
            for (int i = 0; i < PlayerData.Doorname.Count; i++)
            {
                PlayerPrefsExtended.SetString(i.ToString(), PlayerData.Doorname[i]);   
            }

            // ���������� ����������� ���������
            PlayerPrefsExtended.SetBool("Light", PlayerData.Light);
            PlayerPrefsExtended.SetBool("Mechanism", PlayerData.Mechanism);
            PlayerPrefsExtended.SetBool("Heart", PlayerData.Heart);

            // ���������� ��������� �������
            PlayerPrefsExtended.SetBool("WhiteWire", PlayerData.WhiteWire);
            PlayerPrefsExtended.SetBool("RedWire", PlayerData.RedWire);
            PlayerPrefsExtended.SetBool("BlueWire", PlayerData.BlueWire);

            PlayerPrefsExtended.SetBool("CurseDeath", PlayerData.CurseDeath);


            PlayerPrefsExtended.Save();

            //Debug.Log(PlayerPrefsExtended.GetVector3("PlayerPosition", Vector3.zero));
            //Debug.Log(PlayerPrefsExtended.GetFloat("Y", 0));

            // �����
            //Debug.Log("���������� �� Y" + (PlayerPrefsExtended.GetFloat("TestPlayerY", 0)));
            Debug.Log("����������" + PlayerPrefsExtended.GetVector3("cordstest", Vector3.zero));
        }
    }

}
