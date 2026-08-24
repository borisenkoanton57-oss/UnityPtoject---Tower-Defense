using UnityEngine;

[CreateAssetMenu(menuName = "Actors/Turret")]
public class TurretData : ScriptableObject
{
    [Header("Turret Stats")]
    [SerializeField] private int damage, price;
    [SerializeField] private float fireRate, speedOfBullet;
    [SerializeField] private string turretName;
    [SerializeField] private bool canRotate;

    [Header("Visuals")]
    [SerializeField] private Sprite sprite;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject turretPlacer;

    public int Damage
    {
        get { return damage; }
        set {damage = value; }
    }

    public int Price
    {
        get {return price; }
        set {price = value; }
    }

    public float FireRate
    {
        get {return fireRate; }
        set {fireRate = value; }
    }

    public float SpeedOfBullet
    {
        get {return speedOfBullet; }
        set {speedOfBullet = value; }
    }

    public string TurretName
    {
        get {return turretName; }
        set {turretName = value; }
    }

    public bool CanRotate
    {
        get {return canRotate; }
        set {canRotate = value; }
    }

    public Sprite Sprite
    {
        get {return sprite; }
        set {sprite = value; }
    }

    public GameObject Model
    {
        get {return model; }
        set {model = value; }
    }

    public GameObject TurretPlacer
    {
        get {return turretPlacer; }
        set {turretPlacer = value; }
    }
}
