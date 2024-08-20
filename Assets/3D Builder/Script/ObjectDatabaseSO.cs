using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ObjectDatabaseSO : ScriptableObject
{
    public List<ObjectData> objectsData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [Serializable]
    public class ObjectData
    {
        [field: SerializeField]
        public string Name {  get; private set; }
        [field: SerializeField]
        public int ID { get; private set; }

        [field: SerializeField]
        public Vector2Int size {  get; private set; }=Vector2Int.one;

        [field: SerializeField]
        public GameObject Prefabs { get; private set; }
    }
}
