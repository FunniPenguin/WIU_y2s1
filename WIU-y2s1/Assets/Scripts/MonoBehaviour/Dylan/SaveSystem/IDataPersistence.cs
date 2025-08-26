using UnityEngine;

public interface IDataPersistence
{
    void LoadData(GameData data);
    void SaveData(GameData data);
}

//This class is done by Yap Jun Hong Dylan