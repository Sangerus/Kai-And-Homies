using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currentLevel;
    public float health;
    public float startingHealth;
    public SerializableDictionary<string, bool> starCollected;
    public int pocketWatch; 
    public Vector3 checkpointPosition;
    public SerializableDictionary<string, bool> activatedCheckpoints;
    public SerializableDictionary<string, bool> deadEnemies;
    public SerializableDictionary<string, bool> leverStates;

    public GameData()
    {
        this.currentLevel = 1;
        this.health = 4;
        this.startingHealth = 4;
        starCollected = new SerializableDictionary<string, bool>();
        this.pocketWatch = 0;
        this.checkpointPosition = Vector3.zero;
        this.activatedCheckpoints = new SerializableDictionary<string, bool>();
        this.deadEnemies = new SerializableDictionary<string, bool>();
        this.leverStates = new SerializableDictionary<string, bool>();
    }
}

