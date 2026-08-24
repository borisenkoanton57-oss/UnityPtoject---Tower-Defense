using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    public static UIButtonSound Instance;
    [SerializeField] private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        Instance = this;
    }

     public void PlayClick()
    {
        audioSource.Play();
    }
}
