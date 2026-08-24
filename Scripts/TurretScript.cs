using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurretScript : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;

    [SerializeField] private Transform[] firePoints;
    [SerializeField] private GameObject bulletPrefab;
    public TurretData data;
    public int SellPrice;

    private float fireRate, speedOfBullet, damage;
    private bool canRotate;
    private Transform target;

    void Start()
    {
        fireRate = data.FireRate;
        speedOfBullet = data.SpeedOfBullet;
        damage = data.Damage;
        name = data.TurretName;
        SellPrice = data.Price;
        canRotate = data.CanRotate;

        StartCoroutine(Shoot(fireRate));
    }

    void Update()
    {
       if(!canRotate)
        {
            return;
        }
       if(target)
        {
            //See what to turn in tower, depending on the model
            transform.GetChild(0).GetChild(0).LookAt
            (target.position, Vector3.up);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if(!target && other.tag == "Enemy")
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform == target)
        {
            target = null;
        }
    }

    IEnumerator Shoot(float delay)
    {
        if (target)
        {
            foreach (Transform firePoint in firePoints)
            {
                Rigidbody currentBullet = Instantiate(bulletPrefab,
                firePoint.position, firePoint.rotation).GetComponent<Rigidbody>();

                currentBullet.AddForce(firePoint.forward * speedOfBullet);
                currentBullet.transform.name = damage.ToString();
                Destroy(currentBullet.gameObject, 1f);
            }
            
            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
        }
        yield return new WaitForSeconds(delay);
        StartCoroutine(Shoot(fireRate));
    }
}
