using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public double TimeForADay;
    public double TimeBetweenDeliveries;
    public double TimeBetweenWaves;

    private double lastDay;
    private double lastDelivery;
    private double lastWave;

    private int nbOfDay;
    private int amountDelivery;
    private int amountOfEnemy;
    private void Start()
    {
        //Récupération de la TileMap;
        lastDay = Time.time;
        lastDelivery = Time.time;
        lastWave = Time.time;
        nbOfDay = 0;
        amountDelivery = 150;
        amountOfEnemy = 5;
    }

    private void Update()
    {
        manageDay();
        manageDelivery();
        manageWave();
    }

    private void manageWave()
    {
        if (Time.time - lastWave > TimeBetweenWaves)
        {
            Debug.Log("OHHH NO a wave is coming ! " + "\n" + amountOfEnemy + " Enemy");
            lastWave = Time.time; 
            amountOfEnemy += 3;
        }
    }

    private void manageDelivery()
    {
        if (Time.time - lastDelivery > TimeBetweenDeliveries)
        {
            Debug.Log("Delivery is cuming ! Hide in bush noob\n" + amountDelivery + " stuff needed");
            lastDelivery = Time.time;
            amountDelivery += 50;
        }
    }

    private void manageDay()
    {
        if (Time.time - lastDay > TimeForADay/2)
        {
            Debug.Log("Its night boys");
        }

        if (Time.time - lastDay > TimeForADay)
        {
            Debug.Log("Its a new Day BRA");
            nbOfDay++;
            lastDay = Time.time;
        }
    }

}
