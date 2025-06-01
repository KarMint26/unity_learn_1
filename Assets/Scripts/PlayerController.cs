using UnityEngine;
using UnityEngine.EventSystems; // Diperlukan untuk menangani event pada UI

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public float moveSpeed = 3.5f; // Kecepatan gerak player
    public float jumpForce = 5f;   // Kekuatan lompat player

    private bool moveRightPressed = false;
    private bool moveLeftPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        // Pastikan Rigidbody2D dan Animator sudah di-assign di Inspector
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Proses input dari keyboard (untuk testing di Unity Editor)
        HandleKeyboardInput();

        // Proses pergerakan berdasarkan status tombol UI
        HandleMovement();
    }

    void HandleKeyboardInput()
    {
        // Tombol D untuk bergerak ke kanan
        if (Input.GetKeyDown(KeyCode.D))
        {
            PressRight();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            ReleaseRight();
        }

        // Tombol A untuk bergerak ke kiri
        if (Input.GetKeyDown(KeyCode.A))
        {
            PressLeft();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            ReleaseLeft();
        }

        // Tombol Spasi untuk melompat
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PressJump();
        }
    }

    void HandleMovement()
    {
        if (moveRightPressed)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1); // Menghadap ke kanan
            anim.SetBool("running", true);
        }
        else if (moveLeftPressed)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1); // Menghadap ke kiri
            anim.SetBool("running", true);
        }
        else
        {
            // Berhenti bergerak horizontal jika tidak ada tombol arah yang ditekan
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("running", false);
        }
    }

    // Public methods untuk dihubungkan dengan tombol UI

    // Dipanggil saat tombol KANAN ditekan
    public void PressRight()
    {
        moveRightPressed = true;
        moveLeftPressed = false; // Pastikan tidak bergerak ke kiri secara bersamaan
    }

    // Dipanggil saat tombol KANAN dilepas
    public void ReleaseRight()
    {
        moveRightPressed = false;
    }

    // Dipanggil saat tombol KIRI ditekan
    public void PressLeft()
    {
        moveLeftPressed = true;
        moveRightPressed = false; // Pastikan tidak bergerak ke kanan secara bersamaan
    }

    // Dipanggil saat tombol KIRI dilepas
    public void ReleaseLeft()
    {
        moveLeftPressed = false;
    }

    // Dipanggil saat tombol LOMPAT ditekan
    public void PressJump()
    {
        // Tambahkan pengecekan "isGrounded" jika diperlukan agar tidak lompat di udara
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}