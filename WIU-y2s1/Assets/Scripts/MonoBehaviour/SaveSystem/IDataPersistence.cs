using UnityEngine;

public interface IDataPersistence
{
    void LoadData(GameData data);
    void SaveData(ref GameData data);
}

//This class is done by Yap Jun Hong Dylan