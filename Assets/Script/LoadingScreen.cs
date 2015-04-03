using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using UnityEngine;

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
                    
                    Application.LoadLevel("Level 1");
                }

                else
                {
                    
                    Application.LoadLevel(levelName);
                }
            
        }
   }


