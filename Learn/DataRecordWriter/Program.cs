using System;
using System.Collections.Generic;

class DataRecordReader
{
    private Dictionary<long, long> m_keep_map;

    public DataRecordReader(Dictionary<long, long> keep_map)
    {
        m_keep_map = keep_map;
    }

    public bool KeepKey(long key, out long code)
    {
        if (m_keep_map.TryGetValue(key, out code))
            return true;

        return false;
    }
}

class Program
{
    static void Main()
    {
        Dictionary<long, long> configMap = new Dictionary<long, long>
        {
            {1001, 42},
            {1002, 56},
            {1003, 999},
        };

        DataRecordReader reader = new DataRecordReader(configMap);

        // Example usage of the KeepKey function
        long keyToFind = 1002;
        long value;

        if (reader.KeepKey(keyToFind, out value))
        {
            Console.WriteLine($"Key {keyToFind} found! Value: {value}");
        }
        else
        {
            Console.WriteLine($"Key {keyToFind} not found in the configuration.");
        }

        // Trying to find a key that doesn't exist
        long nonExistentKey = 9999;
        if (reader.KeepKey(nonExistentKey, out value))
        {
            Console.WriteLine($"Key {nonExistentKey} found! Value: {value}");
        }
        else
        {
            Console.WriteLine($"Key {nonExistentKey} not found in the configuration.");
        }
    }
}
