using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PLAYERSSETUPERCOMPONENT : MonoBehaviour
{
    private List<string> _names = new List<string>
    {
        "Alexander", "Olivia", "Liam", "Sophia", "Noah", "Isabella", "Mason", "Ava", "Ethan", "Mia",
        "James", "Charlotte", "Benjamin", "Amelia", "Jacob", "Harper", "Michael", "Evelyn", "Elijah", "Abigail",
        "Daniel", "Ella", "Matthew", "Scarlett", "Aiden", "Grace", "Henry", "Chloe", "Joseph", "Victoria",
        "Samuel", "Riley", "David", "Aria", "Sebastian", "Lily", "Gabriel", "Aurora", "Carter", "Hannah",
        "Owen", "Zoe", "John", "Penelope", "Jack", "Layla", "Luke", "Lillian", "Jayden", "Nora",
        "Dylan", "Lucy", "Grayson", "Stella", "Levi", "Ellie", "Isaac", "Paisley", "Julian", "Audrey",
        "Hudson", "Skylar", "Mateo", "Violet", "Anthony", "Savannah", "Jaxon", "Brooklyn", "Lincoln", "Bella",
        "Joshua", "Claire", "Christopher", "Hazel", "Andrew", "Samantha", "Theodore", "Kylie", "Caleb", "Maya",
        "Ryan", "Autumn", "Asher", "Caroline", "Nathan", "Aubrey", "Thomas", "Anna", "Leo", "Natalie",
        "Isaiah", "Eva", "Charles", "Madeline", "Josiah", "Alice", "Adam", "Ruby", "Eli", "Sarah"
    };

    [SerializeField] private List<Material> _materials;

    [SerializeField] private List<GameObject> _roads;

    public static List<Transform> playersList = new List<Transform>();

    private int _maxPlayersCount;

    public void Init(Transform player)
    {
        playersList.Clear();
        _maxPlayersCount = Random.Range(3, _roads.Count);
        playersList.Add(player);
        FindObjectOfType<LIVEPLAYERSCOMPONENT>().RegisterPlayer(player);
        StartCoroutine(SetupPlayers());
    }

    private IEnumerator SetupPlayers()
    {
        while (playersList.Count < _maxPlayersCount)
        {
            yield return new WaitForSeconds(15 / _maxPlayersCount);
            int index = Random.Range(0, _roads.Count);
            _roads[index].gameObject.SetActive(true);
            playersList.Add(_roads[index].GetComponentInChildren<MOVEMENTCOMPONENT>().transform);
            FindObjectOfType<LIVEPLAYERSCOMPONENT>().RegisterPlayer(playersList[playersList.Count - 1]);
            _roads.RemoveAt(index);
            index++;
        }
    }

    public Material GetRandomMaterial()
    {
        Material material = _materials[Random.Range(0, _materials.Count)];
        _materials.Remove(material);
        return material;
    }

    public string GetRandomName()
    {
        string name = _names[Random.Range(0, _names.Count)];
        _names.Remove(name);
        return name;
    }
}
