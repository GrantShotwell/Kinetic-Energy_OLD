#if UNITY_EDITOR
using System;
using UnityEditor;

namespace KineticEnergy.Unity {

    public static class InspectorHelper {

        /// <summary>Tests if any unordered pair of a property in <see cref="Editor.targets"/> are unequal.</summary>
        /// <typeparam name="ScriptType">The <see cref="UnityEngine.Object"/> type that the editor is assigned to.</typeparam>
        /// <param name="targets"><see cref="Editor.targets"/></param>
        /// <param name="mixedTest">Checks if the two given <see cref="ScriptType"/>s are mixed.</param>
        /// <returns>Returns true if any unordered pair of the given array passes the given <see cref="Func{ScriptType, ScriptType, bool}"/>.</returns>
        public static bool TargetsAreMixed<ScriptType>(UnityEngine.Object[] targets, Func<ScriptType, ScriptType, bool> mixedTest)
        where ScriptType : UnityEngine.Object {

            //If there is only one or zero (n < 2) scripts to compare, then they're not mixed.
            var length = targets.Length;
            if(length < 2) return false;

            //Compare every script to... any of the scripts really. How about index zero?
            // If they're all equal, you only need one for comparison.
            var script1 = targets[0] as ScriptType;
            for(int i = 0; i < length; i++) {
                var script2 = targets[i] as ScriptType;
                if(mixedTest(script1, script2)) return true;
            }
            return false;

        }

    }

}

#endif