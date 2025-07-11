using UnityEngine;

public class Portal : MonoBehaviour
{
    // Variabel ini akan muncul di Inspector untuk kita isi
    public string namaSceneTujuan;

    // Fungsi ini akan otomatis terpanggil saat ada objek masuk ke Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Pertama, kita cek apakah yang masuk adalah si Player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player masuk portal, memuat scene: " + namaSceneTujuan);

            // Cari script 'load_scenes' yang ada di scene
            load_scenes sceneLoader = FindObjectOfType<load_scenes>();

            // Jika scriptnya ketemu, panggil fungsi ChangeScene
            if (sceneLoader != null)
            {
                sceneLoader.ChangeScene(namaSceneTujuan);
            }
            else
            {
                Debug.LogError("Script 'load_scenes' tidak ditemukan di scene!");
            }
        }
    }
}