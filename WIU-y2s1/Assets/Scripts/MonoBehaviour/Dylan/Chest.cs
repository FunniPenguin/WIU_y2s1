using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour
{
    public UnityEvent _chestOpened;
    private string _id;
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
        if (data.savePoints.ContainsKey(id))
        { data.savePoints.Remove(id); }
        data.savePoints.Add(id, _currSavePoint);
    }
    public void LoadData(GameData data)
    {
        if (data.savePoints.TryGetValue(id, out bool isSavePoint))
            _currSavePoint = isSavePoint;
        if (_currSavePoint)
        {
            _player.transform.position = transform.position;
            //So that when saving the game only will have one save point marked as true
            _currSavePoint = false;
        }
    }
    IEnumerator UseChest()
    {
        _animator.Play("ChestOpen");
        yield return new WaitForSeconds(1);
        foreach (GameObject prefab in _prefabList)
        {
            Vector2 ItemPosition = new Vector2(transform.position.x, transform.position.y + 1);
            GameObject Item = Instantiate(prefab, ItemPosition, this.transform.rotation);
            Debug.Log(Item);
            Item.SetActive(true);
            Debug.Log(Item.transform.position);
        }
        this.gameObject.SetActive(false);
        yield return null;
    }
}
