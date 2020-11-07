using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VWG.Community.Util.Helper;

namespace VWG.Community.Util
{
    public static class OpenCC
    {
        public static void Load()
        {
            // call EmbeddedDllClass.ExtractEmbeddedDlls() for each DLL that is needed
            EmbeddedDllClass.ExtractEmbeddedDlls("opencc.dll", Properties.Resources.opencc);
        }

        /// <summary>
        /// object for lock statement.
        /// </summary>
        private static object locker = new object();

        /// <summary>
        /// Convert input string with certain config files.
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="configFileName">Config file name.</param>
        /// <returns></returns>
        /// <exception cref="ExternalException">Throw when OpenCC occurs an error!</exception>
        public static string Convert(string input, string configFileName)
        {
            IntPtr opencc_ptr = opencc_open(configFileName);
            if (opencc_ptr == new IntPtr(-1))
            {
                ThrowOpenccException();
            }

            byte[] utf8StringBytes = Encoding.UTF8.GetBytes(input + char.MinValue);
            IntPtr resultPtr = opencc_convert_utf8(opencc_ptr, utf8StringBytes, (UIntPtr)utf8StringBytes.Length);
            if (resultPtr == IntPtr.Zero)
            {
                ThrowOpenccException();
            }

            string result = StringFromNativeUtf8(resultPtr);
            opencc_convert_utf8_free(resultPtr);

            var closeResult = opencc_close(opencc_ptr);
            if (closeResult != 0)
            {
                ThrowOpenccException();
            }

            return result;
        }

        private static string StringFromNativeUtf8(IntPtr nativeUtf8)
        {
            int len = OpenCCHelper.String.Strlen(nativeUtf8);

            byte[] buffer = new byte[len];
            Marshal.Copy(nativeUtf8, buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer);
        }

        private static void ThrowOpenccException()
        {
            lock (locker)
            {
                var errorMessage = opencc_error();
                var errorMessageString = StringFromNativeUtf8(errorMessage);
                throw new ExternalException(errorMessageString);
            }
        }

        /// <summary>
        /// Makes an instance of opencc.
        /// </summary>
        /// <param name="configFileName">Location of configuration file. If this is set to NULL, OPENCC_DEFAULT_CONFIG_SIMP_TO_TRAD will be loaded.</param>
        /// <returns>A description pointer of the newly allocated instance of opencc. On error the return value will be (opencc_t) -1.</returns>
        //[DllImport("libopencc")]
        // 用 local file
        //[DllImport("..\\..\\opencc_v1_1_1\\opencc.dll", EntryPoint = "opencc_open")]
        // 用 embedded file (see EmbeddedDllClass, codes in Program.cs and embedded dll in Resources.resx)
        [DllImport("opencc.dll", EntryPoint = "opencc_open", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr opencc_open(string configFileName);

        /// <summary>
        /// Converts UTF-8 string This function returns an allocated C-Style string, which stores the converted string.
        /// You MUST call opencc_convert_utf8_free() to release allocated memory.
        /// </summary>
        /// <param name="opencc">The opencc description pointer.</param>
        /// <param name="input">The UTF-8 encoded string.</param>
        /// <param name="length">The maximum length in byte to convert. If length is (size_t)-1, the whole string (terminated by '\0') will be converted.</param>
        /// <returns>The newly allocated UTF-8 string that stores text converted, or NULL on error.</returns>
        //[DllImport("libopencc")]
        // 用 local file
        //[DllImport("..\\..\\opencc_v1_1_1\\opencc.dll", EntryPoint = "opencc_convert_utf8")]
        // 用 embedded file (see EmbeddedDllClass, codes in Program.cs and embedded dll in Resources.resx)
        [DllImport("opencc.dll", EntryPoint = "opencc_convert_utf8", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr opencc_convert_utf8(IntPtr opencc, byte[] input, UIntPtr length);

        /// <summary>
        /// Releases allocated buffer by opencc_convert_utf8.
        /// </summary>
        /// <param name="str">Pointer to the allocated string buffer by opencc_convert_utf8.</param>
        //[DllImport("libopencc")]
        // 用 local file
        //[DllImport("..\\..\\opencc_v1_1_1\\opencc.dll")]
        // 用 embedded file (see EmbeddedDllClass, codes in Program.cs and embedded dll in Resources.resx)
        [DllImport("opencc.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void opencc_convert_utf8_free(IntPtr str);

        /// <summary>
        /// Destroys an instance of opencc.
        /// </summary>
        /// <param name="opencc">The description pointer.</param>
        /// <returns>0 on success or non-zero number on failure.</returns>
        //[DllImport("libopencc")]
        // 用 local file
        //[DllImport("..\\..\\opencc_v1_1_1\\opencc.dll")]
        // 用 embedded file (see EmbeddedDllClass, codes in Program.cs and embedded dll in Resources.resx)
        [DllImport("opencc.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int opencc_close(IntPtr opencc);

        /// <summary>
        /// Returns the last error message.
        /// Note that this function is the only one which is NOT thread-safe.
        /// </summary>
        /// <returns>Error message</returns>
        //[DllImport("libopencc")]
        // 用 local file
        //[DllImport("..\\..\\opencc_v1_1_1\\opencc.dll")]
        // 用 embedded file (see EmbeddedDllClass, codes in Program.cs and embedded dll in Resources.resx)
        [DllImport("opencc.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr opencc_error();
    }

    public enum OpenCCType
    {
        s2t,    // Simplified Chinese to Traditional Chinese 簡體到繁體
        t2s,    // Traditional Chinese to Simplified Chinese 繁體到簡體
        s2tw,   // Simplified Chinese to Traditional Chinese (Taiwan Standard) 簡體到臺灣正體
        tw2s,   // Traditional Chinese (Taiwan Standard) to Simplified Chinese 臺灣正體到簡體
        s2hk,   // Simplified Chinese to Traditional Chinese (Hong Kong variant) 簡體到香港繁體
        hk2s,   // Traditional Chinese (Hong Kong variant) to Simplified Chinese 香港繁體到簡體
        s2twp,  // Simplified Chinese to Traditional Chinese (Taiwan Standard) with Taiwanese idiom 簡體到繁體（臺灣正體標準）並轉換爲臺灣常用詞彙
        tw2sp,  // Traditional Chinese (Taiwan Standard) to Simplified Chinese with Mainland Chinese idiom 繁體（臺灣正體標準）到簡體並轉換爲中國大陸常用詞彙
        t2tw,   // Traditional Chinese (OpenCC Standard) to Taiwan Standard 繁體（OpenCC 標準）到臺灣正體
        hk2tw,  // Traditional Chinese (Hong Kong variant) to Traditional Chinese 香港繁體到繁體（OpenCC 標準）
        t2hk,   // Traditional Chinese (OpenCC Standard) to Hong Kong variant 繁體（OpenCC 標準）到香港繁體
        t2jp,   // Traditional Chinese Characters (Kyūjitai) to New Japanese Kanji (Shinjitai) 繁體（OpenCC 標準，舊字體）到日文新字體
        jp2t,   // New Japanese Kanji (Shinjitai) to Traditional Chinese Characters (Kyūjitai) 日文新字體到繁體（OpenCC 標準，舊字體）
        tw2t    // Traditional Chinese (Taiwan standard) to Traditional Chinese 臺灣正體到繁體（OpenCC 標準）
    }
}
