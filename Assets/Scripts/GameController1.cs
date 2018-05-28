using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO; // how we read and write our data; Input or output
using System.Runtime.Serialization.Formatters.Binary; // 

public class GameController1 : MonoBehaviour
{
    public static GameController1 instance;

    private GameData data;

    public int currentLevel;

    public int currentScore;

    public bool isGameStartedFirstTime;
    public bool isMusicOn;
    public int selectedPlayer;
    public int selectedWeapon;
    public int coins;
    public int highScore;

    public bool[] players;
    public bool[] levels;
    public bool[] weapons;
    public bool[] achievements;
    public bool[] collectedItems;


    void Awake() {
        MakeSingleton();
        InitializeVariables();

    }

    // Use this for initialization
    void Start()
    {
       

    }

    // SINGLETON of this class
    void MakeSingleton() {

        if(instance != null) {
           Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void InitializeVariables()
    {
        Load();

        if (data != null)
        {
            isGameStartedFirstTime = data.getIsGameStartedFirst();
        }
        else
        {
            isGameStartedFirstTime = true;
        }
        if (isGameStartedFirstTime)
        {
            // game started for the first time

            highScore = 0;
            coins = 0;

            selectedPlayer = 0;
            selectedWeapon = 0;

            isGameStartedFirstTime = false;
            isMusicOn = false;

            players = new bool[6];
            levels = new bool[40];
            weapons = new bool[4];
            achievements = new bool[8];
            collectedItems = new bool[40];

            players[0] = true;
            for (int i = 1; i < players.Length; i++)
            {
                players[i] = false;
            }

            levels[0] = true;
            for (int i = 1; i < levels.Length; i++)
            {
                levels[i] = false;
            }

            weapons[0] = true;
            for (int i = 1; i < weapons.Length; i++)
            {
                weapons[i] = false;
            }
            for (int i = 1; i < achievements.Length; i++)
            {
                achievements[i] = false;
            }

            for (int i = 1; i < collectedItems.Length; i++)
            {
                collectedItems[i] = false;
            }

            data = new GameData();

            data.setHighScore(highScore);
            //data.setCoins(coins);
            data.setSelectedPlayer(selectedPlayer);
            data.setIsGameStartedFirstTime(isGameStartedFirstTime);
            data.setIsMusicOn(isMusicOn);

            Save();

            Load();



        }
    }
    public void Save()
    {

        FileStream file = null;
        try
        {

            BinaryFormatter bf = new BinaryFormatter();

            // creating file
            file = File.Create(Application.persistentDataPath + "/GameData.dat");

            if (data != null)
            {

                data.setHighScore(highScore);
                data.setCoins(coins);
                data.setIsGameStartedFirstTime(isGameStartedFirstTime);
                data.setPlayers(players);
                data.setLevels(levels);
                data.setWeapon(weapons);
                data.setSelectedPlayer(selectedPlayer);
                data.setIsMusicOn(isMusicOn);
                data.setAchievements(achievements);
                data.setCollectedItems(collectedItems);
                data.setSelectedWeapon(selectedWeapon);

                bf.Serialize(file, data);
            }

        }
        catch (Exception e)
        {

        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    public void Load()
    {
        FileStream file = null;

        try
        {

            BinaryFormatter bf = new BinaryFormatter();

            file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);

            data = (GameData)bf.Deserialize(file);

        }
        catch (Exception e)
        {

        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }


} // game controller

// Serializable, will be stored in our binary file.
[Serializable]
class GameData
{

    private bool isGameStartedFirstTime; // this variable will be used for initialization.
    private bool isMusicOn;
    private int selectedPlayer; // to keep track which player is selected.
    private int selectedWeapon;
    private int coins; // keep track how much the player have earned.
    private int highScore;

    private bool[] players; // keep track of players that are locked and unlocked.
    private bool[] levels; // keep track of levels that are locked and unlocked.
    private bool[] weapons;
    private bool[] achievements;
    private bool[] collectedItems; // items that will be given to user when the game is finished.

    public void setIsGameStartedFirstTime(bool isGameStartedFirstTime)
    {
        this.isGameStartedFirstTime = isGameStartedFirstTime;
    }

    public bool getIsGameStartedFirst()
    {
        return this.isGameStartedFirstTime;
    }

    public void setIsMusicOn(bool isMusicOn)
    {
        this.isMusicOn = isMusicOn;
    }
    public bool getIsMusicOn()
    {
        return this.isMusicOn;
    }

    public void setSelectedPlayer(int slctdPlayer)
    {
        this.selectedPlayer = slctdPlayer;
    }
    public int getSelectedPlayer()
    {
        return this.selectedPlayer;
    }

    public void setSelectedWeapon(int selectedWeapon)
    {
        this.selectedWeapon = selectedWeapon;
    }

    public int getSelectedWeapon()
    {
        return selectedWeapon;
    }

    public void setCoins(int coins)
    {
        this.coins = coins;
    }
    public int getCoins()
    {
        return this.coins;
    }

    public void setHighScore(int highScore)
    {
        this.highScore = highScore;
    }
    public int getHighscore()
    {
        return this.highScore;
    }

    public void setPlayers(bool[] chosenPlayers)
    {
        this.players = chosenPlayers;
    }

    public bool[] getPlayers()
    {
        return this.players;
    }
    public void setLevels(bool[] levels)
    {
        this.levels = levels;
    }

    public bool[] getLevels()
    {
        return this.levels;
    }

    public void setWeapon(bool[] weapon)
    {
        this.weapons = weapon;
    }
    public bool[] getWeapon()
    {
        return this.weapons;
    }

    public void setAchievements(bool[] achievement)
    {
        this.achievements = achievement;
    }
    public bool[] getAchievements()
    {
        return this.achievements;
    }

    public void setCollectedItems(bool[] collectedItem)
    {
        this.collectedItems = collectedItem;
    }
    public bool[] getCollected()
    {
        return this.collectedItems;
    }
}