using Godot;
using System.Collections;
using System.Collections.Generic;

namespace Merlebirb.Tag
{
    //===== TAG =====//
    /*
    4/18/21
    Description: trying to emulate Unity tags.
    Notes:
    - Thank you @rJe on github for the Unity version of this tag system!
    
    */

    public class Tag : Node
    {
        [Export] public List<string> m_tagList = new List<string>();

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            for (int i = 0; i < m_tagList.Count; i++)
            {
                var tag = m_tagList[i];
                m_tagList[i] = tag.Trim().ToLower();
            }

            UpdateTagSystem();
        }

        public void AddTag(string toAdd)
        {
            var tag = toAdd.ToLower();
            if (!m_tagList.Contains(tag))
            {
                RemoveFromTagSystem();
                m_tagList.Remove(tag);
                UpdateTagSystem();
            }
        }

        public void RemoveTag(string toRemove)
        {
            var tag = toRemove.ToLower();
            if (m_tagList.Contains(tag))
            {
                RemoveFromTagSystem();
                m_tagList.Remove(tag);
                UpdateTagSystem();
            }
        }

        public List<string> GetTags()
        {
            return m_tagList;
        }

        private void OnEnable()
        {
            GD.Print("Tag Enabled");
            UpdateTagSystem();
        }

        private void OnDisable()
        {
            RemoveFromTagSystem();
        }

        private void OnDestroy()
        {
            RemoveFromTagSystem();
        }

        private void UpdateTagSystem()
        {
            GD.Print("Updating Tag Dictionary...");
            TagSystem.AddObject(this);
        }

        private void RemoveFromTagSystem()
        {
            TagSystem.RemoveObject(this);
        }
    }
}

