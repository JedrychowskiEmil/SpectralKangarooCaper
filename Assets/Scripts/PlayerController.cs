using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public int playerJump = 7;
    public int playerSpeed = 6;

    public int floorsToSpriteChange = 4;
    private int currentPassedFloors = 0;
    private int nextLevel = 1;
    private uint score;

    private Rigidbody2D rb;
    public GameObject tmpCamera;
    public Animator animator;

    //UI
    public TMP_Text UIscore;
    public TMP_Text UIscoreAfterDeath;
    public TMP_Text UIgameover;
    public Button UIplayAgain;
    public Button UIgoToMenu;

    public Sprite[] floors;
    public Sprite[] backgrounds;
    public Sprite[] bushes;
    public Sprite[] clouds;
    public Sprite[] grasses;
    public Sprite[] flowers;
    public Sprite[] mazeBoxesDown;
    public Sprite[] mazeBoxesTop;

    public GameObject[] mazes;

    private bool isAlive;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        score = 0;
        isAlive = true;

        UIscore.gameObject.SetActive(true);
        UIscoreAfterDeath.gameObject.SetActive(false);
        UIgameover.gameObject.SetActive(false);
        UIgoToMenu.gameObject.SetActive(false);
        UIplayAgain.gameObject.SetActive(false);
        UIgoToMenu.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (isAlive)
        {
           UIscore.text = string.Format("{0:0000000}", score);

            if (Input.touchCount > 0)
            {
                rb.gravityScale = 3;
                animator.SetInteger("JumpState", 1);
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    rb.gravityScale = 1;
                    rb.velocity = new Vector2(rb.velocity.x, playerJump);
                    animator.SetInteger("JumpState", 0);
                    SoundController.playSound("jump");
                }

            }
        }
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
            tmpCamera.transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Respawn")
        {
            float x = collision.transform.position.x + 18.75f;
            float y = collision.transform.position.y;
            collision.transform.position = new Vector2(x, y);

            score++;
            SoundController.playSound("score");
            currentPassedFloors++;
            spawnMaze(x, y);
            if (currentPassedFloors >= floorsToSpriteChange)
            {
                if(currentPassedFloors - floorsToSpriteChange == 2)
                {
                    currentPassedFloors = 0;
                    nextLevel = (nextLevel + 1) % floors.Length;
                }
                else
                {
                    for(int i = 0; i < collision.gameObject.transform.childCount; i++)
                    {
                        GameObject child = collision.gameObject.transform.GetChild(i).gameObject;
                        switch (child.tag)
                        {
                            case "Floor":
                                child.GetComponent<SpriteRenderer>().sprite = floors[nextLevel];
                                break;
                            case "Background":
                                child.GetComponent<SpriteRenderer>().sprite = backgrounds[nextLevel];
                                break;
                            case "Bush":
                                child.GetComponent<SpriteRenderer>().sprite = bushes[nextLevel];
                                break;
                            case "Cloud":
                                child.GetComponent<SpriteRenderer>().sprite = clouds[nextLevel];
                                break;
                            case "Grass":
                                child.GetComponent<SpriteRenderer>().sprite = grasses[nextLevel];
                                break;
                            case "Flower":
                                child.GetComponent<SpriteRenderer>().sprite = flowers[nextLevel];
                                break;
                        }

                    }
                }

            }
        }


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            isAlive = false;
            SoundController.playSound("hit");
            UIscoreAfterDeath.text = "Score " + score;
            UIscore.gameObject.SetActive(false);
            UIscoreAfterDeath.gameObject.SetActive(true);
            UIgameover.gameObject.SetActive(true);
            UIgoToMenu.gameObject.SetActive(true);
            UIplayAgain.gameObject.SetActive(true);
            UIgoToMenu.gameObject.SetActive(true);
        }
    }

    private void spawnMaze(float x, float y)
    {
        if (currentPassedFloors == floorsToSpriteChange)
        {
            foreach (GameObject maze in mazes)
            {
                for (int m = 0; m < maze.transform.childCount; m++)
                {
                    GameObject child = maze.transform.GetChild(m).gameObject;
                    switch (child.gameObject.tag)
                    {
                        case "BoxDown":
                            child.GetComponent<SpriteRenderer>().sprite = mazeBoxesDown[nextLevel];
                            break;
                        case "BoxTop":
                            child.GetComponent<SpriteRenderer>().sprite = mazeBoxesTop[nextLevel];
                            break;
                    }
                    
                }
            }

        }


        GameObject newMaze = Instantiate(mazes[Random.Range(0, mazes.Length)]);
        newMaze.transform.position = new Vector2(x, y);
    }

}
