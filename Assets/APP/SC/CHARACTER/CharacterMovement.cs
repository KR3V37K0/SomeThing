using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] Vector2 speed;
    [SerializeField] CharacterController controller;
    [SerializeField] Transform trans_camera, trans_target;
    Vector2 vec_input;
    Vector3 vec_move;
    Vector3 camForward;
    Vector3 camRight;

    [SerializeField] float rotation_speed;
    float input_rotation;


    private float turnSmoothVelocity;
    [SerializeField] private float turnSmoothTime = 0.1f; // регулирует плавность поворота


    private void Update()
    {
        camForward = trans_camera.forward;
        camRight = trans_camera.right;
        //нормализовать???

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        vec_move = (camForward * vec_input.y * speed.y) + (camRight * vec_input.x * speed.x);



        // Плавный поворот в сторону движения
        Vector3 moveDir = vec_move;
        moveDir.y = 0f;

        if (moveDir.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
        }




        controller.Move(vec_move * Time.deltaTime);

        //transform.rotation *= Quaternion.AngleAxis(rotation_speed*input_rotation, Vector3.up);
    }
    public void GetMove(InputAction.CallbackContext ctx)
    {
        vec_input = ctx.ReadValue<Vector2>();
    }
    public void GetLook(InputAction.CallbackContext ctx)
    {
        input_rotation = ctx.ReadValue<Vector2>().x;
    }
}
