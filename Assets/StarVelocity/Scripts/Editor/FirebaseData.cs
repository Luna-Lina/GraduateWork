using UnityEditor;
using UnityEngine;

namespace StarVelocity.Data
{
    [CustomEditor(typeof(FirebaseWrapper))]
    public class FirebaseData : Editor
    {
        FirebaseWrapper firebaseWrapper;

        private void OnEnable()
        {
            firebaseWrapper = (FirebaseWrapper)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Load Data"))
            {
                firebaseWrapper.LoadData();
            }

            if (GUILayout.Button("Save Data"))
            {
                //firebaseWrapper.SaveData();

            }
        }
    }
}