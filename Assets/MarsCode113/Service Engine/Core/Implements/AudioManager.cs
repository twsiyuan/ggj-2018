using UnityEngine;

namespace MarsCode113.ServiceFramework
{
    public class AudioManager : ServiceManager_Base, IAudioManager
    {

        #region [ Fields / Properties ]

        [SerializeField]
        private AudioTrack[] audioTracks;

        [SerializeField]
        private SoundPack soundPack;

        #endregion


        #region [ Basic ]

        public override void Clean()
        {
            foreach(var track in audioTracks) {
                CheckAudioTrackLauncher(track);
                track.Clean();
            }
        }


        public AudioClip GetClip(int index)
        {
            return SoundPack.GetClipAt(index);
        }

        #endregion


        #region [ Core ]

        public void Play(int trackID, AudioClip clip)
        {
            var track = audioTracks[trackID];

            CheckAudioTrackLauncher(track);

            track.ReadyToSwitch(clip);

            StartCoroutine(track.Launcher);
        }


        public void Play(int trackID)
        {
            var track = audioTracks[trackID];

            CheckAudioTrackLauncher(track);

            track.ReadyToPlay();

            StartCoroutine(track.Launcher);
        }


        public void PlayAll()
        {
            for(var trackID = 0; trackID < audioTracks.Length; trackID++)
                Play(trackID);
        }


        public void Pause(int trackID)
        {
            var track = audioTracks[trackID];

            CheckAudioTrackLauncher(track);

            track.ReadyToPause();

            StartCoroutine(track.Launcher);
        }


        public void PauseAll()
        {
            for(var trackID = 0; trackID < audioTracks.Length; trackID++)
                Pause(trackID);
        }


        public void Stop(int trackID)
        {
            var track = audioTracks[trackID];

            CheckAudioTrackLauncher(track);

            track.ReadyToStop();

            StartCoroutine(track.Launcher);
        }


        public void StopAll()
        {
            for(var trackID = 0; trackID < audioTracks.Length; trackID++)
                Stop(trackID);
        }


        public void PlayOneShot(int trackID, AudioClip clip)
        {
            audioTracks[trackID].PlayOneShot(clip);
        }

        #endregion


        #region [ Utility ]

        public void SetVolume(int trackID, float volume)
        {
            var track = audioTracks[trackID];
            track.Volume = volume;
        }


        public void SetAllVolumes(float volume)
        {
            foreach(var track in audioTracks)
                track.Volume = volume;
        }


        public void SetDuration(int trackID, float duration)
        {
            var track = audioTracks[trackID];
            track.Duration = duration;
        }


        public void SetAllDurations(float duration)
        {
            foreach(var track in audioTracks)
                track.Duration = duration;
        }


        private void CheckAudioTrackLauncher(AudioTrack track)
        {
            if(track.Launcher != null)
                StopCoroutine(track.Launcher);
        }

        #endregion


        #region [ Editor Compilation ]
#if UNITY_EDITOR
        public AudioTrack[] AudioTracks {
            get { return audioTracks; }
            set { audioTracks = value; }
        }

        public SoundPack SoundPack {
            get { return soundPack; }
        }
#endif
        #endregion

    }
}