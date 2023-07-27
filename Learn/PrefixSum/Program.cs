using System;

class Program
{
    static void Main()
    {
        int[] arr = { 3, 1, 7, 2, 5, 4 };
        int n = arr.Length;
        int[] prefixSum = new int[n];

        // Calculate the prefix sum array
        prefixSum[0] = arr[0];
        for (int i = 1; i < n; i++)
        {
            prefixSum[i] = prefixSum[i - 1] + arr[i];
        }

        // Print the prefix sum array
        Console.WriteLine("Prefix sum array:");
        for (int i = 0; i < n; i++)
        {
            Console.Write(prefixSum[i] + " ");
        }
        Console.WriteLine();

        // Find the sum of elements in a specific range [startIndex, endIndex]
        int startIndex = 1;
        int endIndex = 4;
        int sumInRange = prefixSum[endIndex] - (startIndex > 0 ? prefixSum[startIndex - 1] : 0);
        Console.WriteLine($"Sum in range [{startIndex}, {endIndex}] is: {sumInRange}");
    }
}
