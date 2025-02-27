using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JellyType
{
    White = 0,
    Red = 1,
    Blue = 2,
    Green = 3,
    Purple = 4,
    Yellow = 5,
    Orange = 6,
}

public class RouletteInfinityJellyComponent : MonoBehaviour
{
    public JellyType jellyType;

    [SerializeField] private Material[] _jellyinifitiycomponentColors;
    [SerializeField] private ParticleSystem _destroyEffect;

    public float intensity = 0.5f;
    public float mass = 1f;
    public float stiffness = 0.5f;
    public float damping = 0.75f;

    private Mesh _meshClone;
    private MeshRenderer _renderer;
    private JellyVertex[] _jellyVertices;
    private Vector3[] _vertexArray;

    private AudioClip _destroyClip;

    public void Init()
    {
        _destroyClip = Resources.Load("Audio/destroy") as AudioClip;
        int index = (int)jellyType;
        Mesh originalMesh = GetComponent<MeshFilter>().sharedMesh;
        _meshClone = Instantiate(originalMesh);
        GetComponent<MeshFilter>().sharedMesh = _meshClone;
        _renderer = GetComponent<MeshRenderer>();

        _renderer.sharedMaterial = _jellyinifitiycomponentColors[index];
        _vertexArray = _meshClone.vertices;
        _jellyVertices = new JellyVertex[_vertexArray.Length];

        for (int i = 0; i < _vertexArray.Length; i++)
        {
            _jellyVertices[i] = new JellyVertex(i, transform.TransformPoint(_vertexArray[i]));
        }
    }

    private void Update()
    {
        Bounds bounds = _renderer.bounds;
        Vector3[] updatedVertices = new Vector3[_vertexArray.Length];

        Matrix4x4 localToWorld = transform.localToWorldMatrix;
        Matrix4x4 worldToLocal = transform.worldToLocalMatrix;

        for (int i = 0; i < _jellyVertices.Length; i++)
        {
            Vector3 target = localToWorld.MultiplyPoint3x4(_vertexArray[_jellyVertices[i].ID]);
            float intensityFactor = Mathf.Clamp01(1 - (bounds.max.y - target.y) / bounds.size.y) * intensity;

            _jellyVertices[i].Shake(target, mass, stiffness, damping);
            updatedVertices[_jellyVertices[i].ID] = worldToLocal.MultiplyPoint3x4(_jellyVertices[i].Position);
        }

        _meshClone.vertices = updatedVertices;

        if (Time.frameCount % 8 == 0)
        {
            _meshClone.RecalculateNormals();
        }
    }

    public void JellDestroy()
    {
        SettingsManager.playSound?.Invoke(_destroyClip);
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
        {
            ParticleSystem system = Instantiate(_destroyEffect, transform.position, transform.rotation);
            system.GetComponent<ParticleSystemRenderer>().sharedMaterial = _renderer.sharedMaterial;
            GameController.DestroyedCount += 1;
            Destroy(gameObject);
            GameController.checkingEnd?.Invoke();
        });
    }

    private class JellyVertex
    {
        public int ID;
        public Vector3 Position;
        public Vector3 Velocity;
        public Vector3 Force;

        public JellyVertex(int id, Vector3 pos)
        {
            ID = id;
            Position = pos;
            Velocity = Vector3.zero;
            Force = Vector3.zero;
        }

        public void Shake(Vector3 target, float mass, float stiffness, float damping)
        {
            Force = (target - Position) * stiffness;
            Velocity = (Velocity + Force / mass) * damping;
            Position += Velocity;

            if (Velocity.magnitude < 0.001f)
            {
                Position = target;
                Velocity = Vector3.zero;
            }
        }
    }
}