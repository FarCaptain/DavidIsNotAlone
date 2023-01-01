using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewColorCode", menuName = "Sphinx/ColorCode")]
public class ColorCode : ScriptableObject
{
    public List<Color> RedColorTheme;
    public List<Color> BlueColorTheme;
}
