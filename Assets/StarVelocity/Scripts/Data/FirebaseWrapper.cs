using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace StarVelocity.Data
{
    public class FirebaseWrapper : MonoBehaviour
    {
        private static readonly string _firebasePlayerData = "playerData";
        public event Action<List<PlayerData>> OnDataLoaded;
        public List<PlayerData> playerData = new List<PlayerData>();

        void Start()
        {
#if UNITY_EDITOR
            FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
#endif
        }

        public void LoadData()
        {
            playerData.Clear();

            Task.Run(async () =>
            {
                var snapshot = await FirebaseDatabase.DefaultInstance.RootReference.Child(_firebasePlayerData).GetValueAsync();

                if (snapshot != null && snapshot.Exists)
                {
                    foreach (var child in snapshot.Children)
                    {
                        var playerJson = child.GetRawJsonValue();

                        if (!string.IsNullOrEmpty(playerJson))
                        {
                            var player = JsonUtility.FromJson<PlayerData>(playerJson);

                            if (player != null)
                            {
                                playerData.Add(player);
                            }
                        }
                    }
                }

                OnDataLoaded?.Invoke(playerData);
            });
        }

        public static void SaveData(string userName, string score)
        {
            FirebaseDatabase.DefaultInstance.RootReference.Child(_firebasePlayerData).GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Failed to load data from Firebase.");
                    return;
                }

                var snapshot = task.Result;

                if (snapshot != null && snapshot.Exists)
                {
                    foreach (var child in snapshot.Children)
                    {
                        var playerJson = child.GetRawJsonValue();

                        if (!string.IsNullOrEmpty(playerJson))
                        {
                            var player = JsonUtility.FromJson<PlayerData>(playerJson);

                            if (player != null && player.playerName == userName)
                            {
                                if (player.playerScore < int.Parse(score))
                                {
                                    FirebaseDatabase.DefaultInstance.RootReference.Child(_firebasePlayerData).Child(userName).SetRawJsonValueAsync(JsonUtility.ToJson(new PlayerData(int.Parse(score), userName)));
                                }
                            }
                            else
                            {
                                FirebaseDatabase.DefaultInstance.RootReference.Child(_firebasePlayerData).Child(userName).SetRawJsonValueAsync(JsonUtility.ToJson(new PlayerData(int.Parse(score), userName)));
                            }
                        }
                    }
                }
            });
        }

        [Serializable]
        public class PlayerData
        {
            public int playerScore;
            public string playerName;

            public PlayerData(int playerScore, string playerName)
            {
                this.playerScore = playerScore;
                this.playerName = playerName;
            }
        }
    }
}