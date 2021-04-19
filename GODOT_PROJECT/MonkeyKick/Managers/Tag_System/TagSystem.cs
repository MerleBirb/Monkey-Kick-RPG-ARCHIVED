using Godot;
using System.Collections;
using System.Collections.Generic;

namespace Merlebirb.Tag
{
    //===== TAG SYSTEM =====//
    /*
    4/18/21
    Description: Controls the logic for the tags.
    Notes:
    - Thank you @rJe on github for the Unity version of this tag system!
    
    */

    public class TagSystem : Node
    {
        public static Dictionary<int, List<Node>> m_taggedObjects = new Dictionary<int, List<Node>>(); // the dictionary that holds all tags

        public static void Clear()
        {
            m_taggedObjects.Clear();
        }

        public static void AddObject(Tag tagObj)
        {
            var tags = tagObj.GetTags();
            var node = tagObj.GetParent();
            foreach(var tag in tags)
            {
                AddObjectForTag(tag, node);
            }
        }

        private static void AddObjectForTag(string tag, Node node)
        {
            var hash = tag.GetHashCode();
            if (!m_taggedObjects.ContainsKey(hash)) { m_taggedObjects[hash] = new List<Node>(); };

            var nodeList = m_taggedObjects[hash];
            if (!nodeList.Contains(node)) { nodeList.Add(node); };
        }

        public static void RemoveObject (Tag tagObj)
        {
            var tags = tagObj.GetTags();
            var node = tagObj.GetParent();
            foreach(var tag in tags)
            {
                RemoveObjectForTag(tag, node);
            }
        }

        private static void RemoveObjectForTag (string tag, Node node)
        {
            var hash = tag.GetHashCode();
            if (m_taggedObjects.ContainsKey(hash))
            {
                var nodeList = m_taggedObjects[hash];
                nodeList.Remove(node);
            }
        }

        public static List<Node> AllObjectsForTag(string tagName)
        {
            var hash = tagName.ToLower().GetHashCode();
            if (m_taggedObjects.ContainsKey(hash)) 
            { 
                return m_taggedObjects[hash]; 
            }
            else
            {
                return new List<Node>();
            }
        }

        public static Node ObjectForTag(string tagName)
        {
            var hash = tagName.ToLower().GetHashCode();
            if (m_taggedObjects.ContainsKey(hash))
            {
                var nodeList = m_taggedObjects[hash];
                if (nodeList.Count > 0)
                {
                    return nodeList[0];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}

