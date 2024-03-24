using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ScratchCardDatabaseObject", order = 1)]
public class ScratchCardDatabase : ScriptableObject
{
    public List<Sprite> cardImages = new List<Sprite>();

   

}
