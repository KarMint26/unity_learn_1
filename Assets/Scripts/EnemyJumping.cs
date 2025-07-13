using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyJumping : MonoBehaviour
{
    [Header("Pengaturan Gerak")]
    public float moveSpeed = 3f;
    public Transform posisiAtas_Obj;
    public Transform posisiBawah_Obj;

    // --- PERBAIKAN LOGIKA ---
    private Vector3 posisiAtas;
    private Vector3 posisiBawah;
    private Vector3 targetPosisi;
    // -------------------------

    void Start()
    {
        // --- PERBAIKAN LOGIKA ---
        // Simpan koordinat dunia dari titik atas dan bawah di awal
        posisiAtas = posisiAtas_Obj.position;
        posisiBawah = posisiBawah_Obj.position;
        // -------------------------

        // Set posisi target awal ke atas
        targetPosisi = posisiAtas;
    }

    void Update()
    {
        // Gerakkan musuh menuju posisi target
        transform.position = Vector3.MoveTowards(transform.position, targetPosisi, moveSpeed * Time.deltaTime);

        // Cek jika musuh sudah sangat dekat dengan target
        if (Vector3.Distance(transform.position, targetPosisi) < 0.1f)
        {
            // --- PERBAIKAN LOGIKA ---
            // Jika targetnya adalah posisi atas, ganti target ke posisi bawah
            if (targetPosisi == posisiAtas)
            {
                targetPosisi = posisiBawah;
            }
            // Jika targetnya adalah posisi bawah, ganti target ke posisi atas
            else
            {
                targetPosisi = posisiAtas;
            }
            // -------------------------
        }
    }

    // Fungsi ini tetap berguna jika pemain menyentuh musuh
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}