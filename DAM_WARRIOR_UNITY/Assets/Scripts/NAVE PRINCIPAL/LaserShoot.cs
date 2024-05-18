using UnityEngine;
using UnityEngine.InputSystem;

public class LaserShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // Ya no se usar� directamente para instanciar
    public float bulletSpeed = 10f;
    private AudioSource audioSource;
    private AudioSource audioSource2;
    public AudioClip shootSound;
    public GameObject specialBulletPrefab; // Ya no se usar� directamente para instanciar
    public AudioClip specialShootSound;
    private Transform escalador;
    private bool isShooting = false;

    void Start()
    {
        escalador = GameObject.Find("Escalador").transform;
        audioSource = GameObject.Find("SFX_SHOOT").GetComponent<AudioSource>();
        audioSource2 = GameObject.Find("SFX_SHOOT2").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Shoot();
        }

        if (Input.GetButtonDown("Fire1") && GameManager.fullPower && !isShooting)
        {
            ShootSpecial();
        }
    }

    void Shoot()
    {
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // Aseg�rate de que PuntoDisparo existe y tiene un transform
        Transform firePoint = transform.Find("PuntoDisparo");
        if (firePoint == null)
        {
            Debug.LogError("No se encontr� el punto de disparo. Aseg�rate de que el objeto 'PuntoDisparo' existe como hijo en la nave.");
            return;
        }

        // Obteniendo la bala del pool y verificando que no sea nula
        GameObject bullet = BulletPool.Instance.GetBullet();
        if (bullet == null)
        {
            Debug.LogError("No se pudo obtener una bala del pool. Aseg�rate de que el BulletPool est� configurado correctamente.");
            return;
        }

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No se encontr� Rigidbody2D en la bala. Aseg�rate de que todas las balas tienen un Rigidbody2D.");
            return;
        }
        rb.velocity = transform.right * bulletSpeed;
    }


    void ShootSpecial()
    {
        if (specialShootSound != null && audioSource2 != null)
        {
            audioSource2.PlayOneShot(specialShootSound);
        }

        escalador.localScale = new Vector3(0f, escalador.localScale.y, escalador.localScale.z);
        GameManager.fullPower = false;
        isShooting = true;

        GameObject specialBullet = BulletPool.Instance.GetSpecialBullet(); // Usar el m�todo GetSpecialBullet
        specialBullet.transform.position = transform.Find("PuntoDisparo").position;
        specialBullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rb = specialBullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed * 3; // Uso de velocidad directa

        Invoke("ResetIsShooting", 0.5f);
    }


    void ResetIsShooting()
    {
        isShooting = false;
    }
}
