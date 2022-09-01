using System.Text.Json;

namespace GP.Utilix;

public class FilesHelper
{
    private const string RootPath = @"F:\GraduationProject\GP.ECommerce1";
    
    public static void WriteToJsonFile<T>(string fileName, T objectToWrite, bool append = false) where T : new()
    {
        TextWriter? writer = null;
        try
        {
            var opt = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(objectToWrite, opt);
            var path = RootPath + $@"\{fileName}.json";
            writer = new StreamWriter(path, append);
            Task.Run(() => writer.Write(json)).Wait();
        }
        finally
        {
            writer?.Close();
        }
    }

    public static T ReadFromJsonFile<T>(string fileName) where T : new()
    {
        TextReader? reader = null;
        try
        {
            var path = RootPath + $@"\{fileName}.json";
            reader = new StreamReader(path);
            var result = Task.Run(() => reader.ReadToEnd()).Result;
            var json = JsonSerializer.Deserialize<T>(result);
            if (json == null)
                throw new Exception("Couldn't Parse the json Data by the given type");
            return json;
        }
        finally
        {
            reader?.Close();
        }
    } 
}