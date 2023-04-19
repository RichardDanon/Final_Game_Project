using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelector : MonoBehaviour
{
    public void onClick(Button button)
    {
        switch(button.name.ToString())
        {
            case "Button1":
                SceneManager.LoadScene("Level_01"); 
                break;
            case "Button2":
                SceneManager.LoadScene("Level_02");
                break;
            case "Button3":
                SceneManager.LoadScene("Level_03");
                break;
            case "Button4":
                SceneManager.LoadScene("Level_04");
                break;
            case "Button5":
                SceneManager.LoadScene("Level_05");
                break;
            case "Button6":
                SceneManager.LoadScene("Level_06");
                break;
        }
    }
}
