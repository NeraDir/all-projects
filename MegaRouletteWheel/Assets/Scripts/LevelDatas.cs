using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Level Data", menuName = "Create Level Data")]
public class LevelDatas : ScriptableObject
{
    public Level[] levels;

    [ContextMenu("GenerateLevels")]
    private void GenerateLevels()
    {
        System.Random random = new System.Random();
        int[] shootCounts = { 10, 15, 20 };
        int maxLines = 6;

        levels = new Level[18];

        for (int i = 0; i < 18; i++)
        {
            int lineCount = UnityEngine.Random.Range(3, maxLines + 1); 
            int shootCount = shootCounts[i % shootCounts.Length];
            HashSet<JellyType> usedTypes = new HashSet<JellyType>();

            LineData[] lineDatas = new LineData[lineCount];

            for (int j = 0; j < lineCount; j++)
            {
                bool useTwoTypes = UnityEngine.Random.value > 0.5f; 
                JellyType firstType = (JellyType)random.Next(Enum.GetValues(typeof(JellyType)).Length);
                JellyType secondType = useTwoTypes ? (JellyType)random.Next(Enum.GetValues(typeof(JellyType)).Length) : firstType;

                lineDatas[j] = new LineData
                {
                    type = useTwoTypes ? new JellyType[] { firstType, secondType } : new JellyType[] { firstType },
                    lineCount = lineCount
                };

                usedTypes.Add(firstType);
                if (useTwoTypes) usedTypes.Add(secondType);
            }

            List<JellyType> attackBlockTypes = usedTypes.ToList();

            while (attackBlockTypes.Count < shootCount / 10)
            {
                attackBlockTypes.Add(usedTypes.ElementAt(random.Next(usedTypes.Count)));
            }

            levels[i] = new Level
            {
                lineDatas = lineDatas,
                shootCount = shootCount,
                attackBlockTypes = attackBlockTypes.ToArray()
            };
        }
    }

}

[Serializable]
public struct Level
{
    public LineData[] lineDatas;
    public int shootCount;
    public JellyType[] attackBlockTypes;
}

[Serializable]
public struct LineData
{
    public JellyType[] type;
    public int lineCount;
}