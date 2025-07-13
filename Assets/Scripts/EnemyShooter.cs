using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("Pengaturan Tembak")]
    public GameObject peluruMusuhPrefab;
    public Transform titikTembak;
    public float kecepatanPeluru = 5f;
    public float fireRate = 2f; // Akan menembak setiap 2 detik

    private float timer = 0f;
    private Transform playerTransform;

    void Start()
    {
        // Cari objek player berdasarkan tag-nya
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    void Update()
    {
        // Jika tidak ada player, jangan lakukan apa-apa
        if (playerTransform == null) return;

        // Membuat musuh selalu menghadap ke arah player
        if (playerTransform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Menghadap ke kiri
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // Menghadap ke kanan
        }

        // Logika Timer untuk menembak
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            Tembak();
            timer = 0f; // Reset timer
        }
    }

    void Tembak()
    {
        if (peluruMusuhPrefab != null && titikTembak != null)
        {
            GameObject peluru = Instantiate(peluruMusuhPrefab, titikTembak.position, Quaternion.identity);
            Rigidbody2D rbPeluru = peluru.GetComponent<Rigidbody2D>();

            // Beri kecepatan ke arah hadap musuh
            rbPeluru.velocity = new Vector2(kecepatanPeluru * transform.localScale.x, 0);
        }
    }
}