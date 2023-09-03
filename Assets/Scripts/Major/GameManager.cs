using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player; // to set the Player to the position
    public Camera _camera;
    [Space]
    public PlayerHealth health;
    [Space]
    public Transform LastSavedPos;
    public string lastSavedId; // Bonfires check this string if its same with their Id, if it is it means they're the last bonfire used
    [Space]
    public GameObject TabbedCanvas;
    public GameObject PausedCanvas;
    [Space]
    public TMP_Text ScoreText;
    public TMP_Text TimeText;
    [Space]
    public bool IsPaused = false;
    public bool OnZone = false;

    void Update(){
        ScoreText.text = $"{TotalScore:000000} :Score";

        if(Input.GetKey(KeyCode.Tab) && IsPaused == false){
            TabbedCanvas.SetActive(true);
        } else if(Input.GetKeyUp(KeyCode.Tab) && IsPaused == false){
            TabbedCanvas.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            Paused();
        }

        UpdateCurrentSpentTime();
    }

    void Start(){
        TabbedCanvas.SetActive(false);
        PausedCanvas.SetActive(false);
        LoadIn();

        _camera.transform.position = Player.transform.position;
    }

    [Space]
    public float seconds; // Used to add up Seconds to minutes and minutes to hours
    public float minutes;
    public float hours;
    public void UpdateCurrentSpentTime(){
        seconds += Time.deltaTime;

        if(seconds >= 60){
            seconds = 0; 
            minutes++;
        } else if(minutes >= 60){
            minutes = 0;
            hours++;
        }

        TimeText.text = $"Time: {hours:00}:{minutes:00}:{seconds:00}";
    }

    public static int TotalScore;
    public static void ReceiveScore(int receivedAmount){
        TotalScore += receivedAmount;
    }

    public void Heal(int HealthAmount){
        if(health.Health < health.MaxHealth){
            health.Health += HealthAmount;
        }
    }

    public void Restart(){
        Player.transform.position = LastSavedPos.position;
    }

    public void Resume(){
        PausedCanvas.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    void Paused(){
        PausedCanvas.SetActive(true);
        TabbedCanvas.SetActive(false);
        Time.timeScale = 0.2f;
        IsPaused = true;
    }

    public void LoadIn(){
        TotalScore = SaveSystem.GetInt("Score");
        seconds = SaveSystem.GetFloat("seconds");
        minutes = SaveSystem.GetFloat("minutes");
        hours = SaveSystem.GetFloat("hours");
        Player.transform.position = SaveSystem.GetVector3("LastSavedPos");
        lastSavedId = SaveSystem.GetString("LastSavedId");
        _camera.orthographicSize = SaveSystem.GetFloat("CameraZoom");
    }

    public void MainMenu(){
        Time.timeScale = 1;
        // Saving
        SaveSystem.SetInt("Score", TotalScore);
        SaveSystem.SetFloat("seconds", seconds);
        SaveSystem.SetFloat("minutes", minutes);
        SaveSystem.SetFloat("hours", hours);
        SaveSystem.SetVector3("LastSavedPos", LastSavedPos.position);
        SaveSystem.SetString("LastSavedId", lastSavedId);
        SaveSystem.SetFloat("CameraZoom", _camera.orthographicSize);
        SceneManager.LoadScene("Menu");
    }

    public void Save_Game(){
        SaveSystem.SetInt("Score", TotalScore);
        SaveSystem.SetFloat("seconds", seconds);
        SaveSystem.SetFloat("minutes", minutes);
        SaveSystem.SetFloat("hours", hours);
        SaveSystem.SetVector3("LastSavedPos", LastSavedPos.position);
        SaveSystem.SetString("LastSavedId", lastSavedId);
        SaveSystem.SetFloat("CameraZoom", _camera.orthographicSize);
    }

    private void OnApplicationQuit() {
        Save_Game();
    }
    
}
