namespace GAS.Config
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "GASConfig", menuName = "GAS/Config", order = 1)]
    public class GASConfig : ScriptableObject
    {
        [Header("GAS App Info")]
        public int appId;
        public string appToken;
    }

}