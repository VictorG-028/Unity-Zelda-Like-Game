using UnityEngine;

public class SmoothUpDownMovement : MonoBehaviour
{
    public float minHeight = 0f; // Altura m�nima
    public float maxHeight = 3f; // Altura m�xima
    public float movementTime = 2f; // Tempo total para uma subida e descida completa

    private bool goingUp = true; // Flag para controlar se o objeto est� subindo ou descendo
    private float elapsedTime = 0f; // Tempo decorrido desde o in�cio da subida ou descida

    // Fun��o p�blica para ser chamada de outro script
    public void PerformMovement()
    {
        // Incrementa o tempo decorrido
        elapsedTime += Time.deltaTime;

        // Calcula a porcentagem de conclus�o do movimento
        float t = elapsedTime / movementTime;

        // Se o objeto estiver subindo
        if (goingUp)
        {
            // Interpola entre a altura m�nima e m�xima
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(minHeight, maxHeight, t), transform.position.z);

            // Se atingir a altura m�xima, inverte a dire��o
            if (t >= 1f)
            {
                goingUp = false;
                elapsedTime = 0f;
            }
        }
        else // Se o objeto estiver descendo
        {
            // Interpola entre a altura m�xima e m�nima
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(maxHeight, minHeight, t), transform.position.z);

            // Se atingir a altura m�nima, inverte a dire��o
            if (t >= 1f)
            {
                goingUp = true;
                elapsedTime = 0f;
            }
        }
    }
}
