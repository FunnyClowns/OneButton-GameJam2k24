using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Form Style Data", menuName = "Form/Form Style", order = 1)]
public class FormStyleData : ScriptableObject
{
    [Header("Form Sprites")]
    public Sprite paperFormSprite;
    public Sprite validStampSprite;
    public List<Sprite> invalidStampSprites;
    public List<Color> validStampColors;
    public List<Color> invalidStampColors;
    public List<Sprite> validBuildingSprites;
    public List<Sprite> invalidBuildingSprites;
}
