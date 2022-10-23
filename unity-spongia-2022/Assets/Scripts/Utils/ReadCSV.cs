using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Utils
{
    public class ReadCSV
    {
        public string[][] ReadCSVFile(string resourcesPath)
        {
            var textFile = Resources.Load<TextAsset>(resourcesPath);
            string textString = textFile.ToString();

            string[] lines = textString.Split(new[] { '\r', '\n' });
            string[][] arr = new string[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(",");

                for (int j = 0; j < values.Length; j++)
                {
                    arr[i][j] = values[j];
                }
            }

            return arr;
        }
    }
}
