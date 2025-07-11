using UnityEngine;

public class PeluruController : MonoBehaviour
{
    [Header("Pengaturan Ledakan")]
    public float jarakMaksimum = 5f;
    public float radiusLedakan = 2f;
    public GameObject efekLedakanPrefab;

    private Vector3 posisiAwal;
    private bool sudahMeledak = false;

    void Start()
    {
        posisiAwal = transform.position;
    }

    void Update()
    {
        float jarakDitempuh = Vector3.Distance(posisiAwal, transform.position);
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

        Collider2D[] objekTerkena = Physics2D.OverlapCircleAll(transform.position, radiusLedakan);

        foreach (Collider2D objek in objekTerkena)
        {
            if (objek.CompareTag("Enemy"))
            {
                // --- LOGIKA PENAMBAHAN KOIN PINDAH KE SINI ---
                // Koin hanya akan bertambah jika musuh terdeteksi dalam ledakan.
                if (GameManager.instance != null)
                {
                    GameManager.instance.TambahKoin(10);
                }
                // ---------------------------------------------

                // Hancurkan musuh tersebut
                Destroy(objek.gameObject);
            }
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Jika menabrak musuh atau tanah, dan belum pernah meledak
        if ((other.CompareTag("Enemy") || other.CompareTag("Ground")) && !sudahMeledak)
        {
            // Panggil Meledak() tapi JANGAN tambah koin di sini lagi.
            Meledak();
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusLedakan);
    }
}