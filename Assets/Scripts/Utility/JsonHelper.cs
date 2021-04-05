using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class JsonHelper
{
    public static List<T> FromJson<T>(string json)
    {
        List<T> DataArray = new List<T>();
        
        var dataArrayJson = JArray.Parse(json);
        for(int index = 0; index < dataArrayJson.Count; index++)
        {
            T addData = JsonUtility.FromJson<T>(dataArrayJson[index].ToString());
            DataArray.Add(addData);
        }
        
        return DataArray;
    }
}
