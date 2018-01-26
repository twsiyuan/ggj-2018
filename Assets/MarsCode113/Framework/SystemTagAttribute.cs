using System;

namespace MarsCode113
{
    public class SystemTagAttribute : Attribute
    {

        public string Tag;


        public SystemTagAttribute(string tag)
        {
            Tag = tag;
        }

    }
}