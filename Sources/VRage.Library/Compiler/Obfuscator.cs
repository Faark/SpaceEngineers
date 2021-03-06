﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if XB1
using System.Diagnostics;
#endif

#if true //!XB1_TMP

namespace System.Reflection
{
#if XB1
	[Unsharper.UnsharperDisableReflection()]
#endif
	public static class Obfuscator
    {
		public const string NoRename = "cw symbol renaming";
		public static readonly bool EnableAttributeCheck = true;
#if XB1
		public static bool CheckAttribute(this MemberInfo member)
		{
			Debug.Assert(false);
			return false;
		}
#else

        public static bool CheckAttribute(this MemberInfo member)
        {
            if(!EnableAttributeCheck)
                return true;

            var attr = member.GetCustomAttributes(typeof(ObfuscationAttribute), false);
            foreach (var a in attr.OfType<ObfuscationAttribute>())
            {
                if (a.Feature == NoRename && a.Exclude)
                    return true;
            }
            return false;
        }
#endif
    }
}

#endif
