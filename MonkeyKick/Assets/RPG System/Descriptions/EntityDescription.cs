// Merle Roji
// 1/15/22

using UnityEngine;
using System.Collections.Generic;

namespace MonkeyKick.RPGSystem.Entities
{
    [CreateAssetMenu(fileName = "Description", menuName = "RPGSystem/Entity/Entity Description")]
    public class EntityDescription : ScriptableObject
    {
        [Header("Basic Information")]
        [SerializeField] protected string _name = "Name";
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        [TextArea(15, 20)]
        [SerializeField] protected string _description;
        public string Description
        {
            get => _description;
            set => _description = value;
        }
    }
}
