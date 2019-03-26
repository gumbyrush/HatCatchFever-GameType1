using UnityEngine;
using System.Collections;
using System.Text;

public class HT_GameController : MonoBehaviour
{
    public ArrayList ballPositions;
    public Camera cam;
    public GameObject[] balls;
    public float timeLeft;
    public float changeX;
    public float ballDropPosition;
    public Vector3 spawnPosition;
    public Vector3 targetWidth;
    public GUIText timerText;
    public GameObject gameOverText;
    public GameObject restartButton;
    public GameObject splashScreen;
    public GameObject startButton;
    public HT_HatController hatController;

    private float maxWidth;
    private bool counting;

    // Use this for initialization
    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        float ballWidth = balls[0].GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - ballWidth;
        timerText.text = "TIME LEFT:\n" + Mathf.RoundToInt(timeLeft);
    }

    void FixedUpdate()
    {
        if (counting)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = 0;
            }
            timerText.text = "TIME LEFT:\n" + Mathf.RoundToInt(timeLeft);
        }
    }

    public void StartGame()
    {
        splashScreen.SetActive(false);
        startButton.SetActive(false);
        hatController.ToggleControl(true);
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2.0f);
        counting = true;
        changeX = 0.0f;
        ArrayList ballDropping = new ArrayList
        {
           4f,
           -4f

        };

        //trying To Get balls To Drop Sequentially 

        while (timeLeft > 0)
        {
            for (var i = 0; i < ballDropping.Count; i++)
            {
                ballDropPosition = (float)ballDropping[i];
                GameObject ball = balls[Random.Range(0, balls.Length)];
                Vector3 spawnPosition = new Vector3(
                    ballDropPosition,
                    transform.position.y,
                    0.0f
                );
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(ball, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(1f);
            }


            ;
        }

        yield return new WaitForSeconds(2.0f);
        gameOverText.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        restartButton.SetActive(true);
    }

}