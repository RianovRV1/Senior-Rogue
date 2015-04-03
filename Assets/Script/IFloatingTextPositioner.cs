using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IFloatingTextPositioner //an interface to use floating text
{
    bool GetPosition(ref Vector2 postion, GUIContent content, Vector2 size);
}

