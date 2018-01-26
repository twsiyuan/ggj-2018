using UnityEngine;
using UnityEngine.Audio;

namespace MarsCode113.ServiceFramework
{
    [SystemTag("Audio")]
    public interface IAudioManager : IServiceManager
    {

        /// <summary>
        /// Get audio clip from sound pack.
        /// </summary>
        AudioClip GetClip(int index);


        /// <summary>
        /// Switch audio clip.
        /// </summary>
        void Play(int trackID, AudioClip clip);


        /// <summary>
        /// Continue to play audio.
        /// </summary>
        void Play(int trackID);


        /// <summary>
        /// Play all audio tracks.
        /// </summary>
        void PlayAll();


        /// <summary>
        /// Play one shot from sound pack.
        /// </summary>
        void PlayOneShot(int groupID, AudioClip clip);


        /// <summary>
        /// Pause one audio track.
        /// </summary>
        void Pause(int trackID);


        /// <summary>
        /// Pause all audio tracks.
        /// </summary>
        void PauseAll();


        /// <summary>
        /// Stop one audio track.
        /// </summary>
        void Stop(int trackID);


        /// <summary>
        /// Stop all audio tracks.
        /// </summary>
        void StopAll();


        /// <summary>
        /// Set volume with one track.
        /// </summary>
        void SetVolume(int trackID, float volume);


        /// <summary>
        /// Set volumes with one tracks.
        /// </summary>
        void SetAllVolumes(float volume);


        /// <summary>
        /// Set duration with one track.
        /// </summary>
        void SetDuration(int trackID, float duration);


        /// <summary>
        /// Set durations with all tracks.
        /// </summary>
        void SetAllDurations(float duration);

    }
}