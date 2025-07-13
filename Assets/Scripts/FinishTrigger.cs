using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    // Slot untuk menampung Panel UI dari Inspector
    public GameObject panelKemenangan;

    // Fungsi ini dipanggil saat pemain masuk ke trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Cek apakah yang masuk adalah Player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player mencapai garis finis!");

            // Jika panel sudah di-assign
            if (panelKemenangan != null)
            {
                // Munculkan panelnya!
                panelKemenangan.SetActive(true);

                // (Opsional) Menghentikan waktu agar game berhenti
                Time.timeScale = 0f;
            }
            else
            {
                Debug.LogWarning("Panel Kemenangan belum di-assign di Inspector!");
            }
        }
    }
}