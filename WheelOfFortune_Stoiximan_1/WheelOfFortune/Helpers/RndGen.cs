using System;
/// <summary>
/// Custom Random generator.Acoriding to MS " On most Windows systems,
/// Random objects created within 15 milliseconds of one another are likely to have identical seed values. "
/// </summary>
public static class RndGen
{
    /// <summary>
    /// Same as unityEngine.random max value is NOT included in the results
    /// </summary>
    /// <param name="_minValue"></param>
    /// <returns></returns>
    /// <param name="_excludedMaxValue"></param>
    public static int RndInt(int _minValue, int _excludedMaxValue)
    {
        return new Random().Next(_minValue, _excludedMaxValue);
    }
    ///// <summary>
    ///// Same as unityEngine.random max value -> IS <- included in the results
    ///// </summary>
    ///// <param name="_minValue"></param>
    ///// <param name="_maxIncludedValue"></param>
    ///// <returns></returns>
    //public static float RndFloat(float _minValue, float _maxIncludedValue)
    //{
    //    return UnityEngine.Random.Range(_minValue, _maxIncludedValue);
    //}
    /// <summary>
    /// Strings in the form of " name_50" means name has 50 'slots' out of X where x = _a + ... + _z.
    /// Example: for 1% chance on nameA do: "nameA_1","nameB_50","nameC_49". 1 + 50 + 49 = 100, 1  of 100 is 1%.
    /// Returns an index for the provided String [].
    /// For higher prescision use bigger numbers.
    /// Returns -1 if array is empty, 0 if array has only one element
    /// </summary>
    /// <param name="_nameAndChance"></param>
    /// <returns></returns>
    public static int probabilityBasedRnd(string [] _nameAndChance)
    {
        // This method returns an index for it to be used with the supplied array (outside of this method)
        // for the user to get the indexed value from that array. For example getting a skill.
        if (_nameAndChance.Length > 1)
        {
            int [] chances = new int[_nameAndChance.Length];
            int totalChances = 0;
            int tempChances = 0;
            for (int i = 0; i < _nameAndChance.Length; i++)
            {
                string result = _nameAndChance[i].Substring(_nameAndChance[i].LastIndexOf('_') + 1);
                int.TryParse(result, out tempChances);//if the value is invalid the 0 is returned.
                chances[i] = tempChances;
                totalChances += tempChances;
            }
            int randomValue = RndInt(0, totalChances + 1);//offset the -1 in random range
            int tempValue = 0;
            for (int i = 0; i < chances.Length; i++ )
            {
                tempValue += chances[i];
                if (tempValue >= randomValue)
                    return i;//returns proper index
            }
            return chances.Length;//returns last index
        }
        else if (_nameAndChance != null)
            return 0;//returns 1st index
        return -1;//array was null return -1 to indicate error.
    }
    /// <summary>
    /// Float array elements are the chances per index. Returns an index based on the length
    /// of the array.
    /// </summary>
    /// <param name="_chance"></param>
    /// <returns></returns>
    public static int probabilityBasedRnd(float [] _chance)
    {
        // This method returns an index for it to be used with the supplied array (outside of this method)
        // for the user to get the indexed value from that array. For example getting a crit or not.
        if (_chance.Length > 1)
        {
            float [] chances = new float[_chance.Length];
            float totalChances = 0;
            for (int i = 0; i < _chance.Length; i++)
            {
                chances[i] = _chance[i];
                totalChances += _chance[i];
            }
            int randomValue = RndInt(0, (int)totalChances + 1);//offset the -1 in random range
            int tempValue = 0;
            for (int i = 0; i < chances.Length; i++ )
            {
                tempValue += (int)chances[i];
                if (tempValue >= randomValue)
                    return i;//returns proper index
            }
            return chances.Length;//returns last index
        }
        else if (_chance != null)//not null but == 1
            return 0;//returns 1st index
        return -1;//array was null return -1 to indicate error.
    }
}
