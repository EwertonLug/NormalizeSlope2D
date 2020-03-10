using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalizeSlope : MonoBehaviour
{
    public LayerMask whatIsGround;
    public float MaximumTilt = 35;
    public bool grounded = false;
    public float hitSize = 2f;
    // @NOTE Deve ser chamado de FixedUpdate () para funcionar corretamente
    public void MoveAndNormalizeSlope(float directionX, float speed)
    {
        Vector2 forceDirection = new Vector2(0,0);
        // Verifique se há chao abaixo.
        RaycastHit2D hitDirection = Physics2D.Raycast(transform.position, Vector2.down, hitSize, whatIsGround);
        //Desenhando hit
        Debug.DrawRay(transform.position, Vector2.down * hitSize, Color.yellow);
        // Se houver chão abaixo.
        if (hitDirection.collider != null)
        {
            grounded = true;
            // Desenhando um raio com a diração co chao detectado (Normals)
            Debug.DrawRay(transform.position, hitDirection.normal, Color.white);
           
            // Se isso não apontar para cima, estamos em uma inclinação
            // e deve ajustar nossa forceDirection ...

            // Obtenha um vetor perpendicular ao normal.
            // Isso apontará para cima ao longo da inclinação.

            if (directionX >= 0) // Right
                forceDirection = new Vector2(hitDirection.normal.y, -hitDirection.normal.x);
            else // Left
                forceDirection = new Vector2(-hitDirection.normal.y, hitDirection.normal.x);

        }else{
            grounded = false;
        }
        

        float angle = 0;
       
        // Criando um raio para direção que detecta o angulo do obstáculo a  frente
        RaycastHit2D hitAngle = Physics2D.Raycast(transform.position, forceDirection, 0.5f, whatIsGround);
        Debug.DrawRay(transform.position, hitAngle.normal, Color.blue);
        //Pegando angulo do chao
        angle = Vector2.Angle(hitAngle.normal, Vector2.up);
        //Verifica Angulo
        if (angle < MaximumTilt)
        {
             // Criando um raio na direção à seguir 
            Debug.DrawRay(transform.position, forceDirection * Mathf.Abs(directionX), Color.green);
            //Move o player
            transform.Translate(forceDirection * Mathf.Abs(directionX) * speed);
        }else{
             // Criando um raio na direção à seguir 
            Debug.DrawRay(transform.position, forceDirection * Mathf.Abs(directionX), Color.red);
        }
    }
}
