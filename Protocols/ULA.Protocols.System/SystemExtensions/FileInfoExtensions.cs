﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.System.SystemExtensions
{
    /// <summary>
    /// 	Extension methods for the FileInfo and FileInfo-Array classes
    /// </summary>
    public static class FileInfoExtensions
    {
        /// <summary>
        /// 	Renames a file.
        /// </summary>
        /// <param name = "file">The file.</param>
        /// <param name = "newName">The new name.</param>
        /// <returns>The renamed file</returns>
        /// <example>
        /// 	<code>
        /// 		var file = new FileInfo(@"c:\test.txt");
        /// 		file.Rename("test2.txt");
        /// 	</code>
        /// </example>
        public static FileInfo Rename(this FileInfo file, string newName)
        {
            var filePath = Path.Combine(Path.GetDirectoryName(file.FullName), newName);
            file.MoveTo(filePath);
            return file;
        }

        /// <summary>
        /// 	Renames a without changing its extension.
        /// </summary>
        /// <param name = "file">The file.</param>
        /// <param name = "newName">The new name.</param>
        /// <returns>The renamed file</returns>
        /// <example>
        /// 	<code>
        /// 		var file = new FileInfo(@"c:\test.txt");
        /// 		file.RenameFileWithoutExtension("test3");
        /// 	</code>
        /// </example>
        public static FileInfo RenameFileWithoutExtension(this FileInfo file, string newName)
        {
            var fileName = string.Concat(newName, file.Extension);
            file.Rename(fileName);
            return file;
        }

        /// <summary>
        /// 	Moves several files to a new folder at once and optionally consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "targetPath">The target path.</param>
        /// <returns>The moved files</returns>
        public static FileInfo[] MoveTo(this FileInfo[] files, string targetPath)
        {
            return files.MoveTo(targetPath, true);
        }

        /// <summary>
        /// 	Movies several files to a new folder at once and optionally consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "targetPath">The target path.</param>
        /// <param name = "consolidateExceptions">if set to <c>true</c> exceptions are consolidated and the processing is not interrupted.</param>
        /// <returns>The moved files</returns>
        public static FileInfo[] MoveTo(this FileInfo[] files, string targetPath, bool consolidateExceptions)
        {
            List<Exception> exceptions = null;

            foreach (var file in files)
            {
                try
                {
                    var fileName = Path.Combine(targetPath, file.Name);
                    file.MoveTo(fileName);
                }
                catch (Exception e)
                {
                    if (consolidateExceptions)
                    {
                        if (exceptions == null)
                            exceptions = new List<Exception>();
                        exceptions.Add(e);
                    }
                    else
                        throw;
                }
            }

            if ((exceptions != null) && (exceptions.Count > 0))
                throw new CombinedException("Error while moving one or several files, see InnerExceptions array for details.", exceptions.ToArray());

            return files;
        }

        /// <summary>
        /// 	Sets file attributes for several files at once
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "attributes">The attributes to be set.</param>
        /// <returns>The changed files</returns>
        public static FileInfo[] SetAttributes(this FileInfo[] files, FileAttributes attributes)
        {
            foreach (var file in files)
                file.Attributes = attributes;
            return files;
        }

        /// <summary>
        /// 	Appends file attributes for several files at once (additive to any existing attributes)
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "attributes">The attributes to be set.</param>
        /// <returns>The changed files</returns>
        public static FileInfo[] SetAttributesAdditive(this FileInfo[] files, FileAttributes attributes)
        {
            foreach (var file in files)
                file.Attributes = (file.Attributes | attributes);
            return files;
        }
    }
}
