#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KineticEnergy.Unity {

    public static class InspectorHelper {

        /// <summary>Tests if any unordered pair of a property in <see cref="Editor.targets"/> are unequal.</summary>
        /// <typeparam name="ScriptType">The <see cref="UnityEngine.Object"/> type that the editor is assigned to.</typeparam>
        /// <param name="targets"><see cref="Editor.targets"/></param>
        /// <param name="mixedTest">Checks if the two given <see cref="ScriptType"/>s are mixed.</param>
        /// <returns>Returns true if any unordered pair of the given array passes the given <see cref="Func{ScriptType, ScriptType, bool}"/>.</returns>
        public static bool TargetsAreMixed<ScriptType>(UnityEngine.Object[] targets, Func<ScriptType, ScriptType, bool> mixedTest)
        where ScriptType : UnityEngine.Object {
            var length = targets.Length;
            for(int i = 0; i < length; i++) {
                var script1 = targets[i] as ScriptType;
                for(int j = i + 1; j < length; j++) {
                    var script2 = targets[j] as ScriptType;
                    if(mixedTest(script1, script2)) return true;
                }
            }
            return false;
        }

    }

}

#endif