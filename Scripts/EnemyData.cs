using UnityEngine;

[CreateAssetMenu(menuName = "Actors/Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Stats")]
    [SerializeField] private float health, speed, worth;

    [Header("Visuals")]
    [SerializeField] private GameObject model;

    public float Health
    {
        get {return health; }
        set {health = value; }
    }

    public float Speed
    {
        get {return speed; }
        set {speed = value; }
    }

    public float Worth
    {
        get {return worth; }
        set {worth = value; }
    }

    public GameObject Model
    {
        get {return model; }
        set {model = value; }
    }
}
