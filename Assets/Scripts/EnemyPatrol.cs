using UnityEngine;
using UnityEngine.SceneManagement; // Diperlukan untuk me-restart scene

public class EnemyPatrol : MonoBehaviour
{
    [Header("Pengaturan Patroli")]
    public float moveSpeed = 2f;         // Kecepatan gerak musuh
    private bool facingRight = true;     // Untuk tahu arah hadap musuh

    [Header("Deteksi Dinding & Tepi")]
    public Transform wallCheck;          // Titik untuk mengecek dinding di depan
    public Transform edgeCheck;          // Titik untuk mengecek tepi jurang
    public float checkRadius = 0.1f;     // Radius lingkaran deteksi
    public LayerMask whatIsGround;       // Layer apa yang dianggap sebagai tanah/dinding

    private bool hittingWall;            // Status apakah menabrak dinding
    private bool atEdge;                 // Status apakah berada di tepi jurang

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Mengecek kondisi di depan musuh menggunakan lingkaran tak terlihat
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, whatIsGround);
        atEdge = !Physics2D.OverlapCircle(edgeCheck.position, checkRadius, whatIsGround);

        // --- TAMBAHKAN INI UNTUK DEBUGGING ---
        Debug.Log("Hitting Wall: " + hittingWall + " | At Edge: " + atEdge);
        // ------------------------------------

        // Jika menabrak dinding atau berada di tepi, berbalik arah
        if (hittingWall || atEdge)
        {
            Flip();
        }

        // Memberi kecepatan pada musuh sesuai arah hadapnya
        if (facingRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
    }

    // Fungsi untuk membalik arah musuh
    void Flip()
    {
        facingRight = !facingRight; // Membalik status arah hadap
        Vector3 newScale = transform.localScale;
        newScale.x *= -1; // Membalik skala sumbu X (membalik gambar)
        transform.localScale = newScale;
    }

    // Fungsi ini dipanggil saat musuh bersentuhan dengan collider lain
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cek jika yang disentuh adalah Player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player tersentuh musuh! Merestart level...");
            
            // Me-restart scene yang sedang aktif
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // (Opsional) Menggambar lingkaran deteksi di Editor untuk mempermudah debugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (wallCheck != null) Gizmos.DrawWireSphere(wallCheck.position, checkRadius);
        if (edgeCheck != null) Gizmos.DrawWireSphere(edgeCheck.position, checkRadius);
    }
}