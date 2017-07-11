using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Loading screen layout and transition function to go to the starting level
/// has the same Go to next level function as the RoomManager
/// </summary>
   public class LoadingScreen : MonoBehaviour
    {
       public void Start()
       {
           GotoNextLevel("Level 1");
       }
       public void GotoNextLevel(string levelName)
        {
            StartCoroutine(GotoNextLevelCo(levelName));
        }

        private IEnumerator GotoNextLevelCo(string levelName)
        {
            yield return new WaitForSeconds(3f);
            

                if (string.IsNullOrEmpty(levelName))
                {

                    SceneManager.LoadSceneAsync("Level 1");
                    //Application.LoadLevel("Level 1");

                }

                else
                {
                    SceneManager.LoadSceneAsync(levelName);
                    //Application.LoadLevel(levelName);
                }
            
        }
   }


