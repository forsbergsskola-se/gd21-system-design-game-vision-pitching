using UnityEngine;

public class LogSpammer : MonoBehaviour
{
    [SerializeField] private int numberOfLogsToPrint = 1;

    void Update()
    {
        for (var i = 0; i < numberOfLogsToPrint; i++)
        {
            Debug.Log($"Log number: {i}");
        }
    }
}