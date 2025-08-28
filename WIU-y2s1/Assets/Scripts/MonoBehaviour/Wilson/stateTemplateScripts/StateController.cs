using UnityEngine;

public class StateController : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string GUID = "";
    public State currentState;
    public State remainState;

    public float distanceFromPlayer = 30f;
    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (currentState != null && player != null)
        {
            float dist = Vector3.Distance(transform.position, player.position);

            if (dist <= distanceFromPlayer)
            {
                // Only update FSM if within range
                currentState.UpdateState(this);
            }
        }
    }

    public void TransitionToState(State nextstate)
    {
        if (nextstate != remainState)
        {
            currentState = nextstate;
        }
    }
    public void SaveData(GameData data)
    {
        if (data.mapGameObjects.ContainsKey(GUID))
        {
            data.mapGameObjects.Remove(GUID);
        }
        data.mapGameObjects.Add(GUID, gameObject.activeInHierarchy);
    }
    public void LoadData(GameData data)
    {
        bool isActive = true;
        if (data.mapGameObjects.TryGetValue(GUID, out isActive))
            gameObject.SetActive(isActive);
        else
            gameObject.SetActive(true);
    }
}
