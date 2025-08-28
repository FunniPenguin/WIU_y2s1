using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour, IDataPersistence
{
    public UnityEvent _chestOpened;
    private bool _currSavePoint;
    GameObject _player;
    [SerializeField] float _distanceToInteract = 1.0f;
    [SerializeField] string id = "";
    [SerializeField] GameObject[] _prefabList;
    private Animator _animator;
    private bool _used = false;
    private void Awake()
    {
        _player = FindFirstObjectByType<_PlayerController>().gameObject;
        _animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if ((Input.GetKeyUp(KeyCode.E)) && !(_used))
        {
            var player = FindAnyObjectByType<_PlayerController>();
            if (Vector3.Distance(player.transform.position, transform.position) <= _distanceToInteract)
            {
                _used = true;
                StartCoroutine(UseChest());
                _chestOpened.Invoke();
            }
        }
    }
    public void SaveData(GameData data)
    {
        if (data.mapGameObjects.ContainsKey(id))
        {
            data.mapGameObjects.Remove(id);
        }
        data.mapGameObjects.Add(id, gameObject.activeInHierarchy);
    }
    public void LoadData(GameData data)
    {
        bool isActive = true;
        if (data.mapGameObjects.TryGetValue(id, out isActive))
            gameObject.SetActive(isActive);
        else
            gameObject.SetActive(true);
    }
    IEnumerator UseChest()
    {
        _animator.Play("ChestOpen");
        yield return new WaitForSeconds(1);
        foreach (GameObject prefab in _prefabList)
        {
            Vector2 ItemPosition = new Vector2(transform.position.x, transform.position.y + 1);
            GameObject Item = Instantiate(prefab, ItemPosition, this.transform.rotation);
            Item.SetActive(true);
        }
        this.gameObject.SetActive(false);
        yield return null;
    }
}
