using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
/// <summary>
/// Basic game Heads Up Display that displays the current level and the characters HP at the top left
/// </summary>
   public class GameHUD : MonoBehaviour
    {
        public GUISkin Skin;
        public string text = " ";
        Player player;
       public void Start()
       {
           player = FindObjectOfType<Player>();
       }
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
                if (player != null)
                {
                    GUILayout.BeginVertical(Skin.GetStyle("GameHUD"));
                    {
                        GUILayout.Label(string.Format("Health: {0}/{1}", player.Health, player.MaxHealth), Skin.GetStyle("TimeText"));
                    }
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndArea();
        }
    }

