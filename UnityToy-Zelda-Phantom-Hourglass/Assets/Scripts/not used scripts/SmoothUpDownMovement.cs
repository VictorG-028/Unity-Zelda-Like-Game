using UnityEngine;

public class SmoothUpDownMovement : MonoBehaviour
{
    public float minHeight = 0f; // Altura mínima
    public float maxHeight = 3f; // Altura máxima
    public float movementTime = 2f; // Tempo total para uma subida e descida completa

    private bool goingUp = true; // Flag para controlar se o objeto está subindo ou descendo
    private float elapsedTime = 0f; // Tempo decorrido desde o início da subida ou descida

    // Função pública para ser chamada de outro script
    public void PerformMovement()
    {
        // Incrementa o tempo decorrido
        elapsedTime += Time.deltaTime;

        // Calcula a porcentagem de conclusão do movimento
        float t = elapsedTime / movementTime;

        // Se o objeto estiver subindo
        if (goingUp)
        {
            // Interpola entre a altura mínima e máxima
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(minHeight, maxHeight, t), transform.position.z);

            // Se atingir a altura máxima, inverte a direção
            if (t >= 1f)
            {
                goingUp = false;
                elapsedTime = 0f;
            }
        }
        else // Se o objeto estiver descendo
        {
            // Interpola entre a altura máxima e mínima
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(maxHeight, minHeight, t), transform.position.z);

            // Se atingir a altura mínima, inverte a direção
            if (t >= 1f)
            {
                goingUp = true;
                elapsedTime = 0f;
            }
        }
    }
}
