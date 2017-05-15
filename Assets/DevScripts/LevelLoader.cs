using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {


    public void LoadScene(string name)
    {
        Time.timeScale = 1;
        Application.LoadLevel(name);
    }
}
