using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MazeBlock : MonoBehaviour
{
    public void Init(bool[] walls)
    {
        if (walls != null)
        {
            var w = GetComponentsInChildren<WallScript>();
            var names = new[] { "N", "E", "S", "W" };
            for (int i = 0; i < 4; i++)
            {
                var wall = w.FirstOrDefault(x=> x.gameObject.name == names[i]);
                // wall.enabled = walls[i];
                if (!walls[i]){
                    Destroy(wall.gameObject);
                }
            }
        }
    }
}
