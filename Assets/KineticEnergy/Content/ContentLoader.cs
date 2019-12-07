using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using UnityEngine;

using static KineticEnergy.Intangibles.Master.LevelsOfDetail;

namespace KineticEnergy.Content {

    public class ContentLoader {

        const string CONTENT_FOLDER = "Content";
        
        public List<ContentGroup> ContentGroups { get; } = new List<ContentGroup>();
        
        public ContentGroup FinalGroup { get; } = new ContentGroup(null);

        /// <summary>Creates a new <see cref="ContentLoader"/>.</summary>
        internal ContentLoader() { }

        /// <summary>Finds all mod directories and then 'processes' them.</summary>
        /// <seealso cref="ProcessDirectory"/>
        internal void FindAndProcessAll(Intangibles.Master.LevelsOfDetail logSettings, out string[] directories, out List<ContentGroup> groups) {

            directories = Directory.GetDirectories(CONTENT_FOLDER);
            groups = new List<ContentGroup>(directories.Length);

            foreach(string directory in directories) {
                ProcessDirectory(directory, logSettings, out _, out ContentGroup group);
                groups.Add(group);
            }

            foreach(ContentGroup group in groups) {
                FinalGroup.Add(group);
            }

        }

        internal void UnloadAll() { /* ? */ }

        /// <summary>In this context, a directory is a set of content/mod.</summary>
        /// <param name="directory">The path to the directory.</param>
        /// <seealso cref="FindAndProcessAll"/>
        internal void ProcessDirectory(string directory, Intangibles.Master.LevelsOfDetail logSettings, out Assembly assembly, out ContentGroup group) {

            if(CanShowLog(LevelOfDetail.Basic, logSettings.directories))
                Debug.LogFormat("/* Processing directory '{0}'.", directory);

            string file = null, prefix = directory + "\\KineticEnergy.Mods.";
            foreach(var found in Directory.GetFiles(directory))
                if(found.StartsWith(prefix)) { file = found; break; }

            if(file == null ? false : File.Exists(file)) {

                group = new ContentGroup(directory);
                ContentGroups.Add(group);
                assembly = GetDllFile(file);

                if(CanShowLog(LevelOfDetail.Basic, logSettings.directories))
                    Debug.LogFormat("\tAssembly '{0}' at '{1}' was found for this directory.", assembly.FullName, file);

                if(AssemblyIsLegal(in assembly, out string why)) {
                    var recognizedTypes = 0; var totalTypes = 0;

                    try {

                        Type[] types = assembly.GetTypes();
                        string origin = directory;
                        foreach(Type type in types) {
                            bool unrecognized = true; totalTypes++;
                            if(group.Add(new Content(type, origin), out ContentList found)) {
                                string tName = found.Type.Name;
                                if(CanShowLog(LevelOfDetail.High, logSettings.directories))
                                    Debug.LogFormat("\tFound type '{0}' from '{1}' and recognized it as '{2}'.", type.FullName, file, tName);
                                unrecognized = false;
                                recognizedTypes++;
                            }
                            if(CanShowLog(LevelOfDetail.All, logSettings.directories))
                                if(unrecognized) Debug.LogFormat("\tFound type '{0}' from '{1}', but did not recognize it.", type.FullName, file);
                        }

                    } catch(Exception e) {
                        if(CanShowLog(LevelOfDetail.Basic, logSettings.directories))
                            Debug.LogErrorFormat("\tAssembly was returned as legal, but was unable to be loaded because an error occured." +
                            " Given exception: \"{0}\".", e);
                    }

                    if(CanShowLog(LevelOfDetail.Basic, logSettings.directories))
                        Debug.LogFormat("\t{0}: Found {1} {2}, and {3} of them {4} recognized.",
                        recognizedTypes == 0 ? "Warning" : "Results", totalTypes, totalTypes > 1 ? "total types" : "type",
                        recognizedTypes == 0 ? "none" : recognizedTypes.ToString(), recognizedTypes == 1 ? "was" : "were");

                } else {

                    if(CanShowLog(LevelOfDetail.Basic, logSettings.directories))
                        Debug.LogErrorFormat("\tAssembly '{0}' was returned as illegal." +
                        " Given reason: '{1}'.", file, why);

                }

            } else {

                if(CanShowLog(LevelOfDetail.Basic, logSettings.directories))
                    Debug.LogWarningFormat("\tCould not find the assembly (ex. 'KineticEnergy.Mods.EpsteinDidntKillHimself.dll') for this directory.", file);
                assembly = null;
                group = null;

            }

            if(CanShowLog(LevelOfDetail.Basic, logSettings.directories))
                Debug.LogFormat("Finished processing directory '{0}'. */", directory);

        }

        /// <summary>Returns if the given <see cref="Assembly"/> is illegal.</summary>
        /// <param name="assembly">The <see cref="Assembly"/> to test.</param>
        /// <param name="why">Why is the <see cref="Assembly"/> illegal?</param>
        internal bool AssemblyIsLegal(in Assembly assembly, out string why) {

            var references = assembly.GetReferencedAssemblies();
            foreach(var reference in references) {
                //Debug.Log(reference.Name);
            }

            why = "Not illegal.";
            return true;

        }

        /// <summary>Uses <see cref="ObjImporter"/> to get the mesh from the '*.obj' at the given path.</summary>
        /// <param name="file">The path to the file.</param>
        public static Mesh GetObjFile(string file) {
            var obj = new ObjImporter().ImportFile(file);
            obj.name = file;
            return obj;
        }

        /// <summary>Uses <see cref="File.ReadAllBytes(string)"/> to get the image from the '*.png' at the given path.</summary>
        /// <param name="file">The path to the file.</param>
        public static Texture2D GetPngFile(string file, TextureFormat format, bool mipChain) {
            var png = new Texture2D(2, 2, format, mipChain);
            png.LoadImage(File.ReadAllBytes(file));
            png.name = file;
            return png;
        }

        /// <summary>Uses <see cref="File.ReadAllBytes(string)"/> to get the assembly from the '*.dll' at the given path.</summary>
        /// <param name="file">The path to the file.</param>
        public static Assembly GetDllFile(string file) {
            var dll = Assembly.Load(File.ReadAllBytes(file));
            return dll;
        }

    }

}
