using UnityEngine;
using UnityEngine.Audio;

public class EnemyScript : MonoBehaviour
{
    [Header("Death Sound")]
    [SerializeField] private AudioClip[] deathSounds;
    [SerializeField] private AudioMixerGroup sfxGroup;

    [Header("Key Values")]
    public EnemyData stats;
    public Transform paths;
    public LevelScript level;

    [Header("Stats")]
    [SerializeField] private float damage;
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float worth;

    private int currentPosition = 0;
    private Vector2Int target;
    private Rigidbody rigidbodyEnemy;
   
    void Start()
    {
        rigidbodyEnemy = GetComponent<Rigidbody>();

        health = stats.Health;
        speed = stats.Speed;
        worth = stats.Worth;
        target = Vector2Int.RoundToInt(new Vector2(paths.GetChild
        (currentPosition).position.x, paths.GetChild(currentPosition).position.z));
    }


    void FixedUpdate()
    {
        if (Vector2Int.RoundToInt(new Vector2(transform.position.x, transform.position.z)) == target)
        {
           if(currentPosition < paths.childCount - 1)
            {
                currentPosition++;
                target = Vector2Int.RoundToInt(new Vector2(paths.GetChild
                (currentPosition).position.x, paths.GetChild(currentPosition).position.z));
            }
           else
            {
                level.DamageFinish();
                Destroy(gameObject);
            }
        }
        transform.LookAt(new Vector3(target.x, transform.position.y, target.y), Vector3.up);
        rigidbodyEnemy.AddForce(transform.forward * speed);
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (deathSounds != null && deathSounds.Length > 0)
        {
            AudioClip sound = deathSounds[Random.Range(0, deathSounds.Length)];

            if (sound != null)
            {
                // Создаем временный объект для звука
                GameObject tempAudio = new GameObject("TempAudio");
                tempAudio.transform.position = transform.position;

                AudioSource source = tempAudio.AddComponent<AudioSource>();
                source.clip = sound;
                source.outputAudioMixerGroup = sfxGroup; // SFX миксер
                source.spatialBlend = 0.0f; //звук 2D
                source.Play();

                Destroy(tempAudio, sound.length); // Удаляем 
            }
        }

        level.FillScoreText((int)worth);

        if (transform.parent.childCount <= 1)
        {
            if (level.level >= 49)
            {
                level.Victory();
                return;
            }

            level.ChangeLevel();
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            TakeDamage(float.Parse(collision.transform.name));
            Destroy(collision.gameObject);
        }
    }
}
