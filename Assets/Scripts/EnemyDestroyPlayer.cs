using UnityEngine;

public class EnemyDestroyPlayer : MonoBehaviour {
    public AudioSource _source;

    public void playAudio() {
        _source.Play();
    }
    
}
