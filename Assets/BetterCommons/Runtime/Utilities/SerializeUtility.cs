using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;

namespace Better.Commons.Runtime.Utilities
{
	public static class SerializeUtility
	{
		public static Task<byte[]> SerializeAsync(object obj)
		{
			if (obj == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(obj));

				var emptyBytes = Array.Empty<byte>();
				return Task.FromResult(emptyBytes);
			}

			var task = Task.Factory.StartNew(() => Serialize(obj));
			return task;
		}

		public static byte[] Serialize(object obj)
		{
			if (obj == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(obj));
				return Array.Empty<byte>();
			}

			var binFormatter = new BinaryFormatter();

			using (var stream = new MemoryStream())
			{
				binFormatter.Serialize(stream, obj);
				return stream.ToArray();
			}
		}

		public static async Task<T> DeserializeAsync<T>(byte[] bytes)
			where T : class
		{
			if (bytes == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(bytes));

				return null;
			}

			using (var stream = new MemoryStream())
			{
				var binFormatter = new BinaryFormatter();
				await stream.WriteAsync(bytes, 0, bytes.Length);
				stream.Position = 0;

				var result = binFormatter.Deserialize(stream) as T;
				return result;
			}
		}

		public static T Deserialize<T>(byte[] bytes)
			where T : class
		{
			if (bytes == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(bytes));

				return null;
			}

			using (var stream = new MemoryStream())
			{
				var binFormatter = new BinaryFormatter();

				stream.Write(bytes, 0, bytes.Length);
				stream.Position = 0;

				var result = binFormatter.Deserialize(stream) as T;
				return result;
			}
		}

		public static byte[] CompressGZip(byte[] bytes)
		{
			if (bytes == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(bytes));

				return Array.Empty<byte>();
			}

			using (var memoryStream = new MemoryStream())
			{
				using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
				{
					gzipStream.Write(bytes, 0, bytes.Length);
				}

				return memoryStream.ToArray();
			}
		}

		public static byte[] DecompressGZip(byte[] bytes)
		{
			if (bytes == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(bytes));

				return Array.Empty<byte>();
			}

			using (var memoryStream = new MemoryStream())
			{
				using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
				{
					gzipStream.Write(bytes, 0, bytes.Length);
				}

				return memoryStream.ToArray();
			}
		}

		public static async Task<byte[]> CompressGZipAsync(byte[] bytes)
		{
			if (bytes == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(bytes));

				return Array.Empty<byte>();
			}

			using (var memoryStream = new MemoryStream())
			{
				using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
				{
					await gzipStream.WriteAsync(bytes, 0, bytes.Length);
				}

				return memoryStream.ToArray();
			}
		}

		public static async Task<byte[]> DecompressGZipAsync(byte[] bytes)
		{
			if (bytes == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(bytes));

				return Array.Empty<byte>();
			}

			using (var memoryStream = new MemoryStream())
			{
				using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
				{
					await gzipStream.WriteAsync(bytes, 0, bytes.Length);
				}

				return memoryStream.ToArray();
			}
		}

		public static byte[] CompressDeflate(byte[] bytes)
		{
			if (bytes == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(bytes));

				return Array.Empty<byte>();
			}

			using (var output = new MemoryStream())
			{
				using (var stream = new DeflateStream(output, CompressionLevel.Optimal))
				{
					stream.Write(bytes, 0, bytes.Length);
				}

				return output.ToArray();
			}
		}

		public static byte[] DecompressDeflate(byte[] bytes)
		{
			if (bytes == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(bytes));

				return Array.Empty<byte>();
			}

			using (var input = new MemoryStream(bytes))
			{
				using (var output = new MemoryStream())
				{
					using (var stream = new DeflateStream(input, CompressionMode.Decompress))
					{
						stream.CopyTo(output);
					}

					return output.ToArray();
				}
			}
		}

		public static async Task<byte[]> CompressDeflateAsync(byte[] bytes)
		{
			if (bytes == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(bytes));

				return Array.Empty<byte>();
			}

			using (var output = new MemoryStream())
			{
				using (var stream = new DeflateStream(output, CompressionLevel.Optimal))
				{
					await stream.WriteAsync(bytes, 0, bytes.Length);
				}

				return output.ToArray();
			}
		}

		public static async Task<byte[]> DecompressDeflateAsync(byte[] bytes)
		{
			if (bytes == null)
			{
				DebugUtility.LogException<ArgumentNullException>(nameof(bytes));

				return Array.Empty<byte>();
			}

			var input = new MemoryStream(bytes);

			using (var output = new MemoryStream())
			{
				using (var stream = new DeflateStream(input, CompressionMode.Decompress))
				{
					await stream.CopyToAsync(output);
				}

				return output.ToArray();
			}
		}

		public static string Compress(string uncompressedString)
		{
			if (uncompressedString.IsNullOrEmpty())
			{
				DebugUtility.LogException<ArgumentException>(nameof(uncompressedString));

				return string.Empty;
			}

			var uncompressed = Encoding.UTF8.GetBytes(uncompressedString);
			var compressed = CompressDeflate(uncompressed);

			var result = Convert.ToBase64String(compressed);
			return result;
		}

		public static string Decompress(string compressedString)
		{
			if (compressedString.IsNullOrEmpty())
			{
				DebugUtility.LogException<ArgumentException>(nameof(compressedString));

				return string.Empty;
			}

			var compressed = Convert.FromBase64String(compressedString);
			var uncompressed = DecompressDeflate(compressed);

			var result = Encoding.UTF8.GetString(uncompressed);
			return result;
		}

		public static async Task<string> CompressAsync(string uncompressedString)
		{
			if (uncompressedString.IsNullOrEmpty())
			{
				DebugUtility.LogException<ArgumentException>(nameof(uncompressedString));

				return string.Empty;
			}

			var uncompressed = Encoding.UTF8.GetBytes(uncompressedString);
			var compressed = await CompressDeflateAsync(uncompressed);

			var result = Convert.ToBase64String(compressed);
			return result;
		}

		public static async Task<string> DecompressAsync(string compressedString)
		{
			if (compressedString.IsNullOrEmpty())
			{
				DebugUtility.LogException<ArgumentException>(nameof(compressedString));

				return string.Empty;
			}

			var compressed = Convert.FromBase64String(compressedString);
			var uncompressed = await DecompressDeflateAsync(compressed);

			var result = Encoding.UTF8.GetString(uncompressed);
			return result;
		}
	}
}