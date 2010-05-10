using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace Kopf.PacketPal.Plugins
{
    public static class PluginLoader
    {
        public static void loadFromFile(string fileName, ref ArrayList pluginArray)
        {
            // load specified file
            Assembly myAssembly = Assembly.LoadFile(fileName);
            // grab types (classes) defined
            Type[] myTypes = myAssembly.GetTypes();

            // look for plugins
            foreach (Type x in myTypes)
            {
                // is the type a Plugin
                if (x.IsSubclassOf(Type.GetType("Kopf.PacketPal.Plugins.Plugin")))
                {
                    // grab the constructor
                    ConstructorInfo ci = x.GetConstructor(Type.EmptyTypes);
                    // invoke the constructor and throw the result object into the array
                    pluginArray.Add((Plugin)(ci.Invoke(new Object[0])));
                }
            }
        }

        public static void loadFromDir(string dirName, ref ArrayList pluginArray)
        {
            // get directory info
            DirectoryInfo dir = new DirectoryInfo(dirName);
            // grab all .dll files in the directory
            FileInfo[] myFiles = dir.GetFiles("*.dll");
            foreach( FileInfo f in myFiles)
            {
                // load the plugins from this .dll file
                loadFromFile(f.FullName, ref pluginArray);
            }
        }
    }
}
