using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Kecepatan gerak pemain
    public float turnSpeed = 100f; // Kecepatan rotasi pemain

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Mendapatkan referensi ke komponen Rigidbody
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 moveDirection = Vector3.zero;
        float moveInput = 0f;
        float turnInput = 0f;

        // Cek input layar sentuh
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;

            // Mengubah input layar sentuh menjadi input gerakan
            moveInput = (touchPosition.y > Screen.height / 2) ? 1 : -1; // Maju/Mundur berdasarkan layar sentuh
            turnInput = (touchPosition.x > Screen.width / 2) ? 1 : -1; // Rotasi berdasarkan layar sentuh
        }
        else
        {
            // Input dari joystick atau tombol panah
            moveInput = Input.GetAxis("Vertical"); // Untuk maju/mundur
            turnInput = Input.GetAxis("Horizontal"); // Untuk rotasi kiri/kanan
        }

        // Gerakan maju/mundur berdasarkan arah karakter
        moveDirection = transform.forward * moveInput * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveDirection);

        // Rotasi karakter (kiri/kanan)
        float turn = turnInput * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}
