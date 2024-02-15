using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Entities.Player
{
    // Класс для хранения данных об игроке
    public struct PlayerData
    {
        // Диалог
        public static bool isDialogue = false;

        // Переменная для проверки новая ли игра
        public static bool isNewGame = true;

        // Поля класса игрока
        public static bool OnGround = false;
        public static bool Planer = false;

        // Неиспользуемые переменные
        //public static bool cityKey = false;
        //public static bool fabricKey = false;

        public static bool soulElevator = false;
        public static List<string> keysTags = new List<string>();
        public static bool gravityRotate = false;

        // Координаты для возрождения (используем Vector3 от Unity)
        public static UnityEngine.Vector3 spawnPoint = new UnityEngine.Vector3(-34.9f, -2.449999f, 0);

        // Массив позиций кристаллов
        public static List<UnityEngine.Vector3> Crystals = new List<UnityEngine.Vector3>();

        // Список фоновой музыки для воспроизведения
        public static List<AudioClip> Ambientlist = new List<AudioClip>();

        // Провода
        public static bool BlueWire = false;
        public static bool RedWire = false;
        public static bool WhiteWire = false;

        // Дробовик
        public static bool Boomstick = false;

        // Паника главного героя
        public static bool Panic = false;

        // Количество убитых
        public static byte KillAll;

        // Смерть существа в шахтах
        public static bool CurseDeath = false;

        // Кристалл счастья
        public static bool HappyCrystal = false;


        // Все Части для октрытия ГЛАЗА
        public static bool Light = false;
        public static bool Mechanism = false;
        public static bool Heart = false;

        // Плавное затухание эмбиента
        public static bool AmbientMusicStop = false;

        // Переменная хорошей концовки (влияет на титры)
        public static bool isGoodEnding = true;

        // Список открытых дверей
        public static List<string> Doorname = new List<string>();

        public static bool VoidDeath = false;

        public static int Deaths = 0;

        public static bool speeedrun = false;

        public static int time = 0;

        public static float lightIntensivity = 1f;
    }
}
