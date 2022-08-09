using System.Collections;
using UnityEngine;
using System;
using System.IO;

namespace Helpers
{
    /*     C O R O U T I N E S    H E L P E R
     *     -----------------------------------------------------|
           by Andreas Lishnevsky with the help of Unity Forums.

           USAGE:

           using Helper.CoroutineHelper
           <...> 
           this.DoAfterSec(MyFunction, 4f);
           this.DoAfterSec(() => MyFunction(0, false), 11f);
           this.DoNextFrame(MyFunction);

     */



    public static class CoroutineHelper
    {

        public static void DoAfter(this MonoBehaviour mb, Action f, float delayInSeconds)
        {
            mb.StartCoroutine(InvokeRoutine(f, delayInSeconds));
        }

        public static void DoNextFrame(this MonoBehaviour mb, Action f)
        {
            mb.StartCoroutine(InvokeRoutineF(f));
        }


        public static void DoAfterFrames(this MonoBehaviour mb, Action f, int delay)
        {
            mb.StartCoroutine(InvokeRoutineFrames(f, delay));
        }


        //IENUMERATORS:      
        private static IEnumerator InvokeRoutine(System.Action f, float delay)
        {
            yield return new WaitForSeconds(delay);
            f();
        }

        private static IEnumerator InvokeRoutineF(System.Action f)
        {
            yield return 0;
            f();
        }

        private static IEnumerator InvokeRoutineFrames(System.Action f, int delay)
        {
            yield return WaitFor.Frames(delay);
            f();
        }

    }

    public static class WaitFor
    {
        public static IEnumerator Frames(int frameCount)
        {
            while (frameCount > 0)
            {
                frameCount--;
                yield return null;
            }
        }
    }



    /// <summary> File System Utils </summary>
    public static class FS
    {
        /// <summary>
        /// Reads text file from Resources folder.
        /// USE: ReadFile(new string[] { "DIR", "Subdir?", "filename" });
        /// </summary>
        /// <returns>array of lines</returns>
        public static string[] ReadFile(string[] paths)
        {
            var path = Path.Combine(paths);
            TextAsset data = Resources.Load<TextAsset>(path);
            string[] file = data.text.Split('\n');
            return file;
        }
    }

}