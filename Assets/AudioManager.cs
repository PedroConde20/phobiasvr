using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Singleton para acceder f�cilmente desde otros scripts

    public AudioSource[] audioSources; // Almacena las instancias de AudioSource

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Configura y crea instancias de AudioSource
        audioSources = new AudioSource[3]; // Puedes ajustar el tama�o seg�n tus necesidades

        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        // Busca un AudioSource disponible y reproduce el sonido
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (!audioSources[i].isPlaying)
            {
                audioSources[i].clip = clip;
                audioSources[i].volume = volume;
                audioSources[i].Play();
                return;
            }
        }
    }
}