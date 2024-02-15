using Assets.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using System;
using ModernProgramming;
using DialogueEditor;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Игрок")]
    private Rigidbody2D rb;
    public int PlayerSpeed = 5;
    public float JumpSpeed;
    public bool OnGround;
    public Camera Camera;
    public Transform PlayerTransform;
    public Animator Animation;
    public GameObject playergameObject;

    [Header("Прыжки")]
    public bool isGround;
    public float rayDistance = 0.6f;
    public float JumpForce = 5.0f;
    public Transform JumpZone;
    public LayerMask layerMask;
    private bool isFlip = false;
    //private float checkRadius = 0.2f;

    [Header("Планер")]
    public bool Planer = PlayerData.Planer;
    private bool onDrone = false;
    public bool OnGroundTest = PlayerData.OnGround;
    public AudioSource OpenPlanerSound;

    [Header("Дроны")]
    public Rigidbody2D DroneRb;
    private float DroneVelocity;

    [Header("Паника")]
    public AudioSource Panic;
    public Volume volume;
    ChromaticAberration chromatic;
    LensDistortion lens;
    FilmGrain filmgrain;
    float _duration;

    // Список Эмбиента
    public List<AudioClip> AllAmbientlist = new List<AudioClip>();

    // Дробовик и диалоги с ним
    public GameObject boomstick;
    public GameObject conversation;

    // Диалоги
    public NPCConversation nPCConversation;
    public Conversation conver;
    public ConversationManager conv;
    Parameter par;
    public EditableConversation editConversation;

    [SerializeField] TMPro.TextMeshPro crystalsCountText;
    [SerializeField] TMPro.TextMeshPro CollectedItemText;

    [SerializeField] GameObject Pause;

    [SerializeField] TextMeshPro Timer;


    // Булевая переменная (ТОЛЬКО ДЛЯ ТЕСТИРОВАНЯ)
    public bool test;

    // Очень странное и не используемое (НА УДАЛЕНИЕ)
    //private int cameraAngle = 0;

    void Start()
    {
        _duration = Panic.clip.length;
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент Rigidbody 2d у нашего игрока

        PlayerData.Ambientlist = AllAmbientlist;

        // Перебираем все кристаллы на сцене если мы начали новую игру
        if (PlayerData.isNewGame == true)
        {
            PlayerData.Crystals.Clear();
        }
        
        volume.profile.TryGet(out chromatic);
        volume.profile.TryGet(out lens);
        volume.profile.TryGet(out filmgrain);

        crystalsCountText.text = $"{PlayerData.Crystals.Count}/7";

        if (PlayerData.speeedrun == true)
        {
            StartCoroutine(TimerText());
            Timer.gameObject.SetActive(true);
        }

        Debug.Log(PlayerData.speeedrun);
    }

    private int SecondCount = 0;
    private IEnumerator TimerText()
    {
        SecondCount += 1;
        yield return new WaitForSeconds(1);
        Timer.text = "Time: " + SecondCount;
        PlayerData.time = SecondCount;
        StartCoroutine(TimerText());
    }

    private IEnumerator CollectItem(string ItemName)
    {
        Debug.Log("ITEM");
        CollectedItemText.text = ItemName;
        CollectedItemText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        CollectedItemText.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Тестовая кнопка для тестов, надо же
        //if (Input.GetKeyDown("t"))
        //{
        //    PlayerData.KillAll = 2;
        //    PlayerPrefsExtended.SetInt("Kills", PlayerData.KillAll);
        //    PlayerPrefsExtended.Save();
        //}

        if (Input.GetKeyDown("escape"))
        {
            Pause.SetActive(true);
        }

        test = PlayerData.Panic;

        if (PlayerData.Panic == true)
        {
            if (Panic.isPlaying == false)
            {
                Animation.SetBool("Panic", true);
                Panic.Play();
                chromatic.intensity.value = 1f;
                //lens.intensity.value = -0.86f;
                filmgrain.intensity.value = 1f;

                StartCoroutine(WaitForSound());
            }
                
        }

        //if (Panic.isPlaying == false)
        //{
        //    PlayerData.Panic = false;
        //    Animation.SetBool("Panic", false);
        //    chromatic.intensity.value = 0.06f;
        //    filmgrain.intensity.value = 0.06f;
        //}
    }

    IEnumerator WaitForSound()
    {
        yield return new WaitForSeconds(_duration);
        Panic.Stop();
        PlayerData.Panic = false;
        Animation.SetBool("Panic", false);
        chromatic.intensity.value = 0.06f;
        filmgrain.intensity.value = 0.06f;
        //lens.intensity.value = -0.11f;

    }

    private void FixedUpdate()
    {
        DroneVelocity = DroneRb.velocity.x;
        //if (Input.GetKeyDown("y"))
        //{
        //    SceneManager.LoadScene("_MenuScene");
        //}

        //if (Input.GetKeyDown("q"))
        //{
        //    Shoot.SetActive(true);
        //    Shoot.GetComponent<ParticleSystem>().Play();
        //}

        //if (Shoot.GetComponent<ParticleSystem>().isStopped)
        //{
        //    Shoot.SetActive(false);
        //}

        // Быстрое сохранение (ТОЛЬКО ДЛЯ ТЕСТИРОВАНИЯ)
        if (Input.GetKeyDown("h"))
        {
            PlayerData.spawnPoint = gameObject.transform.position;
            PlayerData.spawnPoint.y += 1;

            // Сохранение данных
            PlayerPrefsExtended.SetVector3("PlayerPosition", PlayerData.spawnPoint);

            PlayerPrefsExtended.SetFloat("TestPlayerY", transform.position.y);
            PlayerPrefsExtended.SetVector3("cordstest", new Vector3(transform.position.x, transform.position.y, transform.position.z));
            Debug.Log("Позиция игрока " + transform.position);


            PlayerPrefsExtended.SetInt("CrystalsCount", PlayerData.Crystals.Count);
            PlayerPrefsExtended.SetFloat("Y", PlayerData.spawnPoint.y);
            for (int i = 0; i < PlayerData.Crystals.Count; i++)
            {
                PlayerPrefsExtended.SetVector3(i.ToString(), PlayerData.Crystals[i]);
                Debug.Log("Сохранились коодринаты кристалла = " + PlayerPrefsExtended.GetVector3(i.ToString(), PlayerData.Crystals[i]));
            }

            PlayerPrefsExtended.Save();
            Debug.Log("Координаты" + PlayerPrefsExtended.GetVector3("cordstest", Vector3.zero));
        }

        Planer = PlayerData.Planer;
        OnGroundTest = PlayerData.OnGround;

        Camera.transform.position = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, -15);

        //---------------------------------
        // Управление персонажем
        if (Animation.GetBool("Death") != true && PlayerData.Panic == false && PlayerData.isDialogue == false)
        {
            if (PlayerData.gravityRotate == false)
            {
                if (onDrone == true)
                {
                    rb.velocity = new Vector2(Input.GetAxis("Horizontal") * PlayerSpeed + DroneVelocity, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(Input.GetAxis("Horizontal") * PlayerSpeed, rb.velocity.y);
                }
                // Скорость устанавливаем в новый вектор, где первое ставим по x и по y ставим скорость y

                if (Input.GetAxis("Horizontal") < 0 && isFlip == false)
                {
                    Flip();
                }
                if (Input.GetAxis("Horizontal") > 0 && isFlip == true)
                {
                    Flip();
                }
            }
            else
            {
                rb.velocity = new Vector2(-(Input.GetAxis("Horizontal") * PlayerSpeed), rb.velocity.y);

                if (Input.GetAxis("Horizontal") < 0 && isFlip == false)
                {
                    Flip();
                }
                if (Input.GetAxis("Horizontal") > 0 && isFlip == true)
                {
                    Flip();
                }
            }
        }
        else rb.velocity = new Vector2(0, rb.velocity.y);
        

        if (PlayerData.gravityRotate == true)
        {
            Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, Quaternion.Euler(0,0,180), Time.deltaTime * 50f); // Плавно поворачиваем камеру на 180 градусов по оси z
            rb.gravityScale = -1f;
            playergameObject.transform.eulerAngles = new Vector3(0, 0, 180);

        }
        else
        {
            // Проверка, что лифт душ неактивен, и при этом мы не летаем на планере
            if (PlayerData.soulElevator == false && Animation.GetBool("Planer") == false)
            {
                if (PlayerData.OnGround == true)
                {
                    //rb.gravityScale = 1f;
                    gameObject.transform.eulerAngles = Vector3.zero;
                }
                Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 50f); // Плавно возвращаем камеру в изначальный наклон по оси z
                playergameObject.transform.eulerAngles = Vector3.zero;
                rb.gravityScale = 1f;
            }
            
        }
        

        if (rb.velocity.x != 0 && Math.Abs(rb.velocity.x) > Math.Abs(DroneVelocity))
        {
            Animation.SetBool("isRun", true);
        }
        else
        {
            Animation.SetBool("isRun", false);
        }

        if (rb.velocity.y > 7)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, 7);
        }

        // Если анимация = Планер, то мы наклоняем персонажа
        if (Animation.GetBool("Planer") == true)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, -7 * Input.GetAxis("Horizontal"), 40);
        }
        else
        {
            PlayerSpeed = 5;
        }

        if (PlayerData.Planer == true)
        {
            rb.AddForce(new Vector2(0, 5.0f), ForceMode2D.Impulse);
            PlayerData.Planer = false;
            rb.gravityScale = 0.2f;
            PlayerSpeed = 7;
            StartCoroutine(wait());
        }
    }

    void CameraRotate()
    {
        int grad = 0;

        while (grad <= 180)
        {
            Camera.transform.Rotate(0, 0, 1);
            grad++;
        }
    }
    // !!! Скорость не становиттся преждней
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
        if (PlayerData.OnGround == true)
        {
            rb.gravityScale = 1f;
            PlayerSpeed = 5;
            PlayerData.Planer = false;
            Animation.SetBool("Planer", false);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Drone")
        {
            //transform.parent = null;
            onDrone = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Planer":
                PlayerData.Planer = true;
                PlayerData.OnGround = false;
                Animation.SetBool("Planer", true);
                OpenPlanerSound.Play();
                break;
            case "Key":
                PlayerData.keysTags.Add(collision.gameObject.name);
                StartCoroutine(CollectItem(collision.gameObject.name));
                //CollectItem(collision.gameObject.name);
                Destroy(collision.gameObject);
                break;
            case "Crystal":
                PlayerData.Crystals.Add(collision.transform.position);
                crystalsCountText.text = $"{PlayerData.Crystals.Count.ToString()}/7";
                //Debug.Log("В список были записаны координаы " + collision.transform.position);
                Destroy(collision.gameObject);
                break;
            case "Drone":
                onDrone = true;
                DroneRb = collision.gameObject.GetComponent<Rigidbody2D>();
                break;
            case "Red Wire":
                PlayerData.RedWire = true;
                StartCoroutine(CollectItem(collision.gameObject.name));
                Destroy(collision.gameObject);
                break;
            case "Blue Wire":
                PlayerData.BlueWire = true;
                StartCoroutine(CollectItem(collision.gameObject.name));
                Destroy(collision.gameObject);
                break;
            case "White Wire":
                PlayerData.WhiteWire = true;
                StartCoroutine(CollectItem(collision.gameObject.name));
                Destroy(collision.gameObject);
                break;
            //case "Boomstick":
            //    PlayerData.Boomstick = true;
            //    Destroy(collision.gameObject);
            //    boomstick.gameObject.SetActive(true);
            //    break;
            case "Happy crystal":
                PlayerData.HappyCrystal = true;
                Destroy(collision.gameObject);
                break;
            default:
                break;
        }

        // Проверка на нахождение на земле
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Animation.SetBool("onGround", true);
            //PlayerData.OnGround = true;
            Animation.SetBool("Planer", false);
        }
        
        
        //// Включаем планирование на планере
        //if (collision.gameObject.tag == "Planer")
        //{
            
        //}

        // Подбираем ключ от города
        //if (collision.gameObject.tag == "Key")
        //{
        //    //collision.gameObject.name
        //    PlayerData.keysTags.Add(collision.gameObject.name);
        //    Destroy(collision.gameObject);
        //}

        // Подбор кристаллов
        //if (collision.gameObject.tag == "Crystal")
        //{
        //    PlayerData.Crystals.Add(collision.transform.position);
        //    //Debug.Log("В список были записаны координаы " + collision.transform.position);
        //    Destroy(collision.gameObject);
        //}

        //if (collision.gameObject.tag == "Drone")
        //{
        //    //rb.velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
        //    //transform.SetParent(collision.transform);
        //    onDrone = true;
        //    DroneRb = collision.gameObject.GetComponent<Rigidbody2D>();
        //    //Debug.Log("Вы встали на платформу");
        //}

        // Меняем гравитацию
        //if (collision.gameObject.tag == "RotateZone")
        //{
        //    if (PlayerData.gravityRotate == false)
        //    {
        //        PlayerData.gravityRotate = true;
        //    }
        //    else
        //    {
        //        PlayerData.gravityRotate = false;
        //    }
        //}

        //----------------------------------------------------------------------
        // Сбор проводов на фабрике
        //if (collision.gameObject.tag == "Blue Wire")
        //{
        //    PlayerData.BlueWire = true;
        //    Destroy(collision.gameObject);
        //}

        //if (collision.gameObject.tag == "Red Wire")
        //{
        //    PlayerData.RedWire = true;
        //    Destroy(collision.gameObject);
        //}

        //if (collision.gameObject.tag == "White Wire")
        //{
        //    PlayerData.WhiteWire = true;
        //    Destroy(collision.gameObject);
        //}
        //----------------------------------------------------------------------
        // Подбор дробовика
        //if (collision.gameObject.tag == "Boomstick")
        //{
        //    PlayerData.Boomstick = true;
        //    Destroy(collision.gameObject);
        //    //GameObject.FindGameObjectWithTag("BoomstickItem").gameObject.SetActive(true);
        //    boomstick.gameObject.SetActive(true);

        //    //conversation.SetBool("Boomstick", true);
            
        //    //conversation.GetComponent<Conversation>().SetBool("Boomstick", true, out eParamStatus paramStatus);
        //    //Debug.Log(conversation.GetComponent<Conversation>().GetBool("Boomstick", out eParamStatus p));
            
            
        //}
    }



    IEnumerator Rotate()
    {
        yield return new WaitForSeconds(3);
        
    }

    void Flip()
    {
        isFlip = !isFlip;
        Vector3 scaler = transform.localScale;

        scaler.x *= -1;

        transform.localScale = scaler;
    }
}