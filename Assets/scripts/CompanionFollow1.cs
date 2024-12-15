using System; // Add this line
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionFollow1 : MonoBehaviour
{
    public Transform target = null;               

    public TargetRecord[] _recordsArray = null;
    public int _a = 0;
    public int _b = 1;
    public TargetRecord _record = null;
    public int _arraySize = 20;
    public bool recordData = false;

    public bool isActivated = true;

    public void Start()
    {
        _recordsArray = new TargetRecord[_arraySize];
        for (int i = 0; i < _recordsArray.Length; i++)
    {
        _recordsArray[i] = new TargetRecord(Vector3.zero, Vector3.one); // Default position and scale
    }
    }

    // update Follower transform data
    public void Update()
    {
        if (isActivated)
        {
            recordData = true; //Start recording player's positions
        }
        else
        {
            recordData = false; //Stop recording player's positions
        }
        if (target == null)
        {
            Debug.LogError("Target is not assigned!");
            return;
        }
        if (recordData)
        {
            RecordData(Time.deltaTime);
        }
        if (_recordsArray[_a] != null && _recordsArray[_b] != null)
        {
            // Apply movement
            transform.position = Vector3.Lerp(_recordsArray[_a].position1, _recordsArray[_b].position1, Time.deltaTime);

            // Clamp scale to prevent extreme values
            Vector3 clampedScale = Vector3.Min(_recordsArray[_a].scale1, Vector3.one * 5f); // Limit max scale to 5
            transform.localScale = clampedScale;
        }
        else
        {
            Debug.LogWarning("Records in array are null!");
        }
    }

    [Serializable] // Fixed attribute

    public class TargetRecord
    {
        public Vector3 position1; // World position
        public Vector3 scale1; // Scale of player. Used to flip sprite/animation.

        public TargetRecord(Vector3 position2, Vector3 scale2)
        {
            position1 = position2;
            scale1 = scale2;
        }
    }

    // Fill array list with Player positions.
    public void RecordData(float deltaTime)
    {
        // record target data
        _recordsArray[_a] = new TargetRecord(target.position, target.localScale);

        // set next record index
        if (_a < _recordsArray.Length - 1)
            _a++;
        else
            _a = 0;

        // set next follow index
        if (_b < _recordsArray.Length - 1)
            _b++;
        else
            _b = 0;

        // handle current record
        _record = _recordsArray[_b];
    }
}
