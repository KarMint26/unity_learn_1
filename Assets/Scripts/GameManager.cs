using UnityEngine;
using TMPro; // Diperlukan untuk menggunakan TextMeshPro

public class GameManager : MonoBehaviour
{
    // --- Singleton Pattern ---
    // Ini memastikan hanya ada satu GameManager di seluruh game
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // -------------------------

    [Header("Pengaturan Koin")]
    public TextMeshProUGUI teksKoin; // Slot untuk UI Teks
    private int jumlahKoin = 0;

    // Fungsi ini dipanggil untuk menambah koin
    public void TambahKoin(int jumlah)
    {
        jumlahKoin += jumlah;
        UpdateTeksKoin();
        Debug.Log("Koin ditambahkan! Total sekarang: " + jumlahKoin);
    }

    // Fungsi untuk mengupdate tampilan UI
    private void UpdateTeksKoin()
    {
        if (teksKoin != null)
        {
            teksKoin.text = " " + jumlahKoin;
        }
    }
}