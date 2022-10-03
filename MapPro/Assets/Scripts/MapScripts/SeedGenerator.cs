
using System;
using System.Collections.Generic;
using UnityEngine;
public static class SeedGenerator
{
    private static float _indexMultiplier = 100;
    private static float _offset = 10000;
    
    public static Vector2 GetSeed( int index)
    {
        if (Dynamic.MapData.seed != null)
        {
            // Generates unique coordinates for any given string by converting and manipulating each character
            int baseValue = 0;
            List<char> chars = new List<char>();
            chars.AddRange(Dynamic.MapData.seed);

            for (int i = 0; i < chars.Count; i++)
            {
                if (char.IsNumber(chars[i]))
                {
                    baseValue += (int)char.GetNumericValue(chars[i]);
                }
                else
                {
                    baseValue += char.ToUpper(chars[i]) - 64;
                }
            }

            float seedX = (baseValue * 234) + ((baseValue % 5) * 20) + ((index * 10) * _indexMultiplier) + _offset;
            float seedY = (baseValue * 53) + ((baseValue % 8) * 25) + ((index + 20) * _indexMultiplier) + _offset;

            return new Vector2(seedX, seedY); 
        }

        return Constant.Map.defaultSeed;

    }
}
