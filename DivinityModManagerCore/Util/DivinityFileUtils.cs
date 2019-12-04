﻿using Alphaleonis.Win32.Filesystem;
using DivinityModManager.Models;
using LSLib.LS;
using LSLib.LS.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DivinityModManager.Util
{
	/// <summary>
	/// Gets a unique file name if the file already exists.
	/// Source: https://stackoverflow.com/a/13050041
	/// </summary>
	public static class DivinityFileUtils
	{
		public static string GetUniqueFilename(string fullPath)
		{
			if (!Path.IsPathRooted(fullPath))
				fullPath = Path.GetFullPath(fullPath);
			if (File.Exists(fullPath))
			{
				String filename = Path.GetFileName(fullPath);
				String path = fullPath.Substring(0, fullPath.Length - filename.Length);
				String filenameWOExt = Path.GetFileNameWithoutExtension(fullPath);
				String ext = Path.GetExtension(fullPath);
				int n = 1;
				do
				{
					fullPath = Path.Combine(path, String.Format("{0} ({1}){2}", filenameWOExt, (n++), ext));
				}
				while (File.Exists(fullPath));
			}
			return fullPath;
		}


		public static List<string> IgnoredPackageFiles = new List<string>(){
			"ReConHistory.txt",
			"dialoglog.txt",
			"errors.txt",
			"log.txt",
			"personallog.txt",
			"story_orphanqueries_found.txt",
			".ailog",
			".log",
			".debugInfo",
			".dmp"
		};

		private static bool IgnoreFile(string targetFilePath, string ignoredFileName)
		{
			if (Path.GetFileName(targetFilePath).Equals(ignoredFileName, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			if (Path.GetExtension(targetFilePath).Equals(ignoredFileName, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			return false;
		}
		#region Package Creation Async
		public static async Task<bool> CreatePackageAsync(string dataRootPath, List<string> inputPaths, string outputPath, List<string> ignoredFiles, CancellationToken? token = null)
		{
			try
			{
				if (token == null) token = CancellationToken.None;

				if (token.Value.IsCancellationRequested)
				{
					return false;
				}

				if (!dataRootPath.EndsWith(Alphaleonis.Win32.Filesystem.Path.DirectorySeparatorChar.ToString()))
				{
					dataRootPath += Alphaleonis.Win32.Filesystem.Path.DirectorySeparatorChar;
				}

				var package = new Package();

				foreach (var f in inputPaths)
				{
					if (token.Value.IsCancellationRequested) throw new TaskCanceledException("Cancelled package creation.");
					await AddFilesToPackageAsync(package, f, dataRootPath, outputPath, ignoredFiles, token.Value);
				}

				Trace.WriteLine($"Writing package '{outputPath}'.");
				using (var writer = new PackageWriter(package, outputPath))
				{
					await WritePackageAsync(writer, package, outputPath, token.Value);
				}
				return true;
			}
			catch (Exception ex)
			{
				if (!token.Value.IsCancellationRequested)
				{
					Trace.WriteLine($"Error creating package: {ex.ToString()}");
				}
				else
				{
					Trace.WriteLine($"Cancelled creating package: {ex.ToString()}");
				}
				return false;
			}
		}

		private static Task AddFilesToPackageAsync(Package package, string path, string dataRootPath, string outputPath, List<string> ignoredFiles, CancellationToken token)
		{
			Task task = null;

			task = Task.Run(() =>
			{
				if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
				{
					path += Path.DirectorySeparatorChar;
				}

				//var files = Directory.EnumerateFiles(path, "*.*", System.IO.SearchOption.AllDirectories).ToDictionary(k => k.Replace(dataRootPath, String.Empty), v => v);

				var files = Directory.EnumerateFiles(path, DirectoryEnumerationOptions.Recursive | DirectoryEnumerationOptions.LargeCache, new DirectoryEnumerationFilters()
				{
					InclusionFilter = (f) =>
					{
						return !ignoredFiles.Any(x => IgnoreFile(f.FullPath, x));
					}
				}).ToDictionary(k => k.Replace(dataRootPath, String.Empty), v => v);

				foreach (KeyValuePair<string, string> file in files)
				{
					if (token.IsCancellationRequested)
					{
						throw new TaskCanceledException(task);
					}
					FilesystemFileInfo fileInfo = FilesystemFileInfo.CreateFromEntry(file.Value, file.Key);
					package.Files.Add(fileInfo);
				}
			}, token);

			return task;
		}

		private static Task WritePackageAsync(PackageWriter writer, Package package, string outputPath, CancellationToken token)
		{
			var task = Task.Run(async () =>
			{
				// execute actual operation in child task
				var childTask = Task.Factory.StartNew(() =>
				{
					try
					{
						//writer.WriteProgress += WriteProgressUpdate;
						writer.Version = PackageVersion.V13;
						writer.Compression = CompressionMethod.LZ4;
						writer.CompressionLevel = CompressionLevel.MaxCompression;
						writer.Write();
					}
					catch (Exception)
					{
						// ignored because an exception on a cancellation request 
						// cannot be avoided if the stream gets disposed afterwards 
					}
				}, TaskCreationOptions.AttachedToParent);

				var awaiter = childTask.GetAwaiter();
				while (!awaiter.IsCompleted)
				{
					await Task.Delay(0, token);
				}
			}, token);

			return task;
		}
		#endregion
		#region Package Creation
		public static bool CreatePackage(string dataRootPath, List<string> inputPaths, string outputPath, List<string> ignoredFiles)
		{
			try
			{
				if (!dataRootPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
				{
					dataRootPath += Path.DirectorySeparatorChar;
				}

				var package = new Package();

				foreach (var f in inputPaths)
				{
					AddFilesToPackage(package, f, dataRootPath, outputPath, ignoredFiles);
				}

				Trace.WriteLine($"Writing package '{outputPath}'.");
				using (var writer = new PackageWriter(package, outputPath))
				{
					WritePackage(writer, package, outputPath);
				}
				return true;
			}
			catch (Exception ex)
			{
				Trace.WriteLine($"Error creating package: {ex.ToString()}");
				return false;
			}
		}

		private static void AddFilesToPackage(Package package, string path, string dataRootPath, string outputPath, List<string> ignoredFiles)
		{
			if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
			{
				path += Path.DirectorySeparatorChar;
			}

			var files = Directory.EnumerateFiles(path, DirectoryEnumerationOptions.Recursive | DirectoryEnumerationOptions.LargeCache, new DirectoryEnumerationFilters()
			{
				InclusionFilter = (f) =>
				{
					return !ignoredFiles.Any(x => !IgnoreFile(f.FullPath, x));
				}
			}).ToDictionary(k => k.Replace(dataRootPath, String.Empty), v => v);

			foreach (KeyValuePair<string, string> file in files)
			{
				Trace.WriteLine("Creating FilesystemFileInfo ");
				FilesystemFileInfo fileInfo = FilesystemFileInfo.CreateFromEntry(file.Value, file.Key);
				package.Files.Add(fileInfo);
			}
		}

		private static void WritePackage(PackageWriter writer, Package package, string outputPath)
		{
			try
			{
				//writer.WriteProgress += WriteProgressUpdate;
				writer.Version = PackageVersion.V13;
				writer.Compression = CompressionMethod.LZ4;
				writer.CompressionLevel = CompressionLevel.MaxCompression;
				writer.Write();
			}
			catch (Exception)
			{
				// ignored because an exception on a cancellation request 
				// cannot be avoided if the stream gets disposed afterwards 
			}
		}
		#endregion
	}
}