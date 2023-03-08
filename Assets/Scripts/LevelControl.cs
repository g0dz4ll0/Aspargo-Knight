using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public int index;
    public string levelName;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Carregar o nível com o índice da build
            SceneManager.LoadScene(index);

            //Carregar o nível com o nome da cena
            //SceneManager.LoadScene(levelName);

            //Recomeçar o nível
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
