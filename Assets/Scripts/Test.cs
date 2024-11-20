using UnityEngine;

public static class Test
{
    public static void Assert(bool condition, string testName)
    {
        if (condition)
        {
            Debug.Log(testName + " passed.");
        }
        else
        {
            Debug.LogError(testName + " failed.");
        }
    }
}