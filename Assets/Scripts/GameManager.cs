using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public double TimeForADay;
    public double TimeBetweenDeliveries;
    public double TimeBetweenWaves;
    public EnemySpawner spawner;

    private double lastDay;
    private double lastDelivery;
    private double lastWave;

    private int nbOfDay;
    private int amountDelivery;
    private int amountOfEnemy;

    private int actualStockInTruck;
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
            spawner.startSpawner(amountOfEnemy);
        }
    }

    private void manageDelivery()
    {
        if (Time.time - lastDelivery > TimeBetweenDeliveries)
        {
            amountOfEnemy += (amountDelivery - actualStockInTruck) / 2;
            actualStockInTruck = 0;
            Debug.Log("Delivery is cuming ! Hide in bush noob\n" + amountDelivery + " stuff needed");
            lastDelivery = Time.time;
            amountDelivery += 10;
        }
    }

    private void manageDay()
    {
        if (Time.time - lastDay > TimeForADay)
        {
            Debug.Log("Its a new Day BRA");
            nbOfDay++;
            lastDay = Time.time;
        }
    }

    public int getTruckStockMissing()
    {
        return amountDelivery - actualStockInTruck;
    }
    public void addInTruckStock(int quantity)
    {
        actualStockInTruck += quantity;
    }

}
