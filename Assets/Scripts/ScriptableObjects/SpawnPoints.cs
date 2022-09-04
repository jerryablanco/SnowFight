using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpawnPoints : ScriptableObject
{
    public List<Vector2> locations = new List<Vector2>();
}
