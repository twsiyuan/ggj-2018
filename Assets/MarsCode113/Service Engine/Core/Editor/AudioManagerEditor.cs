using UnityEngine;
using UnityEditor;
using MarsCode113.ServiceFramework;
using System.Collections.Generic;

[CustomEditor(typeof(AudioManager)), CanEditMultipleObjects]
public class AudioManagerEditor : Editor
{

    private AudioManager script;

    private int audioTrackBuildState = 0;

    private int numberOfTracks;

    private bool onSoundPackPreview;

    private string previewPackName;

    private SerializedProperty clips;


    private void OnEnable()
    {
        script = target as AudioManager;
    }


    public override void OnInspectorGUI()
    {
        if(Application.isPlaying) {

            if(onSoundPackPreview)
                DrawSoundPackPreview();
            else
                DrawPlayingSection();
        }
        else
            DrawAudioTrackBuilder();
    }


    private void DrawPlayingSection()
    {
        GUILayout.Space(10);

        DrawAudioTracks();

        GUILayout.Space(10);

        DrawSoundPackSection();

        GUILayout.Space(10);
    }


    private void DrawAudioTracks()
    {
        GUILayout.Label(new GUIContent("Audio Tracks"));

        if(script.AudioTracks.Length > 0) {
            var id = 0;
            foreach(var track in script.AudioTracks)
                DrawAudioTrackSlot(track, id++);
        }
        else
            GUILayout.Label(new GUIContent("N/A"));

    }


    private void DrawAudioTrackBuilder()
    {
        switch(audioTrackBuildState) {
            case 1:
                DrawAudioTrack_State1();
                break;

            default:
                DrawAudioTrack_State0();
                break;
        }
    }


    #region [ Audio Track Slot ]

    private void DrawAudioTrackSlot(AudioTrack track, int index)
    {
        EditorGUILayout.BeginVertical("box");
        {
            DrawIndexUI(track.Index);

            DrawProgressAndVolume(track.Au);

            DrawParametersUI(track);

            DrawControllerUI(index);
        }
        EditorGUILayout.EndVertical();

        GUILayout.Space(5);
    }


    private void DrawIndexUI(string index)
    {
        EditorGUILayout.BeginHorizontal("box");
        {
            GUILayout.Label(new GUIContent("Track"), GUILayout.Width(70));

            GUILayout.Label(new GUIContent(index), EditorStyles.textField);
        }
        EditorGUILayout.EndHorizontal();
    }


    private void DrawProgressAndVolume(AudioSource au)
    {
        EditorGUILayout.BeginHorizontal();
        {
            DrawAudioTrackProgressBar(au);

            DrawVolumeUI(au.volume);
        }
        EditorGUILayout.EndHorizontal();
    }


    private void DrawAudioTrackProgressBar(AudioSource au)
    {
        EditorGUI.BeginDisabledGroup(!au.isPlaying);
        {
            var rect = GUILayoutUtility.GetRect(EditorGUIUtility.currentViewWidth - 230, 17);

            if(au.clip != null) {
                var progress = au.time / au.clip.length;
                EditorGUI.ProgressBar(rect, progress, au.clip.name);
            }
            else
                EditorGUI.ProgressBar(rect, 0, "N/A");
        }
        EditorGUI.EndDisabledGroup();
    }


