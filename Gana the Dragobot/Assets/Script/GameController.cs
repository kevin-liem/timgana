using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject enemy;
    private int[,] spawnedNumerics;         //2 dimensional array to save the references of math calculation
    private int enemiesSetLimit = 5;       //Number of waves allowed inside the screen
    private int enemiesSetIndex = 0;       //Increment index to track the current math

    public Transform[] spawnPoint;

    public GameObject numericNumber;
    public GameObject numericOperator;
    public GameObject numericResult;
    public int[] numericRange = new int[2];
    public string[] operatorVariation;

    void Awake()
    {
        spawnedNumerics = new int[enemiesSetLimit, spawnPoint.Length];
    }

    void Start()
    {
        StartCoroutine(SpawnEnemiesSet());
    }

    IEnumerator SpawnEnemiesSet()
    {
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            //First enemy from left consist of numbers
            if (i % 2 == 0) { SpawnNumberEnemy(i); }

            //Second enemy from left consist of operator
            else { SpawnOperatorEnemy(i); }
        }

        //When finished spawning one set of enemies
        if (enemiesSetIndex == enemiesSetLimit - 1) { enemiesSetIndex = 0; }
        else { enemiesSetIndex++; }

        yield return new WaitForSeconds(2f);
    }

    //Spawning 1 enemy with numericNumber on its back
    private void SpawnNumberEnemy(int positionIndex)
    {
        //Instantiate the enemy without the number and save the reference to the gameobject
        GameObject tmpEnemyGO = Instantiate(enemy, spawnPoint[positionIndex].position, Quaternion.identity) as GameObject;

        //Instantiate the numeric text
        GameObject tmpSpawnedTextHolder = Instantiate(numericNumber,
            tmpEnemyGO.transform.FindChild("NumericSpawnPoint").position,
            numericNumber.transform.rotation) as GameObject;
        tmpSpawnedTextHolder.transform.parent = tmpEnemyGO.transform.FindChild("NumericSpawnPoint").transform;

        //Set the scaling to default
        Vector3 numericTextScale = new Vector3(numericNumber.transform.localScale.x, numericNumber.transform.localScale.y,
            numericNumber.transform.localScale.z);
        tmpSpawnedTextHolder.transform.localScale = numericTextScale;

        //Get the random number for calculation
        int number = Random.Range(numericRange[0], numericRange[1]);

        //Change the text as number defined
        tmpSpawnedTextHolder.GetComponent<TextMesh>().text = number.ToString();

        //Save the textmesh reference into 2 dimensional array
        string tmpNumber = tmpSpawnedTextHolder.GetComponent<TextMesh>().text;
        spawnedNumerics[enemiesSetIndex, positionIndex] = int.Parse(tmpNumber);
    }

    //Spawning 1 enemy with numericOperator on its back
    private void SpawnOperatorEnemy(int positionIndex)
    {
        //Instantiate the enemy without the operator and save the reference to the gameobject
        GameObject tmpEnemyGO = Instantiate(enemy, spawnPoint[positionIndex].position, Quaternion.identity) as GameObject;

        //Instantiate the numeric text
        GameObject tmpSpawnedTextHolder = Instantiate(numericOperator,
            tmpEnemyGO.transform.FindChild("NumericSpawnPoint").position,
            numericNumber.transform.rotation) as GameObject;
        tmpSpawnedTextHolder.transform.parent = tmpEnemyGO.transform.FindChild("NumericSpawnPoint").transform;

        //Set the scaling to default
        Vector3 numericTextScale = new Vector3(numericNumber.transform.localScale.x, numericNumber.transform.localScale.y,
            numericNumber.transform.localScale.z);
        tmpSpawnedTextHolder.transform.localScale = numericTextScale;

        //Get the random number for operator index
        int opr = Random.Range(0, operatorVariation.Length);

        //Define operator
        string tmpOperatorTxt = "";
        if (opr == 0) { tmpOperatorTxt = "+"; }
        else if (opr == 1) { tmpOperatorTxt = "-"; }
        else { Debug.Log("Operator index out of bounds : " + opr); }

        //Change the text as operator defined
        tmpSpawnedTextHolder.GetComponent<TextMesh>().text = tmpOperatorTxt;

        //Save the textmesh reference into 2 dimensional array
        spawnedNumerics[enemiesSetIndex, positionIndex] = opr;
    }
}

/*
-----------------------------NOTE------------------------------
Enemies ID list :
0. Number
1. Operaotr

Operator ID list :
0. +
1. -
*/