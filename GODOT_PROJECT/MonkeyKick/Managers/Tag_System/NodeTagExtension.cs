using Godot;

namespace Merlebirb.Tag
{
    //===== NODE TAG EXTENSION =====//
    /*
    4/19/21
    Description: Static class for extra functions for the tag system.
    Notes:
    - Thank you @rJe on github for the Unity version of this tag system!
  
    */
    
    public static class NodeTagExtension
    {
        public static Tag GetTag (Node node)
        {
            var tag = node.GetNode<Tag>("Tag");

            if (tag == null)
            {
                tag = new Tag();
                node.AddChild(tag);
            }

            return tag;
        }

        public static void AddTag(this Node node, string tagName)
        {
            var tag = GetTag(node);
            tag.AddTag(tagName);
        }

        public static void RemoveTag(this Node node, string tagName)
        {
            var tag = GetTag(node);
            tag.RemoveTag(tagName);
        }

        public static bool HasTag (this Node node, string tagName)
        {
            var tag = GetTag(node);
            return tag.m_tagList.Contains(tagName.ToLower());
        }
    }
}