    private void DrawParametersUI(AudioTrack track)
    {
        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Label(new GUIContent("Volume"), GUILayout.Width(70));

            var volume = EditorGUILayout.Slider(track.Volume, 0, 1);
            if(volume != track.Volume)
                track.Volume = volume;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Label(new GUIContent("Duration"), GUILayout.Width(70));

            var duration = EditorGUILayout.Slider(track.Duration, 0, 5);
            if(duration != track.Duration)
                track.Duration = duration;
        }
        EditorGUILayout.EndHorizontal();
    }


    private void DrawControllerUI(int index)
    {
        EditorGUILayout.BeginHorizontal();
        {
            if(GUILayout.Button("▶", EditorStyles.miniButtonLeft))
                script.Play(index);

            if(GUILayout.Button("❚❚", EditorStyles.miniButtonMid))
                script.Pause(index);

            if(GUILayout.Button("■", EditorStyles.miniButtonRight))
                script.Stop(index);
        }
        EditorGUILayout.EndHorizontal();
    }


    private void DrawVolumeUI(float volume)
    {
        var unit = 17;
        EditorGUILayout.Knob(new Vector2(unit, unit), volume, 0, 1, "", Color.gray, Color.red, false, GUILayout.Width(unit));
    }

    #endregion


    #region [ Audio Track Builder ]

    private void DrawAudioTrack_State0()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        if(GUILayout.Button("Initialize Tracks", EditorStyles.miniButton)) {
            numberOfTracks = 2;
            audioTrackBuildState = 1;
        }
    }


    private void DrawAudioTrack_State1()
    {
        GUILayout.Space(10);

        EditorGUILayout.BeginVertical("box");
        {
            GUILayout.Label(new GUIContent("Number of Tracks"));

            numberOfTracks = EditorGUILayout.IntSlider(numberOfTracks, 1, 10);
        }
        EditorGUILayout.EndVertical();

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        {
            if(GUILayout.Button("Apply", EditorStyles.miniButtonLeft)) {
                CheckChild();
                SetAudioPlayerComponents();
                audioTrackBuildState = 0;
            }

            if(GUILayout.Button("Cancel", EditorStyles.miniButtonRight)) {
                audioTrackBuildState = 0;
            }
        }
        EditorGUILayout.EndHorizontal();
    }


    private void CheckChild()
    {
        var child = script.transform.Find("Audio Player");

        if(child == null) {
            var go = script.gameObject;
            var clone = PrefabUtility.InstantiatePrefab(script.gameObject) as GameObject;

            child = new GameObject("Audio Player").transform;
            child.SetParent(clone.transform);

            PrefabUtility.ReplacePrefab(clone, go, ReplacePrefabOptions.ConnectToPrefab);
            DestroyImmediate(clone, true);
        }
    }


    private void SetAudioPlayerComponents()
    {
        var child = script.transform.Find("Audio Player").gameObject;
        var childComponents = child.GetComponents<AudioSource>();

        var auComponents = new AudioSource[numberOfTracks];
        var max = Mathf.Max(numberOfTracks, childComponents.Length);

        for(int i = 0; i < max; i++) {
            if(numberOfTracks > i) {
                if(childComponents.Length > i)
                    auComponents[i] = childComponents[i];
                else
                    auComponents[i] = child.AddComponent<AudioSource>();
            }
            else
                DestroyImmediate(childComponents[i], true);
        }

        SetAudioTrackGroups(auComponents);
    }


    private void SetAudioTrackGroups(AudioSource[] auComponents)
    {
        var audioTracks = new AudioTrack[numberOfTracks];
        var pointer = 0;

        foreach(var au in auComponents) {
            au.playOnAwake = false;
            au.loop = true;

            audioTracks[pointer] = new AudioTrack(au);
            pointer++;
        }

        script.AudioTracks = audioTracks;
    }

    #endregion


    #region [ Sound Pack Preview ]

    private void DrawSoundPackSection()
    {
        GUILayout.Label(new GUIContent("Sound Pack"));

        if(script.SoundPack != null)
            DrawSoundPackSlot(script.SoundPack);
        else
            GUILayout.Label(new GUIContent("N/A"));
    }


    private void DrawSoundPackSlot(SoundPack pack)
    {
        EditorGUILayout.BeginVertical("box");
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Label(new GUIContent("Pack"), GUILayout.Width(70));

                GUILayout.Label(new GUIContent(pack.Index), EditorStyles.textField);
            }
            EditorGUILayout.EndHorizontal();

            if(GUILayout.Button("Preview", EditorStyles.miniButton)) {
                previewPackName = pack.Index;

                var so = new SerializedObject(pack);
                clips = so.FindProperty("clips");

                onSoundPackPreview = true;
            }
        }
        EditorGUILayout.EndVertical();
    }


    private void DrawSoundPackPreview()
    {
        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal("box");
        {
            GUILayout.Label(new GUIContent("Pack"), GUILayout.Width(70));

            GUILayout.Label(new GUIContent(previewPackName), EditorStyles.textField);
        }
        EditorGUILayout.EndHorizontal();

        GUI.enabled = false;

        var len = clips.arraySize;

        for(var i = 0; i < len; i++) {
            EditorGUILayout.BeginHorizontal();
            {
                var e = clips.GetArrayElementAtIndex(i);

                GUILayout.Label(new GUIContent(( i + 1 ).ToString() + "."), GUILayout.Width(20));

                EditorGUILayout.ObjectField(e, new GUIContent(""));
            }
            EditorGUILayout.EndHorizontal();
        }

        GUI.enabled = true;

        GUILayout.Space(5);

        if(GUILayout.Button("Back", EditorStyles.miniButton)) {
            onSoundPackPreview = false;
            clips = null;
        }
    }

    #endregion

}