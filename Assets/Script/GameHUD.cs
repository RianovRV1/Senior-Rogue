using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

   public class GameHUD : MonoBehaviour
    {
        public GUISkin Skin;
        public string text = " ";
        public void OnGUI()
        {
            GUI.skin = Skin;

            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            {
                GUILayout.BeginVertical(Skin.GetStyle("GameHUD"));
                {
                    if (text == "level")
                    {
                        GUILayout.Label(string.Format("{0} {1}", text, RoomManager.level), Skin.GetStyle("TimeText"));
                    }
                    else
                    {
                        GUILayout.Label(string.Format("{0}", text), Skin.GetStyle("Loading"));
                    }
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndArea();
        }
    }

