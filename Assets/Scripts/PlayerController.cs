using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    // --- PENGATURAN PUBLIK ---
    [Header("Komponen & Pengaturan")]
    public Rigidbody2D rb;
    public Animator anim;

    [Header("Statistik Gerakan")]
    public float moveSpeed = 3.5f;
    public float jumpForce = 15f;

    // --- TAMBAHAN UNTUK MENEMBAK ---
    [Header("Pengaturan Tembak")]
    public GameObject peluruPrefab;      // Slot untuk Prefab peluru
    public Transform titikTembak;        // Titik munculnya peluru
    public float kecepatanPeluru = 10f;  // Kecepatan peluru
    // ---------------------------------

    // --- VARIABEL INTERNAL ---
    private bool moveRightPressed = false;
    private bool moveLeftPressed = false;
    private bool isGrounded = false;

    // Fungsi Start dipanggil sekali saat game dimulai
    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (anim == null) anim = GetComponent<Animator>();
    }

    // Fungsi Update dipanggil setiap frame, cocok untuk input
    void Update()
    {
        HandleKeyboardInput();
        UpdateAnimatorParameters();
    }

    // FixedUpdate dipanggil dalam interval waktu yang tetap, cocok untuk fisika.
    void FixedUpdate()
    {
        HandleMovement();
    }

    // Mengelola semua input
    void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.D)) PressRight();
        if (Input.GetKeyUp(KeyCode.D)) ReleaseRight();

        if (Input.GetKeyDown(KeyCode.A)) PressLeft();
        if (Input.GetKeyUp(KeyCode.A)) ReleaseLeft();

        if (Input.GetKeyDown(KeyCode.Space)) PressJump();
        
        // TAMBAHAN: Input keyboard untuk testing tembak
        if (Input.GetKeyDown(KeyCode.F))
        {
            Tembak();
        }
    }

    // Menerapkan perubahan fisika berdasarkan input
    void HandleMovement()
    {
        if (moveRightPressed)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else if (moveLeftPressed)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    // Mengupdate semua parameter di Animator
    void UpdateAnimatorParameters()
    {
        anim.SetBool("isGrounded", isGrounded);
        bool isRunning = Mathf.Abs(rb.velocity.x) > 0.1f && isGrounded;
        anim.SetBool("running", isRunning);
    }

    // --- FUNGSI PUBLIK UNTUK TOMBOL UI ---
    public void PressRight() { moveRightPressed = true; moveLeftPressed = false; }
    public void ReleaseRight() { moveRightPressed = false; }
    public void PressLeft() { moveLeftPressed = true; moveRightPressed = false; }
    public void ReleaseLeft() { moveLeftPressed = false; }

    public void PressJump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            anim.SetTrigger("jump");
        }
    }

    // --- FUNGSI BARU UNTUK MENEMBAK ---
    public void Tembak()
    {
        if (peluruPrefab != null && titikTembak != null)
        {
            // Buat instance/copy dari prefab peluru di posisi dan rotasi titikTembak
            GameObject peluru = Instantiate(peluruPrefab, titikTembak.position, titikTembak.rotation);
            Rigidbody2D rbPeluru = peluru.GetComponent<Rigidbody2D>();

            // Beri kecepatan pada peluru
            // transform.localScale.x akan bernilai 1 jika menghadap kanan, dan -1 jika menghadap kiri
            rbPeluru.velocity = new Vector2(kecepatanPeluru * transform.localScale.x, 0f);
        }
        else
        {
            Debug.LogWarning("Peluru Prefab atau Titik Tembak belum di-assign di Inspector!");
        }
    }

    // --- FUNGSI DETEKSI FISIKA ---
    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.7f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}