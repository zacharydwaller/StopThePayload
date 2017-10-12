using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    protected PlayerController player;
    public Text moneyText;
    public Text roundText;
    public Text livesText;

    public MessageBox messageBox;

    public Button startRoundButton;

    public SpawnManager spawnManager;
    protected int round = 0;
    protected bool roundInProgress;

    protected int moneyPerRound = 100;
    protected int maxMoneyPerRound = 500;

    public GameObject waypointHead;
    public GameObject payload;

    public GameObject gameOverUI;
    public GameObject nextLevelUI;

    public void Start()
    {
        player =
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        roundText.text = "0";

        gameOverUI.SetActive(false);
        nextLevelUI.SetActive(false);
    }

    public void Update()
    {
        bool ripLastFrame = roundInProgress;

        moneyText.text = player.money.ToString();
        livesText.text = player.lives.ToString();

        CheckRoundInProgress();
        startRoundButton.interactable= !roundInProgress;

        if(ripLastFrame && roundInProgress == false)
        {
            EndRound();
        }
    }

    public void GameOver()
    {
        startRoundButton.interactable = false;
        gameOverUI.SetActive(true);
    }

    public void StartRound()
    {
        spawnManager.StartRound(round);
        round++;

        roundText.text = round.ToString();

        messageBox.SetMessage("Round " + round.ToString() + " starting.");
    }

    public void EndRound()
    {
        GameObject[] towers;
        int roundMoney = GetMoneyForRound();

        towers = GameObject.FindGameObjectsWithTag("Tower");

        foreach(GameObject tower in towers)
        {
            tower.SendMessage("FillHealth");
        }

        player.money += roundMoney;

        messageBox.SetMessage("Round " + round + " complete\nEarned " + roundMoney + " money");

        if(round == spawnManager.spawnList.Length)
        {
            nextLevelUI.SetActive(true);
            startRoundButton.interactable = false;
        }
    }

    public void CheckRoundInProgress()
    {
        GameObject creep;

        creep = GameObject.FindGameObjectWithTag("Creep");

        if(creep != null)
        {
            roundInProgress = true;
        }
        else
        {
            roundInProgress = false;
        }
    }

    public void CheckpointReached()
    {
        int roundMoney = GetMoneyForRound();

        GameObject[] creeps;

        creeps = GameObject.FindGameObjectsWithTag("Creep");
        foreach(GameObject creep in creeps)
        {
            creep.SendMessage("Die", false);
        }

        EndRound();

        player.LoseLife();
        

        messageBox.SetMessage("Round " + round + " lost, enemies got to the checkpoint\nEarned " + roundMoney + " money");
    }

    public int GetMoneyForRound()
    {
        return Mathf.Min(round * moneyPerRound, maxMoneyPerRound);
    }
}
