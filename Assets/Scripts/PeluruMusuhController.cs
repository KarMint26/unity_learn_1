using UnityEngine;
using UnityEngine.SceneManagement;

public class PeluruMusuhController : MonoBehaviour
{
    [Header("Pengaturan Ledakan")]
    public float jarakMaksimum = 7f;     // Jarak yang ditempuh sebelum meledak
    public float radiusLedakan = 1.5f;    // Radius area ledakan
    public GameObject efekLedakanPrefab;  // (Opsional) Prefab untuk efek visual ledakan

    private Vector3 posisiAwal;
    private bool sudahMeledak = false;

    void Start()
    {
        // Simpan posisi awal saat peluru muncul
        posisiAwal = transform.position;
    }

    void Update()
    {
        // Hitung jarak yang sudah ditempuh dari posisi awal
        float jarakDitempuh = Vector3.Distance(posisiAwal, transform.position);

        // Jika jarak sudah melebihi batas maksimum dan belum pernah meledak
        if (jarakDitempuh >= jarakMaksimum && !sudahMeledak)
        {
            Meledak();
        }
    }

    void Meledak()
    {
        sudahMeledak = true;

        if (efekLedakanPrefab != null)
        {
            Instantiate(efekLedakanPrefab, transform.position, Quaternion.identity);
        }

        // Deteksi semua collider dalam radius ledakan
        Collider2D[] objekTerkena = Physics2D.OverlapCircleAll(transform.position, radiusLedakan);

        foreach (Collider2D objek in objekTerkena)
        {
            // Jika ledakan mengenai objek dengan tag "Player"
            if (objek.CompareTag("Player"))
            {
                Debug.Log("Player terkena ledakan musuh!");
                // Langsung restart level
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                // Hentikan fungsi di sini karena scene akan di-load ulang
                return; 
            }
        }

        // Hancurkan peluru itu sendiri jika tidak mengenai player
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Jika menabrak Player atau tanah, langsung picu ledakan
        if ((other.CompareTag("Player") || other.CompareTag("Ground")) && !sudahMeledak)
        {
            Meledak();
        }
    }
    
    // (Opsional) Menggambar lingkaran radius ledakan di editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Kita beri warna merah untuk membedakan dari ledakan player
        Gizmos.DrawWireSphere(transform.position, radiusLedakan);
    }
}