using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class FileManager
{
    private string _fileDirName = "";
    private string _fileName = "";

    public FileManager(string fileDirName, string fileName)
    {
        _fileDirName = fileDirName;
        _fileName = fileName;
    }
    public GameData Load()
    {
        string fullPathName = Path.Combine(_fileDirName, _fileName);
        GameData LoadedData = null;
        if (File.Exists(fullPathName))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPathName, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                //reads from the Json file and stores it back into GameData obj, which we then assign to loaded data and return
                LoadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e){ }
        }
        return LoadedData;
    }
    public void Save(GameData data) { 
        //Generate the file path with directory name and file name this way
        //to account for different directory separates on different OS
        string fullPathName = Path.Combine(_fileDirName, _fileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPathName));

            //JsonUtility.ToJson takes in a data to be converted into Json format and a bool to either prioritise size or readability of data
            //Passing in the Game data to convert to Json format and passing in true so that data is readable
            string dataToStore = JsonUtility.ToJson(data, true); 

            //When opening files, if an exception is thrown, the file may get closed and it may result in a memory leak
            //Using block ensures that the memory is disposed off even if there is an exception thrown
            using (FileStream stream = new FileStream(fullPathName, FileMode.Create))
            {
                //What i understand is that streamwriter gets refernce to the file at the param location and can edit its contents
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            //This will print an error message on the console window if the file is unable to load
            Debug.LogError("Unable to load file at path: " + fullPathName + "\n" + e);
        }
    }
}
