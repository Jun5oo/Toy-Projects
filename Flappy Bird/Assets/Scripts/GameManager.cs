using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameStarted;
    public bool isGameOver; 
    public bool spawnEnable; 

    public GameObject pipePrefab; // Pipe prefab.  
    List<GameObject> pipePrefabs; // List of pipe object. 
    
    public float pipeNum = 5f; // Number of pipe when the game is started. 
    public float pipeSpeed; // Speed of pipe.
    public float pipeRate;
    public float minRate; 

    public GameObject scoreUI; // Score that shows up during the game 
    public GameObject scoreBoard; // ScoreBoard 
    
    public int score; // Result score 
    public int bestScore; // Best Score 

    public bool endFlag; 

    public float startTime;
    public float currentTime;

    public bool isAccelerated; 

    string path; // Path of the text file that save the best score 

    void Awake()
    {
        isGameStarted = false;
        isGameOver = false; 
        spawnEnable = false;

        pipePrefabs = new List<GameObject>();
        pipeSpeed = 3f;
        pipeRate = 2f;
        minRate = 0.5f; 

        score = 0;

        UpdateScore(0);
        scoreUI.SetActive(false);

        endFlag = false;

        startTime = 0;
        currentTime = 0; 

        isAccelerated = false;

        path = Application.persistentDataPath + "/FlappyBirdScore.txt";
        LoadScore(); 

        for (int i=0; i < pipeNum; i++)
            CreatePipe(); 
    }

    void Update()
    {
        if (isGameOver)
        {
            if (!endFlag)
            {
                endFlag = true;

                if (score > bestScore)
                {
                    bestScore = score;
                    SaveScore(); 
                }

                scoreBoard.SetActive(true); 
                scoreBoard.GetComponent<ScoreBoard>().UpdateScoreBoard();
            }

            else
                return; 
        }

        if (!isGameStarted)
        {
            if (Input.anyKey)
            {
                isGameStarted = true;
                scoreUI.SetActive(true); 
                spawnEnable = true;
                startTime = Time.time; 
            }

            return; 
        }

        currentTime = Time.time - startTime; 

        if(currentTime > 10)
        {
            if(minRate < pipeRate)
            {
                if (isAccelerated)
                {
                    pipeSpeed += 2;
                    pipeRate -= 0.4f;
                }

                else
                {
                    pipeSpeed += 1;
                    pipeRate -= 0.8f;
                }
            }

            startTime = Time.time; 
        }

        if (spawnEnable && !isGameOver)
            StartCoroutine(SpawnPipe());
    }

    #region Pipe 
    GameObject CreatePipe()
    {
        GameObject pipe = Instantiate(pipePrefab, this.gameObject.transform);
        pipePrefabs.Add(pipe);
        pipe.SetActive(false);

        return pipe; 
    }

    IEnumerator SpawnPipe()
    {
        spawnEnable = false;

        GameObject pipe = null; 

        for(int i=0; i<pipePrefabs.Count; i++)
        {
            if (!pipePrefabs[i].activeSelf)
            {
                pipe = pipePrefabs[i];
                pipe.SetActive(true);
                break; 
            }
        }

        // If all of the pipe is in used, Create one; 
        if (!pipe)
        {
            GameObject newlyCreated = CreatePipe();
            newlyCreated.SetActive(true);
        }

        yield return new WaitForSeconds(pipeRate); 

        spawnEnable = true; 
    }
    #endregion

    #region GameSpeed 
    public void AccelerateSpeed()
    {
        isAccelerated = true;
        pipeSpeed *= 2;
    }

    public void DecelerateSpeed()
    {
        isAccelerated = false;
        pipeSpeed /= 2;
    }
    #endregion

    #region Score 
    public void UpdateScore(int _score)
    {
        score += _score; 
        scoreUI.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

    public void UpdateBestScore()
    {
        SaveScore(); 
    }

    public int GetResultScore()
    {
        return score; 
    }

    public int GetBestScore()
    {
        return bestScore; 
    }

    public void SaveScore()
    {
        File.WriteAllText(path, bestScore.ToString());
    }

    public void LoadScore()
    {
        if (File.Exists(path))
        {
            // Debug.Log("File exits"); 
            string _best = File.ReadAllText(path);
            bestScore = int.Parse(_best); 
        }

        else
        {
            // Debug.Log("File Doesn't exists"); 
            bestScore = 0;
            SaveScore(); 
        }
    }

    #endregion 
}
